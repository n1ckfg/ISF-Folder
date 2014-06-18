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
//precision highp float;
precision mediump float;
//precision lowp float;
#endif 

   

#define ptpi 1385.4557313670110891409199368797 //powten(pi)
#define pipi  36.462159607207911770990826022692 //pi pied, pi^pi
#define picu  31.006276680299820175476315067101 //pi cubed, pi^3
#define pepi  23.140692632779269005729086367949 //powe(pi);
#define chpi  11.59195327552152062775175205256  //cosh(pi)
#define shpi  11.548739357257748377977334315388 //sinh(pi)
#define pisq  9.8696044010893586188344909998762 //pi squared, pi^2
#define twpi  6.283185307179586476925286766559  //two pi, 2*pi 
#define pi    3.1415926535897932384626433832795 //pi
#define sqpi  1.7724538509055160272981674833411 //square root of pi 
#define hfpi  1.5707963267948966192313216916398 //half pi, 1/pi
#define cupi  1.4645918875615232630201425272638 //cube root of pi
#define prpi  1.4396194958475906883364908049738 //pi root of pi
#define lnpi  1.1447298858494001741434273513531 //logn(pi); 
#define trpi  1.0471975511965977461542144610932 //one third of pi, pi/3
#define thpi  0.99627207622074994426469058001254//tanh(pi)
#define lgpi  0.4971498726941338543512682882909 //log(pi)       
#define rcpi  0.31830988618379067153776752674503// reciprocal of pi  , 1/pi  
#define rcpipi  0.0274256931232981061195562708591 // reciprocal of pipi  , 1/pipi 

vec3 warp(vec3 v)
{	
	v = sin(v)+cos(v);
	float a = atan(v.y,v.x)*2.+sin(v.z)*pisq;
	float r = length(v.xy)+sin(v.z/2.);
	vec3 c = vec3(cos(r)*0.5+0.5,sin(a)*0.5+0.5,(cos(a)*sin(r))*0.5+0.5 )*(1.+length(v.xy));
	return (c);
}
void main( void )
{
	
	vec2 pos = vv_FragNormCoord*pi;
	float t = TIME;
	vec3 col = vec3(0.0);
	float w =0.0;
	for(float i=1.; i<8.; i+=1.)
	{
		float fac = pow(3.,i);
		col +=warp(vec3(pos*log(fac),t/i)+col)/8.;
		w+=1.0;
	}

	col = (col*col*col)/w;
	gl_FragColor = vec4(col,1.0);
}