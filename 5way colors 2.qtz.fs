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


// 2013-03-30 by @hintz

#define CGFloat float
#define M_PI 3.14159265359

vec3 hsvtorgb(float h, float s, float v)
{
	float c = v * s;
	h = mod((h * 6.0), 6.0);
	float x = c * (1.0 - abs(mod(h, 2.0) - 1.0));
	vec3 color;
 
	if (0.0 <= h && h < 1.0) 
	{
		color = vec3(c, x, 0.0);
	}
	else if (1.0 <= h && h < 2.0) 
	{
		color = vec3(x, c, 0.0);
	}
	else if (2.0 <= h && h < 3.0) 
	{
		color = vec3(0.0, c, x);
	}
	else if (3.0 <= h && h < 4.0) 
	{
		color = vec3(0.0, x, c);
	}
	else if (4.0 <= h && h < 5.0) 
	{
		color = vec3(x, 0.0, c);
	}
	else if (5.0 <= h && h < 6.0) 
	{
		color = vec3(c, 0.0, x);
	}
	else
	{
		color = vec3(0.0);
	}
 
	color += v - c;
 
	return color;
}

void main(void) 
{

	vec2 position = 0.5*(gl_FragCoord.xy - 0.5 * RENDERSIZE) / RENDERSIZE.y;
	float x = position.x;
	float y = position.y;
	
	CGFloat a = atan(x, y);
    
    	CGFloat d = sqrt(x*x+y*y);
    	CGFloat d0 = 0.5*(sin(d-TIME)+1.5)*d+0.02*TIME;
    	CGFloat d1 = 5.0; 
	
    	CGFloat u = mod(a*d1+sin(d*10.0+TIME), M_PI*2.0)/M_PI*0.5 - 0.5;
    	CGFloat v = mod(pow(d0*4.0, 0.75),1.0) - 0.5;
    
    	CGFloat dd = sqrt(u*u+v*v*d1);
    
    	CGFloat aa = atan(u, v);
    
    	CGFloat uu = mod(aa*3.0+3.0*cos(dd*16.0-TIME), M_PI*2.0)/M_PI*0.5 - 0.5;
    	// CGFloat vv = mod(dd*4.0,1.0) - 0.5;
    
    	CGFloat d2 = sqrt(uu*uu+v*v)*1.5;
    
	gl_FragColor = vec4( hsvtorgb(dd+TIME*0.5/d1, dd, d2), 1.0 );
}