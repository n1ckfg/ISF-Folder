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


#define iter 6

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

vec3 rand3(vec2 v){
	return vec3(rand(v),rand(v+350.0),rand(v+1001.0));
}

void main( void ) {
	vec2 p = vv_FragNormCoord;
	float power=0.5+abs(sin(0.25*TIME))*4.0;
	p=vec2(p.x+p.y,p.y-p.x);
	vec3 c = p.xxx*0.;
	vec2 t;
	for(int i=0;i<iter;i++){
		t=abs(2.0*fract(p)-1.0);
		c+=rand3(floor(p))*clamp(1.0-pow(pow(t.x,power)+pow(t.y,power),1.0/power),0.0,1.0);
		p*=3.0;
	}
	gl_FragColor = vec4( c,1.0);
}