/*{
	"CREDIT": "by VIDVOX",
	"CATEGORIES": [
		"Color Effect"
	],
	"INPUTS": [
		{
			"NAME": "inputImage",
			"TYPE": "image"
		},
		{
			"NAME": "mask_color",
			"TYPE": "color",
			"DEFAULT": [
				0.24,
				0.77,
				0.38,
				1.0
			]
		},
		{
			"NAME": "hueTolerance",
			"TYPE": "float",
			"MIN": 0.0,
			"MAX": 0.25,
			"DEFAULT": 0.1
		},
		{
			"NAME": "satTolerance",
			"TYPE": "float",
			"MIN": 0.0,
			"MAX": 0.5,
			"DEFAULT": 0.5
		},
		{
			"NAME": "valueTolerance",
			"TYPE": "float",
			"MIN": 0.0,
			"MAX": 0.5,
			"DEFAULT": 0.5
		},
		{
			"NAME": "maskCutoff",
			"TYPE": "float",
			"MIN": 0.0,
			"MAX": 1.0,
			"DEFAULT": 0.5
		},
		{
			"NAME": "dilate",
			"TYPE": "float",
			"MIN": 0.0,
			"MAX": 6.0,
			"DEFAULT": 2.0
		},
		{
			"NAME": "blur",
			"TYPE": "float",
			"MIN": 0.0,
			"MAX": 6.0,
			"DEFAULT": 2.0
		}
	],
	"PASSES": [
		{
			"TARGET": "alphaPass"
		},
		{
			"TARGET": "horizDilate"
		},
		{
			"TARGET": "vertDilate"
		},
		{
			"TARGET": "horizBlur"
		},
		{
			"TARGET": "vertBlur"
		}
	]
}*/



vec3 rgb2hsv(vec3 c);
vec3 hsv2rgb(vec3 c);

void main() {
	//	generally speaking, sample a bunch of pixels, for each sample convert color val to HSV, if luma is greater than max luma, that's the new color
	vec2		tmpCoord;
	float		maxAlpha;
	vec4		sampleColorRGB;
	vec4		src_rgb;
	
	if (PASSINDEX==0)	{
		//	get the src pixel's color (as RGB), convert to HSV values
		src_rgb = IMG_PIXEL(inputImage, gl_FragCoord.xy);
		vec3		src_hsl = rgb2hsv(src_rgb.rgb);
		//	conver the passed color (which is RGB) to HSV values
		vec3		color_hsl = rgb2hsv(mask_color.rgb);
		
		//	the goal is to calculate a new float value for the alpha channel
		vec3		absDeltas = abs(src_hsl-color_hsl);
		if (absDeltas.r<=hueTolerance && absDeltas.g<=satTolerance && absDeltas.b<=valueTolerance)	{
			float		maxNormDelta = max(max(absDeltas.r/0.25, absDeltas.g/0.5), absDeltas.b/0.5);
			maxNormDelta = smoothstep(maskCutoff, 1.0, maxNormDelta);
			src_rgb.a = maxNormDelta;
		}
		else
			src_rgb.a = 1.0;
		
		
		gl_FragColor = src_rgb;
		//gl_FragColor = vec4(src_rgb.a, src_rgb.a, src_rgb.a, 1.0);
	}
	
	else if (PASSINDEX==1)	{
		//	'dilate' samples on either side + the source pixel = 2*dilate+1 total samples
		for (float i=0.; i<=float(int(dilate)); ++i)	{
			tmpCoord = vec2(clamp(gl_FragCoord.x+i,0.,RENDERSIZE.x), gl_FragCoord.y);
			sampleColorRGB = IMG_PIXEL(alphaPass, tmpCoord);
			//	if this is the first sample for this fragment, don't bother comparing- just set the max luma stuff
			if (i == 0.)	{
				maxAlpha = sampleColorRGB.a;
				src_rgb = sampleColorRGB;
			}
			//	else this isn't the first sample...
			else	{
				//	compare, determine if it's the max luma
				if (sampleColorRGB.a < maxAlpha)	{
					maxAlpha = sampleColorRGB.a;
				}
				//	do another sample for the negative coordinate
				tmpCoord = vec2(clamp(gl_FragCoord.x-i,0.,RENDERSIZE.x), gl_FragCoord.y);
				sampleColorRGB = IMG_PIXEL(alphaPass, tmpCoord);
				if (sampleColorRGB.a < maxAlpha)	{
					maxAlpha = sampleColorRGB.a;
				}
			}
		}
		gl_FragColor = vec4(src_rgb.rgb, maxAlpha);
	}
	else if (PASSINDEX==2)	{
		//	'dilate' samples up and down + the source pixel = 2*dilate+1 total samples
		for (float i=0.; i<=float(int(dilate)); ++i)	{
			tmpCoord = vec2(gl_FragCoord.x, clamp(gl_FragCoord.y+i,0.,RENDERSIZE.y));
			sampleColorRGB = IMG_PIXEL(horizDilate, tmpCoord);
			//	if this is the first sample for this fragment, don't bother comparing- just set the max luma stuff
			if (i == 0.)	{
				maxAlpha = sampleColorRGB.a;
				src_rgb = sampleColorRGB;
			}
			//	else this isn't the first sample...
			else	{
				//	compare, determine if it's the max luma
				if (sampleColorRGB.a < maxAlpha)	{
					maxAlpha = sampleColorRGB.a;
				}
				//	do another sample for the negative coordinate
				tmpCoord = vec2(gl_FragCoord.x, clamp(gl_FragCoord.y-i,0.,RENDERSIZE.y));
				sampleColorRGB = IMG_PIXEL(horizDilate, tmpCoord);
				if (sampleColorRGB.a < maxAlpha)	{
					maxAlpha = sampleColorRGB.a;
				}
			}
		}
		gl_FragColor = vec4(src_rgb.rgb, maxAlpha);
	}
	else if (PASSINDEX==3)	{
		float		tmpRadius = float(int(blur));
		vec4		tmpColor;
		vec4		srcColor;
		float		totalAlpha = 0.0;
		for (float i=-1.*tmpRadius; i<=tmpRadius; ++i)	{
			tmpColor = IMG_PIXEL(vertDilate, gl_FragCoord.xy+vec2(i,0.0));
			totalAlpha += tmpColor.a;
			if (i==0.0)
				srcColor = tmpColor;
		}
		totalAlpha /= ((tmpRadius*2.)+1.);
		gl_FragColor = vec4(srcColor.r, srcColor.g, srcColor.b, totalAlpha);
	}
	else if (PASSINDEX==4)	{
		float		tmpRadius = float(int(blur));
		vec4		tmpColor;
		vec4		srcColor;
		float		totalAlpha = 0.0;
		for (float i=-1.*tmpRadius; i<=tmpRadius; ++i)	{
			tmpColor = IMG_PIXEL(horizBlur, gl_FragCoord.xy+vec2(0.0,i));
			totalAlpha += tmpColor.a;
			if (i==0.0)
				srcColor = tmpColor;
		}
		totalAlpha /= ((tmpRadius*2.)+1.);
		gl_FragColor = vec4(srcColor.r, srcColor.g, srcColor.b, totalAlpha);
	}
}




vec3 rgb2hsv(vec3 c)	{
	vec4 K = vec4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
	//vec4 p = mix(vec4(c.bg, K.wz), vec4(c.gb, K.xy), step(c.b, c.g));
	//vec4 q = mix(vec4(p.xyw, c.r), vec4(c.r, p.yzx), step(p.x, c.r));
	vec4 p = c.g < c.b ? vec4(c.bg, K.wz) : vec4(c.gb, K.xy);
	vec4 q = c.r < p.x ? vec4(p.xyw, c.r) : vec4(c.r, p.yzx);
	
	float d = q.x - min(q.w, q.y);
	float e = 1.0e-10;
	return vec3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

vec3 hsv2rgb(vec3 c)	{
	vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
	vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
	return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}