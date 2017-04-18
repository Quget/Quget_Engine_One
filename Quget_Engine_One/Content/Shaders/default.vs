﻿#version 450 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec2 textureCoordinate;
layout (location = 2) in vec4 color;

out vec2 vs_textureCoordinate;
out vec4 vs_color;

layout(location = 20) uniform  mat4 projection;
layout (location = 21) uniform  mat4 modelView;

void main(void)
{
	vs_textureCoordinate = textureCoordinate;
	gl_Position = projection * modelView * position;
	vs_color = color;
}