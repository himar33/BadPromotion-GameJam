Shader "Unlit/shader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
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

            float3 CalculateLight(float3 normal)
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

            fixed4 frag(VertexOutput o) : SV_Target
            {
                // sample the texture
                fixed4 tex = tex2D(_MainTex, o.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, tex);


                // Because of the interpolators from the pipeline, normals arrive not normalized
                float3 normal = normalize(o.normals);                
                float3 light = CalculateLight(normal);

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
