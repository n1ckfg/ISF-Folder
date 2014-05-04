/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "INPUTS": [
 {
 "NAME": "rate",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 100.0
 },
 {
 "NAME": "speed",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 60.0
 }

  ]
}
*/


#ifdef GL_ES
precision mediump float;
#endif



#define PI 0.01

void main( void ) {

	vec2 p = ( gl_FragCoord.xy / RENDERSIZE.xy ) - 0.5;
	
	float sx = 0.1*sin( rate * p.x - TIME*speed);
	
	float dy = 0.9/ ( 50. * abs(p.y - sx));
	
	gl_FragColor = vec4( (p.x + 0.5) * dy, 0.5 * dy, dy-1.35, 100.0 );

}