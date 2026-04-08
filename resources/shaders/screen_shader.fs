#version 330 core
out vec4 FragColor;

in vec2 TexCoords;
uniform sampler2D screenTexture;
uniform float time;
float changeSpeed = 2.0f;

int unit = 200;
vec2 vecs[4] = vec2[](
    vec2(-1.0, -1.0),
    vec2(-1.0, 1.0),
    vec2(1.0, 1.0),
    vec2(1.0, -1.0)
);
int mat[25] = int[](
    2, 0, 3, 0, 0,
    0, 3, 1, 3, 0,
    3, 2, 1, 3, 1,
    2, 1, 3, 1, 0,
    0, 0, 1, 2, 0
);
int getPos(int x, int y){
    return mat[y * 5 + x];
}
float blending(float t){
  return t*(t*(t*(10+t*(-15+6*t))));
}
float dot(vec2 a, vec2 b){
    return a.x * b.x + a.y * b.y;
}
vec2 rotate(vec2 v, float angle) {
    float s = sin(angle);
    float c = cos(angle);
    return vec2(
        v.x * c - v.y * s,
        v.x * s + v.y * c
    );
}

vec4 color1 = vec4(1.0, 0.6, 0.0, 1.0); // orange
vec4 color2 = vec4(0.1, 0.1, 0.1, 1.0); // black-orange

void main()
{   
    vec2 pos = vec2(gl_FragCoord.x, gl_FragCoord.y);

    int x = int(gl_FragCoord.x) / unit;
    int y = int(gl_FragCoord.y) / unit;

    vec2 uv = (pos - vec2(x * unit, y * unit)) / float(unit);

    int bl = getPos(x, y);
    int br = getPos(x + 1, y);
    int tl = getPos(x, y + 1);
    int tr = getPos(x + 1, y + 1);

    vec2 vbl = (pos - vec2(x * unit, y * unit)) / float(unit);
    vec2 vbr = (pos - vec2((x + 1) * unit, y * unit)) / float(unit);
    vec2 vtl = (pos - vec2(x * unit, (y + 1) * unit)) / float(unit);
    vec2 vtr = (pos - vec2((x + 1) * unit, (y + 1) * unit)) / float(unit);

    float valbl = dot(rotate(vecs[bl], time * changeSpeed), vbl);
    float valbr = dot(rotate(vecs[br], time * changeSpeed), vbr);
    float valtl = dot(rotate(vecs[tl], time * changeSpeed), vtl);
    float valtr = dot(rotate(vecs[tr], time * changeSpeed), vtr);

    float valb = mix(valbl, valbr, blending(uv.x));
    float valt = mix(valtl, valtr, blending(uv.x));
    float valall = mix(valb, valt, blending(uv.y));
    valall = (valall + 1.0) / 2.0;

    FragColor = mix(color2, color1, valall);

    float dist = length(vec2((pos.x - 400.0) / 400.0, (pos.y - 300.0) / 300.0));
    // dist == x -> alpha = 1
    // dist == 1 -> alpha = 0
    float fade_position = 0.87;
    if(dist > 1){
        discard;
    }
    if(dist < fade_position){
        discard;
    }
    else{
        FragColor.a = (1.0 / (fade_position - 1)) * dist - (1.0 / (fade_position - 1));
        FragColor.a = mix(1.0, 0.0, blending((dist - fade_position) / (1.0 - fade_position)));
    }
}  


// void main()
// { 
    // FragColor = texture(screenTexture, TexCoords);
// }