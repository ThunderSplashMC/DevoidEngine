﻿#version 330 core

layout(location = 0) out vec4 color;

in vec2 v_TexCoord;

uniform sampler2D u_Texture;
uniform int USE_TEX_0;

void main()
{
    vec4 tex = texture(u_Texture, v_TexCoord);
	color = tex;
}