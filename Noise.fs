/*{
	"DESCRIPTION": "Noise generator",
	"CREDIT": "by Carter Rosenberg",
	"CATEGORIES": [
		"Generator"
	],
	"INPUTS": [
		{
			"NAME": "seed",
			"TYPE": "float",
			"MIN": 0.01,
			"MAX": 1.0,
			"DEFAULT": 0.5
		}		
	]
}*/

const vec3 colorSeed = vec3(0.19234, 0.342139, 0.72385);

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

void main()
{
	vec2 texc = vec2(vv_FragNormCoord[0],mod(mod(TIME,1.0)+vv_FragNormCoord[1],1.0));
	gl_FragColor = vec4(rand(texc*vec2(seed*TIME,colorSeed.x)),rand(texc*vec2(seed*TIME,colorSeed.y)),rand(texc*vec2(seed*TIME,colorSeed.z)),1.0);
}
