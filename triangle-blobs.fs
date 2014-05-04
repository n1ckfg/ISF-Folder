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

// modified by @hintz


#define PI 3.14159
#define TWO_PI (PI*2.0)
#define N 6.0

void main(void) 
{
	vec2 center = (gl_FragCoord.xy);
	center.x=-100.12*sin(TIME/200.0);
	//center.y=-100.12*cos(TIME/200.0);
	
	vec2 v = (gl_FragCoord.xy - RENDERSIZE/20.0) / min(RENDERSIZE.y,RENDERSIZE.x) * 15.0;
	v.x=v.x-10.0;
	v.y=v.y-200.0;
	float col = 0.0;

	for(float i = 0.0; i < N; i++) 
	{
	  	float a = i * (TWO_PI/N) * 61.95;
		col += cos(TWO_PI*(v.y * cos(a) + v.x * sin(a) /*+ mouse.y +i*mouse.x*/ + sin(TIME*0.004)*100.0 ));
	}
	
	col /= 3.0;

	gl_FragColor = vec4(col*1.0, -col*1.0,-col*4.0, 1.0);
}