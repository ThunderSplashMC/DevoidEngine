﻿#version 460 core

uniform mat4 W_VIEW_MATRIX;
uniform mat4 W_PROJECTION_MATRIX;

layout(location = 1) out vec3 nearPoint;
layout(location = 2) out vec3 farPoint;


// Grid position are in clipped space
vec3 gridPlane[6] = vec3[] (
    vec3(1, 1, 0), vec3(-1, -1, 0), vec3(-1, 1, 0),
    vec3(-1, -1, 0), vec3(1, 1, 0), vec3(1, -1, 0)
);

vec3 UnprojectPoint(float x, float y, float z, mat4 view, mat4 projection) {
    mat4 viewInv = inverse(view);
    mat4 projInv = inverse(projection);
    vec4 unprojectedPoint =  vec4(x, y, z, 1.0) * (projInv * viewInv);
    return unprojectedPoint.xyz / unprojectedPoint.w;
}

void main() {
    vec3 p = gridPlane[gl_VertexID].xyz;
    nearPoint = UnprojectPoint(p.x, p.y, 0.0, W_VIEW_MATRIX, W_PROJECTION_MATRIX).xyz; // unprojecting on the near plane
    farPoint = UnprojectPoint(p.x, p.y, 1.0, W_VIEW_MATRIX, W_PROJECTION_MATRIX).xyz; // unprojecting on the far plane
    gl_Position = vec4(p, 1.0); // using directly the clipped coordinates
}
