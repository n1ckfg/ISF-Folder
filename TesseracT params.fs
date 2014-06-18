/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "INPUTS": [
		{
			"NAME": "x_rot",
			"TYPE": "float",
			"DEFAULT": 0.5,
			"MIN": 0.0,
			"MAX": 1.0
		},
		{
			"NAME": "y_rot",
			"TYPE": "float",
			"DEFAULT": 0.3,
			"MIN": 0.0,
			"MAX": 1.0
		},
		{
			"NAME": "z_rot",
			"TYPE": "float",
			"DEFAULT": 0.1,
			"MIN": 0.0,
			"MAX": 1.0
		},
		{
			"NAME": "q_rot",
			"TYPE": "float",
			"DEFAULT": 1.0,
			"MIN": 0.0,
			"MAX": 1.0
		}
  ]
}
*/


// TesseracT by Dima..

#ifdef GL_ES
precision mediump float;
#endif


mat4 mat  = mat4 ( vec4 ( 1.0 , 0.0 , 0.0 , 0.0 ),
		   vec4 ( 0.0 , 1.0 , 0.0 , 0.0 ),
		   vec4 ( 0.0 , 0.0 , 1.0 , 0.0 ),
		   vec4 ( 0.0 , 0.0 , 0.0 , 1.0 ) );

vec2 pos;

vec4 col = vec4 ( 0.0, 0.0, 0.0, 1000.0 );
	
void Rotate ( float angle, float d1, float d2, float d3, float d4);
void Point ( vec4 p );
void Line4 ( vec4 a, vec4 b );
void Line2 ( vec2 a, vec2 b );

void main( void ) {
	
	pos = gl_FragCoord.xy / RENDERSIZE.y;
	pos.x -= 1. - RENDERSIZE.y / RENDERSIZE.x;
	pos -= .5;
	
	Rotate ( TIME * q_rot,      0.0, 1.0, 1.0, 0.0 );
	Rotate ( TIME * x_rot, 1.0, 0.0, 1.0, 0.0 );
	Rotate ( TIME * y_rot, 1.0, 1.0, 0.0, 0.0 );
	Rotate ( TIME * z_rot, 1.0, 0.0, 0.0, 1.0 );
	
	Line4 ( vec4 ( .2, .2, .2, .2 ), vec4 (-.2, .2, .2, .2 ) );
	Line4 ( vec4 ( .2, .2, .2, .2 ), vec4 ( .2,-.2, .2, .2 ) );
	Line4 ( vec4 ( .2, .2, .2, .2 ), vec4 ( .2, .2,-.2, .2 ) );
	Line4 ( vec4 ( .2, .2, .2, .2 ), vec4 ( .2, .2, .2,-.2 ) );
	
	Line4 ( vec4 ( .2, .2, .2,-.2 ), vec4 (-.2, .2, .2,-.2 ) );
	Line4 ( vec4 ( .2, .2, .2,-.2 ), vec4 ( .2,-.2, .2,-.2 ) );
	Line4 ( vec4 ( .2, .2, .2,-.2 ), vec4 ( .2, .2,-.2,-.2 ) );
	
	Line4 ( vec4 ( .2, .2,-.2, .2 ), vec4 (-.2, .2,-.2, .2 ) );
	Line4 ( vec4 ( .2, .2,-.2, .2 ), vec4 ( .2,-.2,-.2, .2 ) );
	
	Line4 ( vec4 ( .2, .2,-.2,-.2 ), vec4 (-.2, .2,-.2,-.2 ) );
	Line4 ( vec4 ( .2, .2,-.2,-.2 ), vec4 ( .2,-.2,-.2,-.2 ) );
	Line4 ( vec4 ( .2, .2,-.2,-.2 ), vec4 ( .2, .2,-.2, .2 ) );
	
	Line4 ( vec4 ( .2,-.2, .2, .2 ), vec4 (-.2,-.2, .2, .2 ) );
	Line4 ( vec4 ( .2,-.2, .2, .2 ), vec4 ( .2,-.2,-.2, .2 ) );
	Line4 ( vec4 ( .2,-.2, .2, .2 ), vec4 ( .2,-.2, .2,-.2 ) );
	
	Line4 ( vec4 ( .2,-.2, .2,-.2 ), vec4 (-.2,-.2, .2,-.2 ) );
	Line4 ( vec4 ( .2,-.2, .2,-.2 ), vec4 ( .2,-.2,-.2,-.2 ) );
	
	Line4 ( vec4 ( .2,-.2,-.2, .2 ), vec4 (-.2,-.2,-.2, .2 ) );
	Line4 ( vec4 ( .2,-.2,-.2, .2 ), vec4 ( .2,-.2,-.2,-.2 ) );
	
	Line4 ( vec4 ( .2,-.2,-.2,-.2 ), vec4 (-.2,-.2,-.2,-.2 ) );
	
	
	Line4 ( vec4 (-.2, .2, .2, .2 ), vec4 (-.2,-.2, .2, .2 ) );
	Line4 ( vec4 (-.2, .2, .2, .2 ), vec4 (-.2, .2,-.2, .2 ) );
	Line4 ( vec4 (-.2, .2, .2, .2 ), vec4 (-.2, .2, .2,-.2 ) );
	
	Line4 ( vec4 (-.2, .2, .2,-.2 ), vec4 (-.2,-.2, .2,-.2 ) );
	Line4 ( vec4 (-.2, .2, .2,-.2 ), vec4 (-.2, .2,-.2,-.2 ) );
	
	Line4 ( vec4 (-.2, .2,-.2, .2 ), vec4 (-.2,-.2,-.2, .2 ) );
	
	Line4 ( vec4 (-.2, .2,-.2,-.2 ), vec4 (-.2,-.2,-.2,-.2 ) );
	Line4 ( vec4 (-.2, .2,-.2,-.2 ), vec4 (-.2, .2,-.2, .2 ) );
	
	Line4 ( vec4 (-.2,-.2, .2, .2 ), vec4 (-.2,-.2,-.2, .2 ) );
	Line4 ( vec4 (-.2,-.2, .2, .2 ), vec4 (-.2,-.2, .2,-.2 ) );
	
	Line4 ( vec4 (-.2,-.2, .2,-.2 ), vec4 (-.2,-.2,-.2,-.2 ) );
	
	Line4 ( vec4 (-.2,-.2,-.2, .2 ), vec4 (-.2,-.2,-.2,-.2 ) );
	
	Point ( vec4 ( .2, .2, .2, .2 ) );
	Point ( vec4 ( .2, .2, .2,-.2 ) );
	Point ( vec4 ( .2, .2,-.2, .2 ) );
	Point ( vec4 ( .2, .2,-.2,-.2 ) );
	Point ( vec4 ( .2,-.2, .2, .2 ) );
	Point ( vec4 ( .2,-.2, .2,-.2 ) );
	Point ( vec4 ( .2,-.2,-.2, .2 ) );
	Point ( vec4 ( .2,-.2,-.2,-.2 ) );
	
	Point ( vec4 (-.2, .2, .2, .2 ) );
	Point ( vec4 (-.2, .2, .2,-.2 ) );
	Point ( vec4 (-.2, .2,-.2, .2 ) );
	Point ( vec4 (-.2, .2,-.2,-.2 ) );
	Point ( vec4 (-.2,-.2, .2, .2 ) );
	Point ( vec4 (-.2,-.2, .2,-.2 ) );
	Point ( vec4 (-.2,-.2,-.2, .2 ) );
	Point ( vec4 (-.2,-.2,-.2,-.2 ) );
	
	float alpha = max(col.x,max(col.y,col.z));
	//float alpha = clamp(col.x+col.y+col.z,0.0,1.0);
	
	gl_FragColor = vec4( col.xyz, alpha );

}

void Line4 ( vec4 a, vec4 b )
{
	a = mat * a;
	a.xyz /= 1.5 + a.w * 2.;
	b = mat * b;
	b.xyz /= 1.5 + b.w * 2.;
	Line2 ( a.xy , b.xy );
}

void Line2 ( vec2 a, vec2 b )
{
	float d = distance ( pos , a ) + distance ( pos , b ) - distance ( a , b ) + 1e-5;
	col += max ( 1. - pow ( d * 14. , 0.1 ) , -0.01 );
}

void Point ( vec4 p )
{
	p = mat * p;
	p.xyz /= 1.5 + p.w * 2.;
	
	float d = distance ( pos , p.xy );
	
	if ( d < .3 )
	if ( p.z < col.a ) {
		col.b += max ( 1.0 - pow ( d * 5.0 , .1 ) , 0.0 );
	}
}

void Rotate ( float angle, float d1, float d2, float d3, float d4)
{
	float c = cos (angle), s = sin (angle);
	mat *= mat4 ( vec4 (  c*d1+(1.-d1),  s * d2 * d1 , -s * d3 * d1 ,  s * d4 * d1 ),
		      vec4 ( -s * d1 * d2 ,  c*d2+(1.-d2),  s * d3 * d2 , -s * d4 * d2 ),
		      vec4 (  s * d1 * d3 , -s * d2 * d3 ,  c*d3+(1.-d3),  s * d4 * d3 ),
		      vec4 ( -s * d1 * d4 ,  s * d2 * d4 , -s * d3 * d4 ,  c*d4+(1.-d4)) );
}