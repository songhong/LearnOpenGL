#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform vec3 cameraPos;
uniform samplerCube skybox;

uniform sampler2D texture_diffuse1;
uniform sampler2D texture_specular1;
uniform sampler2D texture_height1; // as reflection map


void main()
{   
	FragColor = texture(texture_diffuse1, TexCoords);
}