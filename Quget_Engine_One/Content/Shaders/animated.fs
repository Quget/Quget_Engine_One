#version 450 core
in vec2 vs_textureCoordinate;
in vec4 vs_color;
in vec3 vs_spriteUV;

uniform sampler2D textureObject;
out vec4 color;

void main(void)
{
	//ToDo better animation sheet, currently works well with width,

	float countX = vs_spriteUV.x;
	float index = vs_spriteUV.y;
	float countY = vs_spriteUV.z;

	float indexX = floor(mod(index,countX));
	float indexY = floor(index / countY);

	vec2 newCoords = vec2( vs_textureCoordinate.x/countX + ((1/countX)* indexX),vs_textureCoordinate.y/countY + ((1/countY)* indexY));
	color = texture(textureObject,newCoords) * vs_color;
}