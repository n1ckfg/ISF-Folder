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

void main(){

	// square aspect ratio please..
	vec2 p = (gl_FragCoord.xy / (RENDERSIZE.x));
	// add an offset to center things
	p.y += .25;
	// define an origin
	vec3 c = vec3(0);
	// create a ring of blobs..
	for(int i = 0;i < 13;i++){
		// in a circle..
		float x = (sin(TIME + float(i) * (sin(TIME) * .48)));
		float y = (cos(TIME + float(i) * .48));
		float temp;
		// then, expanding outward from each blob away from teh origin..
		for(int j = 1; j < 10; j++){
		  vec2 o = vec2(x * (float(j) * .04) +.5, y *(float(j) * sin(TIME) * .04) +.5);
		  float r =  clamp(x * 2., 0.2, 1.0);
		  float g =  y * 2.;
		   clamp(g, 0.4, 0.8);	
		  c += 0.0008 / (length(p-o)) * vec3(r, g, x+y);
		// flip to alternate directions
		  temp = -y;
		  y = x;
		  x = temp;
		}
		
	}

	gl_FragColor = vec4(c * 1.3, 1);
}