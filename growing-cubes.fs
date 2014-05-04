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
precision mediump float;
#endif


struct Ray
{
	vec3 Origin;
	vec3 Direction;
};

bool ShouldDraw(vec3 voxel)
{
	return length(voxel + vec3(0.5)) <= (8.0+2.*sin(length(voxel)*0.8+TIME));
}

void IterateVoxel(inout vec3 voxel, Ray ray, out vec3 hitPoint)
{
	vec3 maxA = (voxel + step(vec3(0), ray.Direction) - ray.Origin) / ray.Direction;
	hitPoint = step(maxA,vec3(min(min(maxA.x,maxA.y),maxA.z)));
	voxel += hitPoint*sign(ray.Direction);
}
	
vec4 GetRayColor(Ray ray)
{
	vec3 voxel = ray.Origin - fract(ray.Origin);
	vec3 hitPoint;
	vec4 result = vec4(1.0, 1.0, 1.0, 1.0);
	
	// stupid amd related bug needs this workaround
	bool done = false;
	
	for(int i=0;i<48/*CAREFUL WITH THIS!!!*/;i++)
	{
		if(ShouldDraw(voxel) && !done)
		{
			done = true;
			result = vec4(vec3(0.5, 0.5, 0.5)*dot(hitPoint, vec3(1.7, 1.1, 1.3)), 1.);
			// break; // <-- uncomment this line and see amd driver fail horribly
		}
		
		IterateVoxel(voxel, ray, hitPoint);
	}

	return result;
}

void GetCameraRay(const in vec3 position, const in vec3 lookAt, out Ray currentRay)
{
	vec3 forwards = normalize(lookAt - position);
	vec3 worldUp = vec3(0.0, 1.0, 0.0);
	
	
	vec2 uV = ( gl_FragCoord.xy / RENDERSIZE.xy );
	vec2 viewCoord = uV * 2.0 - 1.0;
	
	float ratio = RENDERSIZE.x / RENDERSIZE.y;
	
	viewCoord.y /= ratio;                              
	
	currentRay.Origin = position;
	
	vec3 right = normalize(cross(forwards, worldUp));
	vec3 up = cross(right, forwards);
	       
	currentRay.Direction = normalize( right * viewCoord.x + up * viewCoord.y + forwards);
}

void main( void ) 
{
	Ray currentRay;
 
	GetCameraRay(vec3(20.0*sin(TIME*0.1), 16.*sin((mouse.y-0.5)*3.14), 18.0*cos(TIME*0.1)), vec3(0.0, 0.0, 0.0), currentRay);
	gl_FragColor = GetRayColor(currentRay);
}