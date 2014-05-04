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


float sphere(vec3 ray, vec3 dir, vec3 center, float radius)
{
 vec3 rc = ray-center;
 float c = dot(rc, rc) - (radius*radius);
 float b = dot(dir, rc);
 float d = b*b - c;
 float t = -b - sqrt(abs(d));
 float st = step(0.0, min(t,d));
 return mix(-1.0, t, st);
}

vec3 background(float t, vec3 rd)
{
 vec3 light = normalize(vec3(sin(t), 0.6, cos(t)));
 float sun = max(0.0, dot(rd, light));
 float sky = max(0.0, dot(rd, vec3(0.0, 1.0, 0.0)));
 float ground = max(0.0, -dot(rd, vec3(0.0, 1.0, 0.0)));
 return 
  (pow(sun, 256.0)+0.2*pow(sun, 2.0))*vec3(2.0, 1.6, 1.0) +
  pow(ground, 0.5)*vec3(0.4, 0.3, 0.2) +
  pow(sky, 1.0)*vec3(0.5, 0.6, 0.7);
}

void main(void)
{
 vec2 uv = (-1.0 + 2.0*gl_FragCoord.xy / RENDERSIZE.xy) * 
  vec2(RENDERSIZE.x/RENDERSIZE.y, 1.0);
 vec3 ro = vec3(mouse*4.0-2.0, -7.0);
 vec3 rd = normalize(vec3(uv, 2.0));
 vec3 p = vec3(0.0, 0.0, 0.0);
 float t = sphere(ro, rd, p, 1.0);
 vec3 nml = normalize(p - (ro+rd*t));
 vec3 bgCol = background(TIME, rd);
 rd = reflect(rd, nml);
 vec3 col = background(TIME, rd) * vec3(0.9, 0.8, 1.0);
 gl_FragColor = vec4( mix(bgCol, col, step(0.0, t)), 1.0 );
}