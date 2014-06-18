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

// just a basic template - @dist
// + fun


void main( void ) {
	vec2 position = ( gl_FragCoord.xy / RENDERSIZE.xy );
	vec2 positionn = ( position - vec2(0.5)) / vec2(RENDERSIZE.y/RENDERSIZE.x,1.0)*2.0;
	float dist = distance(vec2(0.0), positionn);
	float dist2 = float(int(dist*10.0))/10.0; // 10 steps in 1.
	float angle = atan(positionn.y, positionn.x);

	float value;
	value = abs(2.0-dist2) * abs(sin(sin(dist2*TIME*-8.)+2.*TIME*(1.0+dist2)+angle*3.));
	vec3 color = pow(value, 3.0) * vec3(0.7,0.4,0.0) * 0.4;
	
	gl_FragColor = vec4( color, 1.0 );

}