/*{
	"DESCRIPTION": "a simple sine wave generator",
	"CREDIT": "by carter rosenberg",
	"CATEGORIES": [
		"Generators"
	],
	"INPUTS": [
		{
			"NAME": "cycles",
			"TYPE": "float",
			"DEFAULT": 2.0,
			"MIN": 0.0,
			"MAX": 8.0
		},
		{
			"NAME": "offset",
			"TYPE": "float",
			"DEFAULT": 0.0,
			"MIN": 0.0,
			"MAX": 1.0
		},
		{
			"NAME": "frontColor",
			"TYPE": "color",
			"DEFAULT": [
				1.0,
				1.0,
				1.0,
				1.0
			]
		}
	]
}*/

void main()
{
	vec4		returnMe = vec4(0,0,0,0);
	float		sineOfX = 0.5+(sin((offset+cycles*vv_FragNormCoord.x)*6.283185))/2.0;
	if (vv_FragNormCoord.y<sineOfX)	{
		returnMe = frontColor;
	}
	gl_FragColor = returnMe;
}
