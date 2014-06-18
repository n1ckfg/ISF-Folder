/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "INPUTS": [
    
  ]
}
*/


#ifdef GL_ES
precision mediump float;
#endif

// modified version of https://glsl.heroku.com/e#16506.1
// Hexagon code from http://glsl.heroku.com/e#6732.0


// Squish and strech the tunnel
#define STRETCH 0.4
#define SQUISH 0.1

// Thickness of the hexagon lines
#define THICKNESS 0.1

float hex(vec2 p, float r) {
	p = abs(p);
	return max(p.x+p.y*0.57735,p.y*1.1547)-r;
}

vec3 tex(vec2 pos) {
	vec2 p = pos*50.0; 

	p.x *= 1.1547;
	p.y += mod(floor(p.x), 2.0)*0.5;
	p = mod(p, 1.0) - 0.5;
	float d = max(-hex(vec2(p.x/1.1547,p.y),0.57735), hex(vec2(p.x*1.5*0.57735, p.y),0.57735));
	
	float r = THICKNESS;	
	return vec3(smoothstep(r, r-0.1, d),0,.2+.1*sin(TIME*5.));
}

void main(void) {
	vec2 p = vv_FragNormCoord;
	float a = atan(p.x,p.y);
	float r = sqrt(dot(p,p))+.1*pow(sin(TIME/2.),2.)-.1;

	vec2 uv;
	uv.x = 0.05*TIME+SQUISH/r;
	uv.y = a/3.14159265358979*STRETCH;
	uv = fract(uv);
	vec3 col = tex(uv);
	gl_FragColor = vec4(col*r,1);
}