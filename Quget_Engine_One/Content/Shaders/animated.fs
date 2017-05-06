#version 450 core
in vec2 vs_textureOffset;
in vec4 vs_color;
uniform sampler2D textureObject;
out vec4 color;

void main(void)
{
	 color = texture(textureObject, vs_textureOffset) * vs_color;
}