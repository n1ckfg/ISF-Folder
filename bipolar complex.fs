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
    }
  ]
}
*/


#ifdef GL_ES
precision highp float;
#endif
#define pi 3.141592653589793238462643383279
#define pi_inv 0.318309886183790671537767526745
#define pi2_inv 0.159154943091895335768883763372

/// bipolar complex by @Flexi23
/// "logarithmic zoom with a spiral twist and a division by zero in the complex number plane." (from https://www.shadertoy.com/view/4ss3DB)

vec2 complex_mul(vec2 factorA, vec2 factorB){
  return vec2( factorA.x*factorB.x - factorA.y*factorB.y, factorA.x*factorB.y + factorA.y*factorB.x);
}

vec2 complex_div(vec2 numerator, vec2 denominator){
   return vec2( numerator.x*denominator.x + numerator.y*denominator.y,
                numerator.y*denominator.x - numerator.x*denominator.y)/
          vec2(denominator.x*denominator.x + denominator.y*denominator.y);
}

vec2 wrap_flip(vec2 uv){
	return vec2(1.)-abs(fract(uv*.5)*1.-1.);
}
 
float border(vec2 domain, float thickness){
   vec2 uv = fract(domain-vec2(0.5));
   uv = min(uv,1.-uv)*2.;
   return clamp(max(uv.x,uv.y)-1.+thickness,0.,1.)/(thickness);
}

float circle(vec2 uv, vec2 aspect, float scale){
	return clamp( 2. - length((uv-0.5)*aspect*scale), 0., 1.);
}

float sigmoid(float x) {
	return 2./(2. + exp2(-x)) - 1.;
}

float smoothcircle(vec2 uv, vec2 center, vec2 aspect, float radius, float sharpness){
	return 0.5 - sigmoid( ( length( (uv - center) * aspect) - radius) * sharpness) * 0.5;
}

float lum(vec3 color){
	return dot(vec3(0.30, 0.59, 0.11), color);
}

vec2 spiralzoom(vec2 domain, vec2 center, float n, float spiral_factor, float zoom_factor, vec2 pos){
	vec2 uv = domain - center;
	float angle = atan(uv.y, uv.x);
	float d = length(uv);
	return vec2( angle*n*pi2_inv + log(d)*spiral_factor*0., -log(d)*zoom_factor + 0.*n*angle*pi2_inv) + pos;
}

vec2 mobius(vec2 domain, vec2 zero_pos, vec2 asymptote_pos){
	return complex_div( domain - zero_pos, domain - asymptote_pos);
}

const float Pi = 3.14159;
const int zoom = 20;
const float speed = 0.05;
float fScale = 1.0;

void main(void)
{
		// domain map
	vec2 uv = gl_FragCoord.xy / RENDERSIZE.xy;
	
	// aspect-ratio correction
	vec2 aspect = vec2(1.,RENDERSIZE.y/RENDERSIZE.x);
	vec2 uv_correct = 0.5 + (uv -0.5)/ aspect.yx;
	vec2 mouse_correct = 0.5 + ( mouse.xy / RENDERSIZE.xy - 0.5) / aspect.yx;
		
	float phase = TIME*0. + pi*2.;
	float dist = 1.;
	vec2 uv_bipolar = mobius(uv_correct, vec2(0.5 - dist*0.5, 0.5), vec2(0.5 + dist*0.5, 0.5));
	uv_bipolar = spiralzoom(uv_bipolar, vec2(0.), 8., 0., 0.9, mouse.yx*vec2(-1.,2.)*4. );
	uv_bipolar = vec2(-uv_bipolar.y,uv_bipolar.x); // 90° rotation 
	
	
	vec2 pos = 0.5 + (wrap_flip(uv_bipolar) - 0.5)*8.;
		float amnt = 0.5;
	float nd = 0.0;
	vec4 cbuff = vec4(0.0);

	for(float i=0.0; i < 8.0; i += 2.0)
	{
		nd =cos(3.14159 * i * pos.x + (i * 2.75 + cos(TIME) * 0.25) + TIME) * (pos.x - 0.5) + 0.5;
		amnt = 1.0 / abs(nd - pos.y) * 0.005; 
		
		cbuff += vec4(amnt, amnt * 0.2 , amnt * pos.y, 2.0)*4.;
	}
	
	for(float i=0.0; i<5.0;i++){
	nd =sin(3.14*0.8*pos.x + (i*0.2+sin(+TIME)*.8) + TIME)*0.4+0.1 + pos.x;
	amnt = 1.0/abs(nd-pos.y)*0.02; 
	
	cbuff += vec4(amnt, amnt*0.3 , amnt*pos.y, 081.0);
	}
	
	gl_FragColor=vec4(cbuff.rgb, 1.0);
}