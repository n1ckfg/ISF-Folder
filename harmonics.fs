/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "INPUTS": [
    
  ]
}
*/


//xL
#ifdef GL_ES
precision mediump float;
#endif

float pi=3.14159265;
vec3 color = vec3 (.0,.0,.0);
float ratio = RENDERSIZE.x/RENDERSIZE.y;
float freq = 7.;
float ti = TIME*.30;
vec3 col = vec3 (.0,.0,.0);
float edge = .2;
float edgeW = .04;
float lineW = .01;
int i = 5;

void main( void ) {
	
	vec2 p = ( gl_FragCoord.xy / RENDERSIZE.xy )*vec2(2.0,2.0/ratio) - vec2(1.0,1.0/ratio);
		
		
	for(int j = 1; j < 41; j++){
	
	
	float a = sin(freq*p.x+float(j)/8.+.1*ti*float(j))+(p.y*2.0+1.0*.2);
	float b = smoothstep(edge,edge+edgeW, a)-smoothstep(edge+lineW,edge+edgeW+lineW, a);
			
	float h = b;
		float k = mod(float(j),3.);
		if(k == 0.0){
		color.r += h;}
		else if(k == 1.0){ 
		color.g += h;}
		else {color.b += h;}
			
		
	gl_FragColor = vec4(color, 1.);				
			
	}	
}	