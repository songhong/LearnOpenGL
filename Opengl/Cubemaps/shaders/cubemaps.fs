#version 330 core
out vec4 FragColor;

in vec3 Position;
in vec3 Normal;
in vec2 TexCoords;

uniform vec3 cameraPos;
uniform samplerCube skybox;

uniform sampler2D texture_diffuse1;
uniform sampler2D texture_specular1;
uniform sampler2D texture_height1; // as reflection map


void main()
{   
    vec3 I = normalize(Position - cameraPos);
	vec3 R = reflect(I, normalize(Normal));

	vec4 reflect_color = texture(skybox, R);
	vec4 factor = texture(texture_height1, TexCoords);

	vec4 diffuse_color = texture(texture_diffuse1, TexCoords);

	FragColor = diffuse_color + factor * reflect_color;

	//vec3 R = refract(I, normalize(Normal), 1.00/1.52);
    //FragColor = vec4(texture(skybox, R).rgb, 1.0);
}