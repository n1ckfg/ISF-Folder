/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "INPUTS": [
    
  ]
}
*/


// Shader by Nicolas Robert [NRX]
// Latest version: http://glsl.heroku.com/e#15129.4
// Forked from: http://glsl.heroku.com/e#15072.3 ([NRX:] this version has issues on some GPU...)

#ifdef GL_ES
precision mediump float;
#endif


#define DELTA		0.01
#define RAY_LENGTH_MAX	150.0
#define RAY_STEP_MAX	200
#define OBJ_COUNT	4
#define M_PI		3.1415926535897932384626433832795

//#define BASIC

int debugCounter1;
int debugCounter2;

mat3 mRotate (in vec3 angle) {
	float c = cos (angle.x);
	float s = sin (angle.x);
	mat3 rx = mat3 (1.0, 0.0, 0.0, 0.0, c, s, 0.0, -s, c);

	c = cos (angle.y);
	s = sin (angle.y);
	mat3 ry = mat3 (c, 0.0, -s, 0.0, 1.0, 0.0, s, 0.0, c);

	c = cos (angle.z);
	s = sin (angle.z);
	mat3 rz = mat3 (c, s, 0.0, -s, c, 0.0, 0.0, 0.0, 1.0);

	return rz * ry * rx;
}

vec3 vRotateX (in vec3 p, in float angle) {
	float c = cos (angle);
	float s = sin (angle);
	return vec3 (p.x, c * p.y + s * p.z, c * p.z - s * p.y);
}

vec3 vRotateY (in vec3 p, in float angle) {
	float c = cos (angle);
	float s = sin (angle);
	return vec3 (c * p.x - s * p.z, p.y, c * p.z + s * p.x);
}

vec3 vRotateZ (in vec3 p, in float angle) {
	float c = cos (angle);
	float s = sin (angle);
	return vec3 (c * p.x + s * p.y, c * p.y - s * p.x, p.z);
}

float sphere (in vec3 p, in float r) {
	return length (p) - r;
}

float box (in vec3 p, in vec3 b, in float r) {
	vec3 d = abs (p) - b + r;
	return min (max (d.x, max (d.y, d.z)), 0.0) + length (max (d, 0.0)) - r;
}

float plane (in vec3 p, in vec3 n, in float d) {
	return dot (p, normalize (n)) + d;
}

float planeZ (in vec3 p) {
	return p.z;
}

float torusX (in vec3 p, in float r1, in float r2) {
	vec2 q = vec2 (length (p.yz) - r1, p.x);
	return length (q) - r2;
}

float torusY (in vec3 p, in float r1, in float r2) {
	vec2 q = vec2 (length (p.xz) - r1, p.y);
	return length (q) - r2;
}

float torusZ (in vec3 p, in float r1, in float r2) {
	vec2 q = vec2 (length (p.xy) - r1, p.z);
	return length (q) - r2;
}

float cylinderX (in vec3 p, in float r) {
 	return length (p.yz) - r;
}

float cylinderY (in vec3 p, in float r) {
 	return length (p.xz) - r;
}

float cylinderZ (in vec3 p, in float r) {
 	return length (p.xy) - r;
}

vec3 twistX (in vec3 p, in float k, in float angle) {
	return vRotateX (p, angle + k * p.x);
}

vec3 twistY (in vec3 p, in float k, in float angle) {
	return vRotateY (p, angle + k * p.y);
}

vec3 twistZ (in vec3 p, in float k, in float angle) {
	return vRotateZ (p, angle + k * p.z);
}

vec3 repeat (in vec3 p, in vec3 k) {
	if (k.x > 0.0) {
		p.x = mod (p.x, k.x) - 0.5 * k.x;
	}
	if (k.y > 0.0) {
		p.y = mod (p.y, k.y) - 0.5 * k.y;
	}
	if (k.z > 0.0) {
		p.z = mod (p.z, k.z) - 0.5 * k.z;
	}
	return p;
}

float fixDistance (in float d, in float correction, in float k) {
	correction = max (correction, 0.0);
	k = clamp (k, 0.0, 1.0);
	return min (d, max ((d - DELTA) * k + DELTA, d - correction));
}

float getDistance (in vec3 p, in int objectIndex) {
	++debugCounter2;

	p += vec3 (2.0 * sin (p.z * 0.2 + TIME * 2.0), sin (p.z * 0.1 + TIME), 0.0);
	if (objectIndex == 0) {
		float a = atan (p.y, p.x) * 6.0;
		return fixDistance (-cylinderZ (p, 4.0) + 0.5 * sin (a) * sin (p.z), 0.2, 0.8);
	}
	if (objectIndex == 1) {
		p = twistZ (repeat (p, vec3 (5.0, 5.0, 12.0)), 1.0, TIME);
		return fixDistance (box (p, vec3 (0.6, 0.6, 1.5), 0.3), 0.2, 0.8);
	}
	if (objectIndex == 2) {
		p.z += 12.0;
		p = repeat (vRotateZ (p, sin (TIME * 4.0)), vec3 (4.0, 4.0, 24.0));
		return sphere (p, 0.7);
	}
	if (objectIndex == 3) {
		p.z += 12.0;
		p = repeat (p, vec3 (0.0, 0.0, 24.0));
		return fixDistance (torusZ (p, 3.5, 0.5), 0.2, 0.8);
	}
	return RAY_LENGTH_MAX;
}

vec3 getNormal (in vec3 p, in int objectIndex) {
	vec2 h = vec2 (DELTA, 0.0);
	return normalize (vec3 (
		getDistance (p + h.xyy, objectIndex) - getDistance (p - h.xyy, objectIndex),
		getDistance (p + h.yxy, objectIndex) - getDistance (p - h.yxy, objectIndex),
		getDistance (p + h.yyx, objectIndex) - getDistance (p - h.yyx, objectIndex)
	));
}

#ifdef BASIC
int draw (in vec3 p, inout float rayIncrement) {
	int closestObjectIndex = -1;
	rayIncrement = RAY_LENGTH_MAX;
	for (int objectIndex = 0; objectIndex < OBJ_COUNT; ++objectIndex) {
		float dist = getDistance (p, objectIndex);
		if (dist < rayIncrement) {
			rayIncrement = dist;
			closestObjectIndex = objectIndex;
		}
	}
	return closestObjectIndex;
}
#else
float distObject [OBJ_COUNT];

int draw (in vec3 p, inout float rayIncrement, inout float minDist1) {
	int closestObjectIndex = -1;
	float minDist2 = RAY_LENGTH_MAX;
	for (int objectIndex = 0; objectIndex < OBJ_COUNT; ++objectIndex) {
		float dist = distObject [objectIndex];
		dist -= rayIncrement;
		if (dist < minDist1) {
			dist = getDistance (p, objectIndex);
			if (dist < minDist1) {
				minDist2 = minDist1;
				minDist1 = dist;
				closestObjectIndex = objectIndex;
			}
			else if (dist < minDist2) {
				minDist2 = dist;
			}
		}
		distObject [objectIndex] = dist;
	}
	rayIncrement = minDist1;
	minDist1 = minDist2;
	return closestObjectIndex;
}
#endif

vec3 rayMarch (in vec3 origin, in vec3 direction) {
	#ifndef BASIC
	for (int objectIndex = 0; objectIndex < OBJ_COUNT; ++objectIndex) {
		distObject [objectIndex] = 0.0;
	}
	float minDist = RAY_LENGTH_MAX;
	#endif

	vec3 p = origin;
	float rayLength = 0.0;
	float rayIncrement = RAY_LENGTH_MAX;
	int closestObjectIndex = -1;
	for (int rayStep = 0; rayStep < RAY_STEP_MAX; ++rayStep) {
		++debugCounter1;

		#ifdef BASIC
		closestObjectIndex = draw (p, rayIncrement);
		#else
		closestObjectIndex = draw (p, rayIncrement, minDist);
		#endif
		rayLength += rayIncrement;
		p = origin + direction * rayLength;
		if (rayIncrement < DELTA || rayLength > RAY_LENGTH_MAX) {
			break;
		}
	}

	vec3 color;
	if (closestObjectIndex < 0 || rayIncrement >= DELTA) {
		color = vec3 (0.0);
	}
	else {
		vec3 n = getNormal (p, closestObjectIndex);
		color = vec3 (0.5 + 0.5 * sin (n.x * M_PI), 0.5 + 0.5 * sin (n.y * M_PI), 0.5 + 0.5 * sin (n.z * M_PI))
			* pow (1.0 - rayLength / RAY_LENGTH_MAX, 4.0)
			* max (0.5, 8.0 * sin (rayLength * 0.05 - TIME * 4.0) - 6.0);
	}
	return color;
}

void main () {
	debugCounter1 = 0;
	debugCounter2 = 0;

	vec2 p = (2.0 * gl_FragCoord.xy - RENDERSIZE.xy) / RENDERSIZE.y;
	vec3 direction = normalize (vec3 (p.x, p.y, 2.0));

	vec3 origin = vec3 (0.0, 0.0, TIME * 6.0);
	float angle = mod (TIME * 0.5, 8.0);
	if (angle > 1.0 && angle < 3.0) {
		angle = M_PI;
	}
	else if (angle > 4.0) {
		angle = 0.0;
	}
	else {
		angle = M_PI * (0.5 - 0.5 * cos (M_PI * angle));
	}
	direction = mRotate (vec3 (angle, 0.0, M_PI * sin (TIME) * sin (TIME * 0.2))) * direction;
	vec3 color = rayMarch (origin, direction);

	vec3 debugColor = vec3 (float (debugCounter1) / float (RAY_STEP_MAX / 2), float (debugCounter2) / float (OBJ_COUNT * RAY_STEP_MAX / 2), 0.0);
	gl_FragColor = vec4 (mix (color, debugColor, 0.5 + 0.5 * sin (TIME * 0.3)), 1.0);
}