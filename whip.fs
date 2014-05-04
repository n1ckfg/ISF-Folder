/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "INPUTS": [
 {
 "NAME": "rate1",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 100.0
 },
 {
 "NAME": "rate2",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 100.0
 },
 {
 "NAME": "rate3",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 100.0
 },
 {
 "NAME": "size",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 100.0
 },
 {
 "NAME": "offset",
 "TYPE": "float",
 "MIN": 0.0,
 "MAX": 1.0
 },
 {
 "NAME": "speed1",
 "TYPE": "float",
 "MIN": -60.0,
 "MAX": 60.0
 },
 {
 "NAME": "speed2",
 "TYPE": "float",
 "MIN": -60.0,
 "MAX": 60.0
 },
 {
 "NAME": "speed3",
 "TYPE": "float",
 "MIN": -60.0,
 "MAX": 60.0
 }
  ]
}
*/


#ifdef GL_ES
precision mediump float;
#endif


void main( void ) {

	vec2 position = ( gl_FragCoord.xy / RENDERSIZE.xy );
	vec3 light = vec3(pow(1.0-abs(position.y+sin(position.x* size+TIME*speed1)/rate3-offset),rate3),
                      pow(1.0-abs(position.y-cos(position.x* size+TIME*speed2)/rate3-offset),rate1),
                      pow(1.0-abs(position.y-sin(position.x* size+TIME*speed3)/rate3-offset),rate2));
	light += pow(light.r+light.g+light.b,1.0);
	gl_FragColor = vec4(light.r,light.g,light.b, 1.);
}