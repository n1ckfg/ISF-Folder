/*{
	"CREDIT": "original implementation as v002.blur in QC by anton marini and tom butterworth, ported by zoidberg",
	"CATEGORIES": [
		"Blur"
	],
	"INPUTS": [
		{
			"NAME": "inputImage",
			"TYPE": "image"
		},
		{
			"NAME": "blurAmount",
			"TYPE": "float",
			"MIN": 0.0,
			"MAX": 10.0,
			"DEFAULT": 10.0
		}
	],
	"PASSES": [
		{
			"TARGET": "smallA",
			"WIDTH": "floor($WIDTH*min((0.2/($blurAmount/10.0)),1.0))",
			"HEIGHT": "floor($HEIGHT*min((0.2/($blurAmount/10.0)),1.0))"
		},
		{
			"TARGET": "smallB",
			"WIDTH": "floor($WIDTH*min((0.2/($blurAmount/10.0)),1.0))",
			"HEIGHT": "floor($HEIGHT*min((0.2/($blurAmount/10.0)),1.0))"
		},
		{
			"TARGET": "smallC",
			"WIDTH": "floor($WIDTH*min((0.3/($blurAmount/10.0)),1.0))",
			"HEIGHT": "floor($HEIGHT*min((0.3/($blurAmount/10.0)),1.0))"
		},
		{
			"TARGET": "smallD",
			"WIDTH": "floor($WIDTH*min((0.3/($blurAmount/10.0)),1.0))",
			"HEIGHT": "floor($HEIGHT*min((0.3/($blurAmount/10.0)),1.0))"
		},
		{
			"TARGET": "smallE",
			"WIDTH": "floor($WIDTH*min((0.5/($blurAmount/10.0)),1.0))",
			"HEIGHT": "floor($HEIGHT*min((0.5/($blurAmount/10.0)),1.0))"
		},
		{
			"TARGET": "smallF",
			"WIDTH": "floor($WIDTH*min((0.5/($blurAmount/10.0)),1.0))",
			"HEIGHT": "floor($HEIGHT*min((0.5/($blurAmount/10.0)),1.0))"
		},
		{
			"TARGET": "smallG",
			"WIDTH": "floor($WIDTH*min((0.8/($blurAmount/10.0)),1.0))",
			"HEIGHT": "floor($HEIGHT*min((0.8/($blurAmount/10.0)),1.0))"
		},
		{
			"TARGET": "smallH",
			"WIDTH": "floor($WIDTH*min((0.8/($blurAmount/10.0)),1.0))",
			"HEIGHT": "floor($HEIGHT*min((0.8/($blurAmount/10.0)),1.0))"
		},
		{
			"TARGET": "smallI"
		},
		{
			
		}
	]
}*/


varying vec2		texOffsets[3];


void main() {
	vec4		sample0;
	vec4		sample1;
	vec4		sample2;
	if (PASSINDEX == 0)	{
		sample0 = IMG_NORM_PIXEL(inputImage,texOffsets[0]);
		sample1 = IMG_NORM_PIXEL(inputImage,texOffsets[1]);
		sample2 = IMG_NORM_PIXEL(inputImage,texOffsets[2]);
	}
	else if (PASSINDEX == 1)	{
		sample0 = IMG_NORM_PIXEL(smallA,texOffsets[0]);
		sample1 = IMG_NORM_PIXEL(smallA,texOffsets[1]);
		sample2 = IMG_NORM_PIXEL(smallA,texOffsets[2]);
	}
	else if (PASSINDEX == 2)	{
		sample0 = IMG_NORM_PIXEL(smallB,texOffsets[0]);
		sample1 = IMG_NORM_PIXEL(smallB,texOffsets[1]);
		sample2 = IMG_NORM_PIXEL(smallB,texOffsets[2]);
	}
	else if (PASSINDEX == 3)	{
		sample0 = IMG_NORM_PIXEL(smallC,texOffsets[0]);
		sample1 = IMG_NORM_PIXEL(smallC,texOffsets[1]);
		sample2 = IMG_NORM_PIXEL(smallC,texOffsets[2]);
	}
	else if (PASSINDEX == 4)	{
		sample0 = IMG_NORM_PIXEL(smallD,texOffsets[0]);
		sample1 = IMG_NORM_PIXEL(smallD,texOffsets[1]);
		sample2 = IMG_NORM_PIXEL(smallD,texOffsets[2]);
	}
	else if (PASSINDEX == 5)	{
		sample0 = IMG_NORM_PIXEL(smallE,texOffsets[0]);
		sample1 = IMG_NORM_PIXEL(smallE,texOffsets[1]);
		sample2 = IMG_NORM_PIXEL(smallE,texOffsets[2]);
	}
	else if (PASSINDEX == 6)	{
		sample0 = IMG_NORM_PIXEL(smallF,texOffsets[0]);
		sample1 = IMG_NORM_PIXEL(smallF,texOffsets[1]);
		sample2 = IMG_NORM_PIXEL(smallF,texOffsets[2]);
	}
	else if (PASSINDEX == 7)	{
		sample0 = IMG_NORM_PIXEL(smallG,texOffsets[0]);
		sample1 = IMG_NORM_PIXEL(smallG,texOffsets[1]);
		sample2 = IMG_NORM_PIXEL(smallG,texOffsets[2]);
	}
	else if (PASSINDEX == 8)	{
		sample0 = IMG_NORM_PIXEL(smallH,texOffsets[0]);
		sample1 = IMG_NORM_PIXEL(smallH,texOffsets[1]);
		sample2 = IMG_NORM_PIXEL(smallH,texOffsets[2]);
	}
	else if (PASSINDEX == 9)	{
		sample0 = IMG_NORM_PIXEL(smallI,texOffsets[0]);
		sample1 = IMG_NORM_PIXEL(smallI,texOffsets[1]);
		sample2 = IMG_NORM_PIXEL(smallI,texOffsets[2]);
	}
	else	{
		sample0 = vec4(1,0,0,1);
		sample1 = vec4(1,0,0,1);
		sample2 = vec4(1,0,0,1);
	}
	gl_FragColor =  vec4((sample0 + sample1 + sample2).rgb / (3.0), 1.0);
}
