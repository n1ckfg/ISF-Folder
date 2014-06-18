/*{
	"CREDIT": "by Carter Rosenberg",
	"CATEGORIES": [
		"Generator"
	],
	"INPUTS": [
		{
			"NAME": "radius1",
			"TYPE": "float",
			"DEFAULT": 0.1
		},
		{
			"NAME": "radius2",
			"TYPE": "float",
			"DEFAULT": 0.25
		},
		{
			"NAME": "startColor",
			"TYPE": "color",
			"DEFAULT": [
				1.0,
				0.75,
				0.0,
				1.0
			]
		},
		{
			"NAME": "endColor",
			"TYPE": "color",
			"DEFAULT": [
				0.0,
				0.25,
				0.75,
				1.0
			]
		},
		{
			"NAME": "location",
			"TYPE": "point2D",
			"DEFAULT": [
				0.5,
				0.5
			]
		}
	]
}*/


float distance (vec2 start, vec2 end)	{
	return sqrt(pow((start.x-end.x),2.0) + pow((start.y-end.y),2.0));
}


void main() {
	vec2 tmpPt = 2.0 * location / RENDERSIZE;
	float mixOffset = distance(tmpPt, vv_FragNormCoord);
	float tmpRadius = radius1 + radius2;
	if (mixOffset < radius1)	{
		gl_FragColor = startColor;
	}
	else if (mixOffset > tmpRadius)	{
		gl_FragColor = endColor;
	}
	else if (radius1 == radius2)	{
		gl_FragColor = endColor;
	}
	else	{
		gl_FragColor = mix(startColor,endColor,(mixOffset-radius1)/(tmpRadius-radius1));
	}
}