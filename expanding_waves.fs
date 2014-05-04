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



#define PI 100

void main( void ) {

	vec2 p = ( gl_FragCoord.xy / RENDERSIZE.xy ) - 0.5;
	
	float sx = 0.2 * (p.x + 0.5) * sin( 90.0 * p.x - 10. * TIME);
	
	float dy = 1./ ( 50. * abs(p.y - sx));
	
	dy += 1./ (20. * length(p - vec2(p.x, 0.)));
	
	gl_FragColor = vec4( (p.x + 0.5) * dy, 0.5 * dy, dy, 1.0 );

}