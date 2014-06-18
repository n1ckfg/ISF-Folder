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

#define PI 3.14159265358979


mat2 rot(float a) {
	return mat2(cos(a), -sin(a), sin(a), cos(a));
}

void main( void ) {

	vec2 position = vv_FragNormCoord*vec2(1.0,2.0)*(1.1+0.09*sin(0.5*TIME+sin(3.0-TIME-sin(TIME*1.5)*0.3)-sin(TIME*time)*0.01))+0.01*(sin(TIME-1.*sin(TIME*1.3))+0.3)*sin(TIME*6.0+sin(2.0*TIME));
	float color = 0.0;
	float height = 0.0;
	for(int i=0;i<40;i++) {
		position.y += 0.005;
		vec2 a = floor((rot( PI*abs(sin(TIME/5.)+1.)*.3))*position*20.0);
		vec2 b = floor((rot(-PI*abs(sin(TIME/5.)-1.)*.3))*position*20.0);
		vec2 c = fract(position*40.);
		height = max(height, float(30-i)*0.04*0.5*floor(4.0*sin(a+sin(a*b+TIME)-b*sin(a+2.0)).y));
	}
	
	color += height;
	
	gl_FragColor = vec4(vec3(color, color*0.5, sin(color+TIME/3.)*.75)*clamp(2.3*exp(length(vv_FragNormCoord)*-1.1),0.3,1.1),1);

}