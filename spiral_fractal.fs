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


/*
 * inspired by http://www.fractalforums.com/new-theories-and-research/very-simple-formula-for-fractal-patterns/
 * a slight(?) different 
 * public domain
 */
//dashxdr was here...
//ought to be a hotkey for save when "hide code" is active...
//forked from 16019.4
//forked from 16056.0

#define N 60
void main( void ) {
	vec2 v = (gl_FragCoord.xy - RENDERSIZE/2.0) / min(RENDERSIZE.y,RENDERSIZE.x) * 20.0;
	vec2 m = mouse;
	if(mouse.y>.96) m=vec2(.14, .9); // for when "save" button is pressed
	float rsum = 0.0;
	float pi2 = 3.1415926535 * 2.0;
	float a = (m.x-.5)*pi2;
	float C = cos(a);
	float S = sin(a);
	vec2 xaxis=vec2(C, -S);
	vec2 yaxis=vec2(S, C);
	float maxcycle=0.0;

	vec2 shift = vec2( 0, 1.618);
	float zoom = 1.0 + m.y*8.0;
	for ( int i = 0; i < N; i++ ){
		float rr = dot(v,v);
		if ( rr > 0.618 ){
			rr = 1.618/rr ;
			v.x = v.x * rr;
			v.y = v.y * rr;
		}
		if(rr > rsum)
		{
			rsum = rr;
			maxcycle = float(i);
		}

		v = vec2( dot(v,xaxis), dot(v,yaxis)) * zoom + shift;
	}
	
	float col = rsum/2.618;
	col = .2 + 2.0 * min(col, 1.0-col);
	float red, green, blue;
	
	red = fract(cos(maxcycle));
	green = fract(cos(maxcycle*1.2));
	blue = fract(cos(maxcycle*1.5));
	
	gl_FragColor = vec4(vec3(red, green, blue)*col*col, 1.0);
}