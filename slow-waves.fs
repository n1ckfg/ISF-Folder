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


void main( void ) {

	vec2 position = ( gl_FragCoord.xy / RENDERSIZE.xy );
	vec2 splines = vec2(pow(1.0-abs(position.y-cos(position.x*9.0+(TIME*0.12))/10.0-0.55),20.0),
			    pow(1.0-abs(position.y+cos(position.x*7.0+(TIME*0.34))/10.0-0.45),20.0));
	
	splines += pow(splines.x+splines.y, 2.0);
	
	vec3 color = vec3(0.15 * splines.x * splines.y,
			  0.15 * splines.x * splines.y,
			  0.8 * splines.x * splines.y);
	
	
	gl_FragColor = vec4( color.r,color.g,color.b, 1.0 );
}