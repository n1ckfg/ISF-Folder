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


float pi =3.14159265;
float pi2 =pi*2.;
float shift = 1.0;
float off = 1.0;
float ti = TIME*6.;
float spread = 0.90;

int copies = 4;
vec2 sp = vv_FragNormCoord;
float theta = 0.123;
float freq = 4.;
float freq2 = freq - 0.1;
float ratio = RENDERSIZE.x/RENDERSIZE.y;

const int num = 1;


vec2 RotatePoint(vec2 origin, vec2 point, float radian) {   
    float s = sin(radian);   
    float c = cos(radian);  

    // translate point back to origin:  
    point.x -= origin.x;   
    point.y -= origin.y;   

    // rotate point   
    float xnew = point.x * c - point.y * s;   
    float ynew = point.x * s + point.y * c; 

    // translate point back to global coords:
    vec2 TranslatedPoint;
    TranslatedPoint.x = xnew + origin.x;  
    TranslatedPoint.y = ynew + origin.y; 

    return TranslatedPoint;
} 

void main( void ) {
	
	vec3 color = vec3(0.,0.,0.);
	float b3 = 0.5;
	for(int j = 1; j < num*3; j++){
	float jf = float(j);
		
	vec2 p = ( gl_FragCoord.xy / RENDERSIZE.xy )*vec2(2.0,2.0/ratio) - vec2(1.0,1.0/ratio);
	vec2 p2 = RotatePoint(vec2(0.,0.), p, pi * jf/float(num)*.5*sin(TIME/60.));	
	
	
	//float l = length(vec2(p.x,p.y*8.));	
	
	float b = length(p2.x)+length(p2.y*3.);
	
	float b1 = sin(b*50.);
	float b2 = smoothstep(.5,1., b1*sin(TIME/60.0))*1.0;		
	b3 += b2;
		
	color.x += b2/2.;
	color.y += b2/3.;
	color.z += b2/4.;
	//color = smoothstep(.1,2.0,color);
	//color = vec3(b3);
	}
	gl_FragColor = vec4(color,1.);
}