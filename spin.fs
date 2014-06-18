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

// this was just a test, the thumbnail requires alpha to be set otherwise
// it's all black
void main( void ) {
	vec2 sp = vv_FragNormCoord;
	float TIME = TIME + atan(sp.y,sp.x)*float(int(64.*cos(TIME*0.4)));
	float l = length(sp);
	gl_FragColor.rgb = vec3(cos(l*35.0+TIME)) + vec3(1.0, 0.5, 0.0);
	gl_FragColor.a = cos(TIME+length(sp)+gl_FragColor.r+gl_FragColor.g+gl_FragColor.b);
}