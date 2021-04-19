Shader "Custom/Test"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void vert (inout appdata_full v)
        {
            float h = tex2Dlod(_MainTex, v.texcoord).r;
            v.vertex += float4(0,h,0,0)*2;

            float3 d = float3(1,-1,0) * .05;

            float h1 = tex2Dlod(_MainTex, v.texcoord+d.xxzz).r;
            float h2 = tex2Dlod(_MainTex, v.texcoord+d.xyzz).r;
            float h3 = tex2Dlod(_MainTex, v.texcoord+d.yxzz).r;
            float h4 = tex2Dlod(_MainTex, v.texcoord+d.yyzz).r;

            float gradX = ((h1 - h3) + (h2 - h4)) * .5;
            float gradZ = ((h1 - h2) + (h3 - h4)) * .5;
            float3 grad = normalize(float3(gradX, 0, gradZ));

            v.normal = normalize(v.normal + grad*.5);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
