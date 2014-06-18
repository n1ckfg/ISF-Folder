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
precision highp float;
#endif

/*
 * Flower in GLSL using polar coordinates
 * by Yours3lf
 *
 */


const float pi = 3.14159265;

vec2 topolar( vec2 i ) //converts coordinates to polar coordinates
{
	float r = length( i );
	float fi = atan( i.y / i.x );
	return vec2( r, fi );
}

vec3 tocolor( vec3 i ) //helper function so that I can use Colorpicker
{
	return i / 255.0;
}

bool petalfunc( vec2 val, vec2 p ) //generates a petal
{
	return sin( val.x * p.y ) * cos( val.y * p.y ) > p.x;
}

bool middlefunc( float radius, vec2 p ) //generates the middle part of the flower, essentially a circle
{
	return p.x < radius;
}

bool bottomfunc( float rotation, float thickness, vec2 p ) //generates the bottom part, called sepal
{
	return p.y + rotation < 1.0 && p.y + rotation > 1.0 - thickness;
}

bool circlefunc( float start, float thickness, vec2 p )
{
	return p.x < start + thickness && p.x > start;
}

bool eyefunc( vec2 offset, float radius, vec2 p )
{
	return length( p + offset ) < radius;
}

void main( void ) {

	vec2 pos = ( gl_FragCoord.xy / RENDERSIZE.xy ) - 0.5;
	vec2 polarpos = topolar( pos );
	polarpos.x *= 1.15;
	vec3 color = vec3( 0 );
	
	bool middle = middlefunc( 0.1, polarpos );
	
	bool petals = petalfunc( vec2( 4.0, 4.0 ), vec2( polarpos.x, polarpos.y + sin( TIME * 0.1 ) ) );
	bool petals2 = petalfunc( vec2( 3.0, 3.0 ), vec2( polarpos.x, polarpos.y + sin( TIME * 0.2 ) ) );
	bool petals3 = petalfunc( vec2( 5.0, 5.0 ), vec2( polarpos.x, polarpos.y + sin( TIME * 0.3 ) ) );
	bool petals4 = petalfunc( vec2( 2.0, 2.0 ), vec2( polarpos.x, polarpos.y + sin( TIME * 0.4 ) ) );
	
	bool bottom = ( bottomfunc( 2.525, 0.15, polarpos ) || bottomfunc( -0.675, 0.15, polarpos ) ) && pos.y < 0.0;
	
	bool mouth = circlefunc( 0.05, 0.0075, polarpos ) && pos.y < -0.02 || circlefunc( 0.1, 0.005, polarpos );
	
	bool eyes = eyefunc( vec2( -0.02, -0.015 ), 0.01, pos ) || eyefunc( vec2( 0.02, -0.015 ), 0.01, pos );
	
	vec3 petalcolor = tocolor( vec3( 255, 74, 95 ) );
	vec3 petalcolor2 = tocolor( vec3( 245, 20, 46 ) );
	vec3 petalcolor3 = tocolor( vec3( 242, 60, 69 ) );
	vec3 petalcolor4 = tocolor( vec3( 158, 58, 70 ) );
	
	vec3 middlecolor = tocolor( vec3( 240, 220, 67 ) );
	vec3 bottomcolor = tocolor( vec3( 43, 161, 51 ) );
	
	color += petalcolor * float( petals ) + 
		 petalcolor2 * float( petals2 ) + 
		 petalcolor3 * float( petals3 ) + 
		 petalcolor4 * float( petals4 );
	color = mix( color, middlecolor, float( middle ) );
	color = mix( color, bottomcolor, float( bottom ) * ( 1.0 - float(length( color ) > 0.0 ) ) );
	color += float( mouth ) + float( eyes );
	
	gl_FragColor = vec4( color, 1 );

}