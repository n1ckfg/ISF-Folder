/*{
	"DESCRIPTION": "testing an idea of generating particles in ISF",
	"CREDIT": "by carter rosenberg",
	"CATEGORIES": [
		"Generator"
	],
	"INPUTS": [
		{
			"NAME": "attraction_amount",
			"TYPE": "float",
			"MIN": -1.0,
			"MAX": 1.0,
			"DEFAULT": -0.25
		},
		{
			"NAME": "size_decay",
			"TYPE": "float",
			"MIN": 0.0,
			"MAX": 0.25,
			"DEFAULT": 0.05
		},
		{
			"NAME": "count",
			"TYPE": "float",
			"MIN": 2.0,
			"MAX": 512.0,
			"DEFAULT": 32.0
		}
	],
	"PERSISTENT_BUFFERS": {
		"particleBuffer": {
			"WIDTH": "$count",
			"HEIGHT": "1.0"
		}
	},
	"PASSES": [
		{
			"TARGET":"particleBuffer"
		},
		{
		
		}
	]
	
}*/


/*

	the particleBuffer stores the x,y and size of each particle
	on the 1st pass each pixel compares itself to all the others and updates its location
	on the 2nd pass go through each pixel, perform a hit test and determine if it needs to be drawn

*/


float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

float attraction(vec4 p1, vec4 p2, float G)	{
	//	G * (m1 + m2) / d^2
	float d = distance(vec2(p1.r,p1.g),vec2(p2.r,p2.g));
	return G * (p1.b + p2.b) / (d * d);
}

const vec2 seed1 = vec2(0.23345, 0.1293);
const vec2 seed2 = vec2(0.6743, 0.479242682);
const vec2 seed3 = vec2(0.212359, 0.0912341);

void main()
{
	int pCount = int(count);
	//	first pass: read the "particleBuffer"- remember, we're drawing to the persistent buffer "particleBuffer" on the first pass
	if (PASSINDEX == 0)	{
		
		vec4 p = IMG_THIS_NORM_PIXEL(particleBuffer);
		if ((p.a <= 0.01)||(p.b <= 0.01))	{
			//	randomize the start position, clamp it
			p.r = clamp(rand(seed1 * TIME * vv_FragNormCoord[0]),0.1,0.9);
			p.g = clamp(rand(seed2 * TIME * vv_FragNormCoord[0]),0.1,0.9);
			//	randomize the size and alpha, clamp it
			p.b = clamp(rand(seed3 * TIME * vv_FragNormCoord[0]),0.05,0.1);
			p.a = clamp(rand(seed1 * TIME * vv_FragNormCoord[0]),0.5,1.0);
		}
		else	{
			//p.a = p.a - 0.001;
			p.b = p.b - size_decay / 10.0;
			
			for(int i=0; i < pCount ; i++)	{
				vec4 tmp = IMG_NORM_PIXEL(particleBuffer,vec2(float(i)/RENDERSIZE.x,0.5));
				//	pull p in the direction of tmp based on the size
				float a = attraction (p, tmp, attraction_amount / 100.0);
				p.r = p.r + (p.r - tmp.r) * a;
				p.g = p.g + (p.g - tmp.g) * a;
			}
		}
		gl_FragColor = p;
	}
	//	second pass: read from "particleBuffer"
	else if (PASSINDEX == 1)	{
		vec4 color = vec4(1.0,1.0,1.0,0.0);
		for(int i=0; i < pCount ; i++)	{
			vec4 tmp = IMG_NORM_PIXEL(particleBuffer,vec2(float(i)/float(pCount),0.5));
			//	do a hit test – is this pixel within range of the particle
			float d = distance(vv_FragNormCoord, vec2(tmp.r,tmp.g));
			if (d < tmp.b)	{
				color.a = color.a + tmp.a;
				//break;
			}
		}
		gl_FragColor = color;
		//gl_FragColor = IMG_THIS_NORM_PIXEL(particleBuffer);
	}
}
