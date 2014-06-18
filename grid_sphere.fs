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



float sdSphere( vec3 p, float r )
{
  return length(p) - r;
}

float sdSphere( vec2 p, float r )
{
  return length(p) - r;
}

float sdBox( vec3 p, vec3 b )
{
  vec3 d = abs(p) - b;
  return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0));
}

float sdBox( vec2 p, vec2 b )
{
  vec2 d = abs(p) - b;
  return min(max(d.x,d.y),0.0) + length(max(d,0.0));
}


vec3 nrand3( vec2 co )
{
	vec3 a = fract( cos( co.x*8.3e-3 + co.y )*vec3(1.3e5, 4.7e5, 2.9e5) );
	vec3 b = fract( sin( co.x*0.3e-3 + co.y )*vec3(8.1e5, 1.0e5, 0.1e5) );
	vec3 c = mix(a, b, 0.5);
	return c;
}

float map(vec3 p)
{
    float h = 1.8;
    float rh = 0.5;
    float grid = 2.0;
    float grid_half = grid*0.5;
    float cube = 0.75;
    vec3 orig = p;

    vec3 g1 = vec3(ceil((orig.x)/grid), ceil((orig.y)/grid), ceil((orig.z)/grid));
    vec3 rxz =  nrand3(g1.xz);
    vec3 ryz =  nrand3(g1.yz);

    vec3 di = ceil(p/4.8);
    p = abs(p+vec3(0,TIME*3.0,0));
    float d1 = p.y + h;
    float d2 = p.x + h;

    vec3 p1 = mod(p, vec3(grid)) - vec3(grid_half);
    float c1 = sdBox(p1,vec3(cube));

    vec3 sc = orig + vec3(sin(TIME*0.5)*5.0, sin(TIME*0.75)*3.0, 15.0+cos(TIME*0.5)*3.0);
    float s1 = sdSphere(sc, 5.0+sin(TIME*2.0)*1.0 );
    float s2 = sdSphere(sc, 4.0 );
	float sphere = min(max(c1,s1),s2);
	
	float s3 = sdSphere(orig, 20.0 );
	float s4 = sdSphere(orig, 50.0 );
	float back = max(c1, max(s4,-s3));
	
	return min(sphere, back);
}



vec3 genNormal(vec3 p)
{
    const float d = 0.01;
    return normalize( vec3(
        map(p+vec3(  d,0.0,0.0))-map(p+vec3( -d,0.0,0.0)),
        map(p+vec3(0.0,  d,0.0))-map(p+vec3(0.0, -d,0.0)),
        map(p+vec3(0.0,0.0,  d))-map(p+vec3(0.0,0.0, -d)) ));
}

void main()
{
    vec2 pos = (gl_FragCoord.xy*2.0 - RENDERSIZE.xy) / RENDERSIZE.y;
    vec3 camPos = vec3(-0.5,0.0,3.0);
    vec3 camDir = normalize(vec3(0.3, 0.0, -1.0));
    //camPos -=  vec3(0.0,0.0,TIME*3.0);
    vec3 camUp  = normalize(vec3(0.5, 1.0, 0.0));
    vec3 camSide = cross(camDir, camUp);
    float focus = 1.8;

    vec3 rayDir = normalize(camSide*pos.x + camUp*pos.y + camDir*focus);	    
    vec3 ray = camPos;
    int march = 0;
    float d = 0.0;

    float total_d = 0.0;
    const int MAX_MARCH = 64;
    const float MAX_DIST = 100.0;
    for(int mi=0; mi<MAX_MARCH; ++mi) {
        d = map(ray);
        march=mi;
        total_d += d;
        ray += rayDir * d;
        if(d<0.001) {break; }
        if(total_d>MAX_DIST) {
            total_d = MAX_DIST;
            march = MAX_MARCH-1;
            break;
        }
    }
	
    float glow = 0.0;
    {
        const float s = 0.0075;
        vec3 p = ray;
        vec3 n1 = genNormal(ray);
        vec3 n2 = genNormal(ray+vec3(s, 0.0, 0.0));
        vec3 n3 = genNormal(ray+vec3(0.0, s, 0.0));
        glow = (1.0-abs(dot(camDir, n1)))*0.5;
        if(dot(n1, n2)<0.8 || dot(n1, n3)<0.8) {
            glow += 0.6;
        }
    }
    {
	vec3 p = ray;
        float grid1 = max(0.0, max((mod((p.x+p.y+p.z*2.0)-TIME*3.0, 5.0)-4.0)*1.5, 0.0) );
        float grid2 = max(0.0, max((mod((p.x+p.y*2.0+p.z)-TIME*2.0, 7.0)-6.0)*1.2, 0.0) );
        vec3 gp1 = abs(mod(p, vec3(0.24)));
        vec3 gp2 = abs(mod(p, vec3(0.32)));
        if(gp1.x<0.23 && gp1.z<0.23) {
            grid1 = 0.0;
        }
        if(gp2.y<0.31 && gp2.z<0.31) {
            grid2 = 0.0;
        }
        glow += grid1+grid2;
    }

    float fog = min(1.0, (1.0 / float(MAX_MARCH)) * float(march))*1.0;
    vec3  fog2 = 0.01 * vec3(1, 1, 1.5) * total_d;
    glow *= min(1.0, 4.0-(4.0 / float(MAX_MARCH-1)) * float(march));
    gl_FragColor = vec4(vec3(0.15+glow*0.75, 0.15+glow*0.75, 0.2+glow)*fog + fog2, 1.0);
}