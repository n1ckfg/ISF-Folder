/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "INPUTS": [
    {
      "MAX": [
        1,
        1
      ],
      "MIN": [
        0,
        0
      ],
      "NAME": "mouse",
      "TYPE": "point2D"
    }
  ]
}
*/


#ifdef GL_ES
precision mediump float;
#endif


float nsin(float x)
{
	return sin(x) * 0.5 + 0.5;	
}

void main( void ) {

	vec2 uv = gl_FragCoord.xy / RENDERSIZE.xy;
	vec2 aspect = vec2( RENDERSIZE.x / RENDERSIZE.y, 1.0 );
	vec2 p = ( uv * 2.0 - 1.0 ) * aspect;
	vec2 mt = ( mouse * 2.0 - 1.0 ) * aspect;
	vec3 c;
	const float iter = 227.0;
	
	for(float i = 0.0; i < iter; i++)
	{
		float t = TIME + i * 0.05;
		vec3 pos = vec3(sin(t), cos(t), 1.0 + (0.5 + 0.5 * sin(i / iter * 555.0)));
		float s = 1.0 / pos.z;
		vec2 pp = vec2(pos.x * s, pos.y * s);
		float d = distance(pp, p);
		d = (nsin(i)+0.2)*2.0 / d * (0.002 / pos.z);
		c += d;
	}
	c = pow(c, vec3(1.9, 1.5, 0.9));
	gl_FragColor = vec4( c, 1.0 );
}