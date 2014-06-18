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


const float Tau		= 6.2832;
const float speed	= 2.;
const float density	= .2;
const float shape	= .01;

float random( vec2 seed ) {
	return fract(sin(seed.x-seed.y)*1e6);
}

float Cell(vec2 coord) {
	vec2 cell = fract(coord) * vec2(1.,.8) - vec2(0.,.2);
	return (1.-length(cell*2.-1.))*step(random(floor(coord)*.42),density)*2.;
}

void main( void ) {

	vec2 p = gl_FragCoord.xy / RENDERSIZE - vec2(0.5);
	
	float a = fract(atan(p.x, p.y) / Tau);
	float d = length(p);
	
	vec2 coord = vec2(pow(d, shape), a)*256.;
	vec2 delta = vec2((-TIME*speed), .5);
	
	float c = 0.;
	for(int i=0; i<3; i++) {
		coord += delta;
		c = max(c, Cell(coord));
	}
	
	gl_FragColor = vec4(c*d);
}