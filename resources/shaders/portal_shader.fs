#version 330 core
out vec4 FragColor;

in vec2 TexCoord;
in vec2 OriginalTexCoord;

uniform sampler2D texture1;

void main()
{          
    if(texture(texture1, OriginalTexCoord).a < 0.1){
        discard;
    }
    FragColor = texture(texture1, OriginalTexCoord);
}  