Shader "Unlit/shader"
{
    Properties
    {
        [NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Gloss("Gloss", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

             struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normals : NORMAL;
                float4 tangent : TANGENT;
            };

            struct VertexOutput
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normals : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            struct CelShadingProps
            {
                float a;
                float b;
                float c;
                float d;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Gloss;
            float4 _Color;

            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normals = v.normals;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                UNITY_TRANSFER_FOG(o, o.vertex);

                return o;
            }

            float3 CalculateDirLight(float3 normal)
            {
                float3 lightDir = _WorldSpaceLightPos0.xyz;

                float3 lightColor = _LightColor0.rgb;
                float lightIntensity = max(dot(lightDir, normal), 0);

                CelShadingProps csp = { 0.1f, 0.3f, 0.6f, 1.0f };
                if (lightIntensity < csp.a) lightIntensity = 0.2f;
                else if (lightIntensity < csp.b) lightIntensity = csp.b;
                else if (lightIntensity < csp.c) lightIntensity = csp.c;
                else lightIntensity = csp.d;

                return lightIntensity * lightColor;
            }

            float3 CalculatePointLight(float3 fragPos, float3 normal, int i)
            {
                float3 position = normalize(unity_LightPosition[i]).xyz;
                half4 color = unity_LightColor[i];
                half4 att = unity_LightAtten[i];

                float3 lightDir = normalize(position - fragPos);

                // Diffuse shading
                float diff = max(dot(normal, lightDir), 0.0);

                //if (diff < csp.a) diff = 0.25f;
                //else if (diff < csp.b) diff = csp.b;
                //else if (diff < csp.c) diff = csp.c;
                //else diff = csp.d;

                // Specular shading
                //vec3 reflectDir = reflect(-lightDir, normal);
                //float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
                //spec = step(0.5f, spec);

                // Attenuation
                float distance = length(fragPos - position);
                //float attenuation = 1.0 / (1 + light.constant + light.lin * distance + light.quadratic * (distance * distance));
                float attenuation = att;
                //attenuation *= light.intensity;

                //float3 ambient = light.ambient /** material.diffuse*/ * color;
                //float3 diffuse = diffuse * diff /** material.diffuse*/;
                //vec3 specular = light.specular * spec /** material.specular*/;

                float3 ambient = color;
                float3 diffuse = diff;
                ambient *= attenuation;
                diffuse *= attenuation;
                //specular *= attenuation;

                return (ambient + diffuse /*+ specular*/);

            }

            fixed4 frag(VertexOutput o) : SV_Target
            {
                // sample the texture
                fixed4 tex = tex2D(_MainTex, o.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, tex);


                // Because of the interpolators from the pipeline, normals arrive not normalized
                float3 normal = normalize(o.normals);                
                float3 light = CalculateDirLight(normal);
                
                for (int i = 0; i < 8; ++i)
                {
                    light += CalculatePointLight(o.vertex, normal, i);
                }

                // Specular lightning
                //float3 camPos = _WorldSpaceCameraPos;
                //float3 fragToCam = camPos - o.worldPos;
                //float3 viewDir = normalize(fragToCam);
                //
                //float3 reflection = reflect(-viewDir, normal);
                //float specular = pow(max(dot(reflection, lightDir), 0), _Gloss);

                float4 col = float4(light * _Color.rgb * tex.rgb, 0);
                //float4 col = totLight * tex;

                return col;
            }
            ENDCG
        }
    }
}
