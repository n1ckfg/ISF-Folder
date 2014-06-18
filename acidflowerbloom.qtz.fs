/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "INPUTS": [
    
  ]
}
*/


//thematica
#define PROCESSING_COLOR_SHADER

#ifdef GL_ES
precision mediump float;
#endif
#define PI2 6.28318530

float func(float theta){return (2.0/(2.0+sin(theta*8.0)));}

vec3 distancier(vec2 pos){
	float d=length(pos);
	
	float theta0=(pos.y<0.0)? PI2-acos(pos.x/d): acos(pos.x/d);
	float a=d/func(theta0);
	float ecart=0.3+0.15*sin(TIME);
	float a0=floor(a/ecart)*ecart;
	
	float delta=abs(d-a0*func(theta0)*d/(1.1-d));
	if( delta< 0.05 ){
		return vec3(1.0-delta*20.0,delta*delta*400.0,1.0-delta*delta*200.0);
	}
	return vec3(abs(sin(12.0*theta0)),delta*delta*8.0, 1.0-abs(sin(2.0*theta0)));
}

void main( void ) {

	vec2 p = (2.0*( gl_FragCoord.xy / RENDERSIZE.xy )-1.0)*(2.0-sin(TIME*1.3));
	vec2 position=.5*vec2(p.x*cos(TIME)-p.y*sin(TIME),p.x*sin(TIME)+p.y*cos(TIME));
	gl_FragColor = vec4(  distancier(position), 1.0);}