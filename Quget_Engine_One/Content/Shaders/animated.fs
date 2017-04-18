#version 450 core
in vec2 vs_textureCoordinate;
in vec4 vs_color;
in vec3 vs_spriteUV;

uniform sampler2D textureObject;
out vec4 color;

void main(void)
{
	//ToDo better animation sheet, currently works well with width,
	float count = vs_spriteUV.x;
	float index = vs_spriteUV.y;
	float countY = vs_spriteUV.z;
	//float index = vs_spriteUV.z;
	vec2 newCoords = vec2( vs_textureCoordinate.x/count + ((1/count)* index),vs_textureCoordinate.y/countY);
	color = texture(textureObject,newCoords) * vs_color;
}