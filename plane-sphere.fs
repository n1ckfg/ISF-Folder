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


float sdSphere(vec3 p, float r){
	
	return length(p)-r;
}
float udRoundBox( vec3 p, vec3 b, float r ) {
	return length(max(abs(p)-b,0.0))-r;
}

float worldD(vec3 p){
	float d = sdSphere(p-vec3(0.0, 0.0, -5.0), 0.5);
	float d1 = udRoundBox(p-vec3(0.0, -0.8, -5.0), vec3(2.0, 0.001, 2.0), 0.05);
	return min(d,d1);
}

vec3 worldN(vec3 p, float coc) {
	vec3 e = vec3(coc, 0.0, 0.0);
	return normalize(
		vec3(
			worldD(p+e.xyy)-worldD(p-e.xyy),
			worldD(p+e.yxy)-worldD(p-e.yxy),
			worldD(p+e.yyx)-worldD(p-e.yyx)
			)
		);
}

void main( void ) {

	float bgcolor = 0.2 - 0.1*cos(0.1*TIME);
	float color = bgcolor;
	float shadow = 1.;
	vec3 p1, p2;
	float d, td;
	float coc = 1./length(RENDERSIZE);
	
	// camera setup
	vec3 view;
	view.xy = (-1.+2.*gl_FragCoord.xy/RENDERSIZE)*vec2(RENDERSIZE.x/RENDERSIZE.y,1.);
	view.z = -2.1;
	
	// camera ray direction
	vec3 rd = normalize(view);
	// light direction
	vec3 ld = normalize(vec3(-1.,-3.,-1.))*vec3(sin(TIME),1.0,cos(TIME));
	
	td = worldD(vec3(0.0));
	// ray march to world
	for(int i=0; i<128; i++) {
		p1 = rd*td;
		d = worldD(p1);
		
		if(d<coc) {
			// hit the world, copute shadow
			td = coc;
			for(int j=0; j<128; j++) {
				p2 = p1-ld*td;
				d = worldD(p2)+0.2;
				
				if(d<coc) {
					shadow = 0.;
					break;
				}
				td += d;
				shadow = min(shadow,2.*d/td);
				if(td>20.) break;
			}
			// grab color
			color = clamp(dot(-ld*shadow,worldN(p1,coc)),0.0,1.0);
			break;
		}
		td += d;
		if(td>20.) break;
	}
	
	float cor = (1.+bgcolor);
	color = (color+bgcolor)/cor;
	gl_FragColor = vec4( vec3(color), 1.0 );

}