#version 330 core
out vec4 FragColor;

in vec2 TexCoords;
uniform sampler2D gAlbedoSpec;
uniform sampler2D lightingMap;

void main()
{       
    const float gamma = 2.1;

    // retrieve data from gbuffer
    vec3 Diffuse = texture(gAlbedoSpec, TexCoords).rgb;
    vec3 Lighting = texture(lightingMap, TexCoords).rgb;

    // add ambient and lighting
    vec3 color = (Diffuse * 0.1) + Lighting;
    color = pow(color, vec3(1.0 / gamma)); // gamma correction

    FragColor = vec4(color, 1.0);
}