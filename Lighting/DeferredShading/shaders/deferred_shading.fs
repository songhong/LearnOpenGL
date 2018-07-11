#version 330 core
out vec4 FragColor;

uniform vec2 gScreenSize;
uniform sampler2D gPosition;
uniform sampler2D gNormal;
uniform sampler2D gAlbedoSpec;

struct Light {
    vec3 Position;
    vec3 Color;
    
    float Linear;
    float Quadratic;
};

uniform Light light;
uniform vec3  viewPos;

void main()
{             
    vec2 TexCoords = gl_FragCoord.xy / gScreenSize;

    // retrieve data from gbuffer
    vec3 FragPos = texture(gPosition, TexCoords).rgb;
    vec3 Normal = texture(gNormal, TexCoords).rgb;
    vec3 Diffuse = texture(gAlbedoSpec, TexCoords).rgb;
    float Specular = texture(gAlbedoSpec, TexCoords).a;

    // then calculate lighting as usual
    vec3 viewDir  = normalize(viewPos - FragPos);

    // diffuse
    vec3 lightDir = normalize(light.Position - FragPos);
    float diff = max(dot(Normal, lightDir), 0.0);
    vec3 diffuse = diff * Diffuse * light.Color;

    // specular
    vec3 halfwayDir = normalize(lightDir + viewDir);  
    float spec = pow(max(dot(Normal, halfwayDir), 0.0), 16.0);
    vec3 specular = spec * Specular * light.Color;

    // attenuation
    float distance = length(light.Position - FragPos);
    float attenuation = 1.0 / (1.0 + light.Linear * distance + light.Quadratic * distance * distance);

    // final
    vec3 lighting = (diffuse + specular) * attenuation;
    FragColor = vec4(lighting, 1.0);
}