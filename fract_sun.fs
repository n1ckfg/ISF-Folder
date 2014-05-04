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


// Fractal Soup - @P_Malin

vec2 CircleInversion(vec2 vPos, vec2 vOrigin, float fRadius)
{
	vec2 vOP = vPos - vOrigin;

	vOrigin = vOrigin - vOP * fRadius * fRadius / dot(vOP, vOP);


        vOrigin.x += sin(vOrigin.x * 0.001) / cos(vOrigin.y * 0.001);
        vOrigin.y += sin(vOrigin.x * 0.001) * cos(vOrigin.y * 0.001);

        return vOrigin;
}

float Parabola( float x, float n )
{
	return pow( 3.0*x*(1.0-x), n );
}

void main(void)
{
	vec2 vPos = gl_FragCoord.xy / RENDERSIZE.xy;
	vPos = vPos - 0.5;

	vPos.x *= RENDERSIZE.x / RENDERSIZE.y;

	vec2 vScale = vec2(1.2);
	vec2 vOffset = vec2( sin(TIME * 0.123), atan(TIME * 0.0567));

	float l = 0.0;
	float minl = 10000.0;

	for(int i=0; i<48; i++)
	{
		vPos.x = abs(vPos.x);
		vPos = vPos * vScale + vOffset;

		vPos = CircleInversion(vPos, vec2(0.5, 0.5), 0.9);

		l = length(vPos*vPos);
		minl = min(l, minl);
	}


	float t = 2.1 + TIME * 0.035;
	vec3 vBaseColour = normalize(vec3(sin(t * 1.790), sin(t * 1.345), sin(t * 1.123)) * 0.5 + 0.5);

	//vBaseColour = vec3(1.0, 0.15, 0.05);

	float fBrightness = 11.0;

	vec3 vColour = vBaseColour * l * l * fBrightness;

	minl = Parabola(minl, 5.0);

	vColour *= minl + 0.14;

	vColour = 1.0 - exp(-vColour);
	gl_FragColor = vec4(vColour,1.0);
}