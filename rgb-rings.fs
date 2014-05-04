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


float shift = 1.0;
float off = 1.0;
float ti = TIME*2.;

void main( void ) {
	
	float l = length(vv_FragNormCoord-vec2(.0,.0));
	vec3 color = vec3(0.,0.,0.);
	float lum = 0.;
	lum = (abs(sin(l*100.0-ti)/2.+.8) - abs(sin(l*96.0+ti*0.0)+.8))-.5;
	lum = smoothstep(.3,.8,lum);
	color.r = (abs(sin(l*(100.0+shift*1.)-ti)/2.+off) - abs(sin(l*(95.0+shift*1.)+ti*0.5)+off))-.5;
	color.g = (abs(sin(l*(100.0+shift*2.)-ti)/2.+off) - abs(sin(l*(95.0+shift*2.)+ti*1.0)+off))-.5;
	color.b = (abs(sin(l*(100.0+shift*3.)-ti)/2.+off) - abs(sin(l*(95.0+shift*3.)+ti*2.0)+off))-.5;
	//color = smoothstep(.3,.8,color);
	//color = vec3(lum);	
	gl_FragColor = vec4(color,1.);
	
}