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


#define PI 3.14159265358979323846264

// little modification -- novalis


//shader by mattdesl - devmatt.wordpress.com
//uses webgl noise - https://github.com/ashima/webgl-noise


//////////// start of webgl-noise ////////////////
// Description : Array and textureless GLSL 2D simplex noise function.
//      Author : Ian McEwan, Ashima Arts.
//  Maintainer : ijm
//     Lastmod : 20110822 (ijm)
//     License : Copyright (C) 2011 Ashima Arts. All rights reserved.
//               Distributed under the MIT License. See LICENSE file.
//               https://github.com/ashima/webgl-noise
// 

vec3 mod289(vec3 x) {
  return x - floor(x * (1.0 / 289.0)) * 289.0;
}

vec2 mod289(vec2 x) {
  return x - floor(x * (1.0 / 289.0)) * 289.0;
}

vec3 permute(vec3 x) {
  return mod289(((x*34.0)+1.0)*x);
}

float snoise(vec2 v)
  {

	  
  const vec4 C = vec4(0.21,  // (3.0-sqrt(3.0))/6.0
                      0.366025403784439,  // 0.5*(sqrt(3.0)-1.0)
                     -0.577350269189626,  // -1.0 + 2.0 * C.x
                      0.024390243902439); // 1.0 / 41.0
// First corner
  vec2 i  = floor(v + dot(v, C.yy) );
  vec2 x0 = v -   i + dot(i, C.xx);

// Other corners
  vec2 i1;
  //i1.x = step( x0.y, x0.x ); // x0.x > x0.y ? 1.0 : 0.0
  //i1.y = 1.0 - i1.x;
  i1 = (x0.x > x0.y) ? vec2(1.0, 0.0) : vec2(0.0, 1.0);
  // x0 = x0 - 0.0 + 0.0 * C.xx ;
  // x1 = x0 - i1 + 1.0 * C.xx ;
  // x2 = x0 - 1.0 + 2.0 * C.xx ;
  vec4 x12 = x0.xyxy + C.xxzz;
  x12.xy -= i1;

// Permutations
  i = mod289(i); // Avoid truncation effects in permutation
  vec3 p = permute( permute( i.y + vec3(0.0, i1.y, 1.0 ))
		+ i.x + vec3(0.0, i1.x, 1.0 ));

  vec3 m = max(0.5 - vec3(dot(x0,x0), dot(x12.xy,x12.xy), dot(x12.zw,x12.zw)), 0.0);
  m = m*m ;
  m = m*m ;

// Gradients: 41 points uniformly over a line, mapped onto a diamond.
// The ring size 17*17 = 289 is close to a multiple of 41 (41*7 = 287)

  vec3 x = 2.0 * fract(p * C.www) - 1.0;
  vec3 h = abs(x) - 0.5;
  vec3 ox = floor(x + 0.5);
  vec3 a0 = x - ox;

// Normalise gradients implicitly by scaling m
// Approximation of: m *= inversesqrt( a0*a0 + h*h );
  m *= 1.79284291400159 - 0.85373472095314 * ( a0*a0 + h*h );

// Compute final noise value at P
  vec3 g;
  g.x  = a0.x  * x0.x  + h.x  * x0.y;
  g.yz = a0.yz * x12.xz + h.yz * x12.yw;
  return 130.0 * dot(m, g);
}

////////////// end of webgl-noise //////////////////




//mattdesl - devmatt.wordpress.com - textured planet by Matt DesLauriers

float clouds( vec2 coord ) {
	coord += vec2(TIME/100., 0);
	//standard fractal
	float n = snoise(coord);
	n += 0.5 * snoise(coord * 2.0);
	n += 0.25 * snoise(coord * 4.0);
	n += 0.125 * snoise(coord * 8.0);
	n += 0.0625 * snoise(coord * 16.0);
	n += 0.03125 * snoise(coord * 32.0);
	n += 0.03125 * snoise(coord * 64.0);
	return n;
}

float sphere(in vec3 p) {
	return length(vec3(0,0,-2)-p) - 1.;
}

float scene(in vec3 p) {
	return sphere(p);
}

vec3 calcN(in vec3 p) {
	float e = 1e-3;
	return normalize(vec3(
		scene(vec3(p.x+e,p.y,p.z))-scene(vec3(p.x-e,p.y,p.z)),
		scene(vec3(p.x,p.y+e,p.z))-scene(vec3(p.x,p.y-e,p.z)),
		scene(vec3(p.x,p.y,p.z+e))-scene(vec3(p.x,p.y,p.z-e))
	));
}

vec3 raytrace(in vec3 ro, in vec3 rd) {
	float d = 0.; float md = 5.;
	bool i = false;
	for (int s=0;s<64;s++) {
		float td = scene(ro + d*rd);
		if (td < 1e-3) return ro + d*rd;   // intersection
		if (d > md) return vec3(0);
		d += td;
	}
	return vec3(0);
}

void main(void) {
	vec3 color = vec3(0); // some background
	
	vec3 ro = vec3(vv_FragNormCoord,0); vec3 rd = normalize(vec3(vv_FragNormCoord,-1));
	vec3 ip = raytrace(ro, rd);
	
	vec3 ld = normalize(vec3(-1,-1,-.5));
	vec3 vd = vec3(0,0,-1);
	vec3 hd = normalize(-ld-vd);
	if (abs(ip.z) > 0.) { // we hit something, do lightning
		vec3 nd = calcN(ip);
		color += vec3(clouds(vv_FragNormCoord)); // ambient
		//color += smoothstep(0., 1., dot(nd, hd))*normalize(vec3(1.,1.,1.05)); // Blinn-Phong
		//color /= 2.;
		color = smoothstep(0., 1., color);
	}

	gl_FragColor = vec4(pow(color, vec3(1./2.2))*vec3(1.1,1.1,1), 1);
}