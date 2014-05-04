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
 
 
 
const float color_intensity = 0.45;
const float Pi = 3.14159;
#define pi    3.1415926535897932384626433832795 //pi
 
vec4 spuke(vec4 pos)
{
	vec2 p   =((pos.z+vv_FragNormCoord*pi)+(sin((((length(sin(vv_FragNormCoord*(pos.xy)+TIME*pi)))+(cos((vv_FragNormCoord-TIME*pi)/pi)))))*vv_FragNormCoord))+pos.xy*pos.z; 
	vec3 col = vec3( 0.0, 0.0, 0.0 );
	float ca = 0.0;
	for( int j = 1; j < 10; j++ )
	{
		p *= 1.4;
		float jj = float( j );
		
		for( int i = 1; i <6; i++ )  
		{
			vec2 newp = p*0.96;
			float ii = float( i );
			newp.x += 1.2 / ( ii + jj ) * sin( ii * p.y + (p.x*.3) + cos(TIME/pi/pi)*pi*pi + 0.003 * ( jj / ii ) ) + 1.0;
			newp.y += 0.8 / ( ii + jj ) * cos( ii * p.x + (p.y*.3) + sin(TIME/pi/pi)*pi*pi + 0.003 * ( jj / ii ) ) - 1.0;
			p=newp;
			
		
		}
		p   *= 0.9;
		col += vec3( 0.5 * sin( pi * p.x ) + 0.5, 0.5 * sin( pi * p.y ) + 0.5, 0.5 * sin( pi * p.x ) * cos( pi * p.y ) + 0.5 )*(0.5*sin(pos.z*pi)+0.5);
		ca  += 0.7;
	}
	col /= ca;
	return vec4( col * col * col, 1.0 );
}
void main()
{
  vec2 p=(2.*vv_FragNormCoord);
  for(int i=1;i<8;i++)
  {
    vec2 newp=p;
    newp.x+=0.5/float(i)*sin(float(i)*Pi*p.y+TIME*.8)+0.1;
    newp.y+=0.5/float(i)*cos(float(i)*Pi*p.x+TIME*.8)-0.1;
    p=newp;
  }
  vec3 col=vec3(sin(p.x+p.y)*.5+.5,sin(p.x+p.y+6.)*.5+.5,sin(p.x+p.y+12.)*.5+.5);
  gl_FragColor=spuke(vec4(col*col, 1.0));
}
 