/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "INPUTS": [
    
  ]
}
*/



precision mediump float;


void main(void) {
	float intensity = 10.0; // Lower number = more 'glow'
	vec2 offset = vec2(-0.5 , 0); // x / y offset
	vec3 light_color = vec3(0.9, 0.5, 0.1); // RGB, proportional values, higher increases intensity
	float master_scale = 0.21; // Change the size of the effect
	float c = master_scale/(length(vv_FragNormCoord+offset));
	
	gl_FragColor = smoothstep(0.95,1.05,c) * vec4(1.0) + vec4(vec3(c) * light_color, 1.0);
}