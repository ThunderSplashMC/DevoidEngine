﻿#version 330 core
in vec4 FragPos;

uniform vec3 lightPos;

void main()
{
    // get distance between fragment and light source
    float lightDistance = length(FragPos.xyz - lightPos);
    
    // map to [0;1] range by dividing by far_plane
    lightDistance = lightDistance / 300;
    
    // write this as modified depth
    gl_FragDepth = lightDistance;
}