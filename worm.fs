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
precision lowp float;
#endif




#define PI 3.1414159265359
#define M  0.5
#define D  0.6

#define fmod 0.1

void main()
{
	vec2 p = (gl_FragCoord.xy - 0.5* RENDERSIZE) / min(RENDERSIZE.x, RENDERSIZE.y);
  	vec2 t = vec2(gl_FragCoord.xy / RENDERSIZE);

	vec3 c = vec3(0);
  	float y=0.;
	float x=0.;
	for(int i = 0; i < 75; i++) {
		float t = float(i) * TIME * 0.5;
		 x = 1. * cos(0.005*t);
		 y = 1. * sin(t * 0.007);
		vec2 o = 0.15 * vec2(x, y);
		float r = fract(y+x);
		float g = 1. - r;
		c += 0.0013 / (length(p-o)) * vec3(r, g, x+y);
	}

	gl_FragColor = vec4(c, 1);
}