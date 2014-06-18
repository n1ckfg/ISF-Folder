
/*
 {
    "CATEGORIES": [
                   "Automatically Converted"
                   ],
    "INPUTS": [
               {
               "MAX": [
                       1,
                       1
                       ],
               "MIN": [
                       0,
                       0
                       ],
               "NAME": "mouse",
               "TYPE": "point2D"
               },
               {
               "NAME": "height",
               "TYPE": "float",
               "DEFAULT": 18.0,
               "MIN": 1.0,
               "MAX": 60.0
               },
               {
               "NAME": "size",
               "TYPE": "float",
               "DEFAULT": 10.0,
               "MIN": 1.0,
               "MAX": 50.0
               },
               {
               "NAME": "noiseSize",
               "TYPE": "float",
               "DEFAULT": 8.0,
               "MIN": 1.0,
               "MAX": 16.0
               },
               {
               "NAME": "noiseStrength",
               "TYPE": "float",
               "DEFAULT": 0.25,
               "MIN": 0.1,
               "MAX": 1.0
               },
               {
               "NAME": "speed",
               "TYPE": "float",
               "DEFAULT": 4.0,
               "MIN": 1.0,
               "MAX": 10.0
               }
               
               
               ]
}
 */


#ifdef GL_ES
precision mediump float;
#endif

// Paremeters --------------------------

//const float height = 18.0;
//const float size = 10.0;
//const float noiseSize = 8.0;
//const float noiseStrength = 0.25;
const int noiseDepth = 3;
//const float speed = 4.0;

const vec3 color = vec3(2.5, 2.0, 1.5);

const bool followMouse = false;
const vec2 defaultPosition = vec2(0.5, 0.2);

//--------------------------------------



// Perlin noise source code from Stefan Gustavson
// https://github.com/ashima/webgl-noise/blob/master/src/classicnoise2D.glsl

vec4 mod289(vec4 x)
{
    return x - floor(x * (1.0 / 289.0)) * 289.0;
}

vec4 permute(vec4 x)
{
    return mod289(((x*34.0)+1.0)*x);
}

vec4 taylorInvSqrt(vec4 r)
{
    return 1.79284291400159 - 0.85373472095314 * r;
}

vec2 fade(vec2 t) {
    return t*t*t*(t*(t*6.0-15.0)+10.0);
}

// Classic Perlin noise
float cnoise(vec2 P)
{
    vec4 Pi = floor(P.xyxy) + vec4(0.0, 0.0, 1.0, 1.0);
    vec4 Pf = fract(P.xyxy) - vec4(0.0, 0.0, 1.0, 1.0);
    Pi = mod289(Pi); // To avoid truncation effects in permutation
    vec4 ix = Pi.xzxz;
    vec4 iy = Pi.yyww;
    vec4 fx = Pf.xzxz;
    vec4 fy = Pf.yyww;
    
    vec4 i = permute(permute(ix) + iy);
    
    vec4 gx = fract(i * (1.0 / 41.0)) * 2.0 - 1.0 ;
    vec4 gy = abs(gx) - 0.5 ;
    vec4 tx = floor(gx + 0.5);
    gx = gx - tx;
    
    vec2 g00 = vec2(gx.x,gy.x);
    vec2 g10 = vec2(gx.y,gy.y);
    vec2 g01 = vec2(gx.z,gy.z);
    vec2 g11 = vec2(gx.w,gy.w);
    
    vec4 norm = taylorInvSqrt(vec4(dot(g00, g00), dot(g01, g01), dot(g10, g10), dot(g11, g11)));
    g00 *= norm.x;
    g01 *= norm.y;
    g10 *= norm.z;
    g11 *= norm.w;
    
    float n00 = dot(g00, vec2(fx.x, fy.x));
    float n10 = dot(g10, vec2(fx.y, fy.y));
    float n01 = dot(g01, vec2(fx.z, fy.z));
    float n11 = dot(g11, vec2(fx.w, fy.w));
    
    vec2 fade_xy = fade(Pf.xy);
    vec2 n_x = mix(vec2(n00, n01), vec2(n10, n11), fade_xy.x);
    float n_xy = mix(n_x.x, n_x.y, fade_xy.y);
    return 2.3 * n_xy;
}


void main( void ) {
	vec2 position = (followMouse ? mouse : defaultPosition) * RENDERSIZE.xy;
	
	// Compute flame area
	vec2 coord;
	if (gl_FragCoord.y > position.y) {
		coord = vec2(gl_FragCoord.x, position.y + (gl_FragCoord.y - position.y) / height);
	} else {
		coord = gl_FragCoord.xy;
	}
	float dist = distance(position, coord) / RENDERSIZE.y;
	
	// Compute flame noise
	vec2 noisePosition = noiseSize * (gl_FragCoord.xy - position) / RENDERSIZE.y - vec2(0.0, speed * TIME);
	float noise = 0.0;
	for (int i = 0; i < noiseDepth; i++) {
		noise += cnoise(noisePosition * pow(2.0, float(i)));
	}
	gl_FragColor = vec4(1.0, mix(-size * dist, noise, noiseStrength) + color);
}	