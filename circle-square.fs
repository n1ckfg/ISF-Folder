/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "INPUTS": [
    
  ]
}
*/


//xL
#ifdef GL_ES
precision mediump float;
#endif

float ratio = RENDERSIZE.x/RENDERSIZE.y;

void main( void ) {

	vec2 p = ( gl_FragCoord.xy / RENDERSIZE.xy )*vec2(2.0,2.0/ratio) - vec2(1.0,1.0/ratio);

	float ti = TIME*1.0;
	float a = sin(p.x * 3.14 * 4.0);
	float b = sin(p.y * 3.14 * 4.0);
	float c = a*b;
	float c1 = max(c,0.);
	float d = sin(c * 2.0 + 2.*sin(ti))+0.5;
	float e = sin(d*4.);
	float f = sin(e*8.);
	
		
	gl_FragColor = vec4(f, e, 1.-f, 1.);