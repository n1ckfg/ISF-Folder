/*
 {
 "CREDIT": "by thedantheman",
 "DESCRIPTION": "",
 "CATEGORIES": [
 "Automatically Converted"
 ],
 "INPUTS": [
 {
 "NAME": "offsetX",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 1.0,
 "DEFAULT": 0.5
 },
 {
 "NAME": "offsetY",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 1.0,
 "DEFAULT": 0.5
 },
 {
 "NAME": "master_scale",
 "TYPE": "float",
 "MIN": 0.1,
 "MAX": 1000.0,
 "DEFAULT": 10.5
 },
 {
 "NAME": "size",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 1.0,
 "DEFAULT": 0.5
 },
 {
 "NAME": "glow",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 1.0,
 "DEFAULT": 0.5
 },
 {
 "NAME": "colorB",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 1.0,
 "DEFAULT": 0.1
 },
 {
 "NAME": "colorG",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 1.0,
 "DEFAULT": 0.1
 }
 ,
 {
 "NAME": "colorR",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 1.0,
 "DEFAULT": 0.1
 }
 ,
 {
 "NAME": "MIN",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 1.0,
 "DEFAULT": 0.01
 }
 ,
 {
 "NAME": "MAX",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 1.0,
 "DEFAULT": 0.1
 },
 {
 "NAME": "scale",
 "TYPE": "float",
 "MIN": 3,
 "MAX": 20.0,
 "DEFAULT": 4.0
 },
 {
 "NAME": "depthScale",
 "TYPE": "float",
 "MIN": 0.1,
 "MAX": 1.0,
 "DEFAULT": 0.2
 }
 ]
 }
 */

#ifdef GL_ES
precision mediump float;
#endif


float pulse(float cn, float wi, float x)
{
	return 1.-smoothstep(0., wi, abs(x-cn));
}

float hash11(float n)
{
    return fract(sin(n)*43758.5453);
}

vec2 hash22(vec2 p)
{
    p = vec2( dot(p,vec2(127.1, 311.7)), dot(p,vec2(269.5, 183.3)));
	return fract(sin(p)*43758.5453);
}

vec2 field(in vec2 p)
{
	vec2 n = floor(p);
	vec2 f = fract(p);
	vec2 m = vec2(1.);
	vec2 o = hash22(n)*0.17;
	vec2 r = f+o-0.5;
	float d = abs(r.x) + abs(r.y);
	if(d<m.x)
    {
		m.x = d;
		m.y = hash11(dot(n,vec2(1., 2.)));
	}
	return vec2(m.x,m.y);
}

void main(void)
{
	vec2 uv = gl_FragCoord.xy / RENDERSIZE.x-offsetY;
	uv.x *= RENDERSIZE.x/RENDERSIZE.y*0.9;
	uv *= scale;
	
	vec2 p = uv*.01;
	p *= 1./(p-1.);
	
	//global movement
	uv.y += TIME*MIN;
	uv.x += sin(TIME*0.1)*MAX;
	vec2 buv = uv;
	
	float rz = 0.;
	vec3 col = vec3(0.0);
	for(float i=1.; i<=26.; i++)
	{
		vec2 rn = field(uv);
		uv -= p*(i-25.)*depthScale;
		rn.x = pulse(size/2.0, glow/2., rn.x+rn.y*0.17);
		col += rn.x*vec3(cos(rn.y*100.)*colorR, cos(rn.y)*colorG,sin(rn.y)*colorB);
	}
	
	//animated grid
	buv*= mat2(0.707,-0.707,0.707,0.707);
	float rz2 = .4*(sin(buv*10.+1.).x*40.-39.5)*(sin(uv.x*10.)*0.5+0.5);
	vec3 col2 = vec3(0.2,0.4,2.)*rz2*(sin(2.+TIME*0.1+(uv.y*2.+uv.x*10.))*0.5+0.5);
	float rz3 = .3*(sin(buv*10.+4.).y*40.-39.5)*(sin(uv.x*10.)*0.5+0.5);
	vec3 col3 = vec3(1.9,0.4,2.)*rz3*(sin(TIME*0.2-(uv.y*10.+uv.x*2.))*0.5+0.5);
	
	col = max(max(col,col2),col3);
	
	gl_FragColor = vec4(col,1.0);
}