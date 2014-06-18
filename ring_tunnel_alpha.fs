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


// Based on code by Spektre posted at http://stackoverflow.com/questions/3407942/rgb-values-of-visible-spectrum
vec3 spectral_colour(float l) // RGB <0,1> <- lambda l <400,700> [nm]
{
    float r=0.0,g=0.0,b=0.0;
         if ((l>=400.0)&&(l<410.0)) { float t=(l-400.0)/(410.0-400.0); r=    +(0.33*t)-(0.20*t*t); }
    else if ((l>=410.0)&&(l<475.0)) { float t=(l-410.0)/(475.0-410.0); r=0.14         -(0.13*t*t); }
    else if ((l>=545.0)&&(l<595.0)) { float t=(l-545.0)/(595.0-545.0); r=    +(1.98*t)-(     t*t); }
    else if ((l>=595.0)&&(l<650.0)) { float t=(l-595.0)/(650.0-595.0); r=0.98+(0.06*t)-(0.40*t*t); }
    else if ((l>=650.0)&&(l<700.0)) { float t=(l-650.0)/(700.0-650.0); r=0.65-(0.84*t)+(0.20*t*t); }
         if ((l>=415.0)&&(l<475.0)) { float t=(l-415.0)/(475.0-415.0); g=             +(0.80*t*t); }
    else if ((l>=475.0)&&(l<590.0)) { float t=(l-475.0)/(590.0-475.0); g=0.8 +(0.76*t)-(0.80*t*t); }
    else if ((l>=585.0)&&(l<639.0)) { float t=(l-585.0)/(639.0-585.0); g=0.82-(0.80*t)           ; }
         if ((l>=400.0)&&(l<475.0)) { float t=(l-400.0)/(475.0-400.0); b=    +(2.20*t)-(1.50*t*t); }
    else if ((l>=475.0)&&(l<560.0)) { float t=(l-475.0)/(560.0-475.0); b=0.7 -(     t)+(0.30*t*t); }

	return vec3(r,g,b);
}

void main()
{
	vec2 p=(2.0*gl_FragCoord.xy-RENDERSIZE)/min(RENDERSIZE.x,RENDERSIZE.y);
	float r=length(p-vec2(0.0,-1.0));

	vec3 col=vec3(0.0);
	for(int i=0;i<20;i++)
	{
		float fi=float(i)-fract(2.0*TIME);
		float l=r*(fi*0.3+1.0)*2000.0-2800.0;
		col+=spectral_colour(l)/(0.01*fi*fi*fi*fi+1.0);
	}
    vec3 c = pow(col, vec3(1.0/2.2));
    if(c.x >0.0){
        gl_FragColor=vec4(pow(col,vec3(1.0/2.2)),1.0);
    }else{
        gl_FragColor = vec4(0., 0., 0., 0.);
    }
}