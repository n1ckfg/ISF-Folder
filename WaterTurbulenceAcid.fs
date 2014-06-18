/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "INPUTS": [
    
  ]
}
*/


// water turbulence effect by joltz0r 2013-07-04, improved 2013-07-07
// ACIIIIEEEED! 3-phase color map, who needs HSV? ;) --joltz0r
#ifdef GL_ES
precision mediump float;
#endif



#define MAX_ITER 16
void main( void ) {

	vec2 p = vv_FragNormCoord*5.0;
	vec2 i = p;
	float c = 1.0;
	float inten = 1.0;

	vec2 sc = vec2(sin(TIME*0.9), cos(TIME*1.1)) * vec2(2.0);
	for (int n = 0; n < MAX_ITER; n++) {
		float t = TIME * (1.0 - (1.0 / float(n+1)));
		i = p + vec2(cos(t - i.x) + sin(t + i.y), sin(t - i.y) + cos(t + i.x));
		c += 1.0/length(p+sc)/length(vec2(sin(i.x+t)/inten,cos(i.y+t)/inten));
	}
	c /= float(MAX_ITER-1);
	c = pow(c,3.0-(sin(TIME*0.666)*1.5));
	gl_FragColor = vec4(abs(cos(sin(TIME*0.1+2.0)*c*TIME)), abs(cos(sin(TIME*0.1+4.0)*c*TIME+2.0)), abs(cos(sin(TIME*0.1)*c*TIME+4.0)), 1.0);
}