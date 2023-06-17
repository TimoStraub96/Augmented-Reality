Shader "Custom/WhiteCubeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = 1; // Set the albedo color to white
            o.Metallic = 0; // No metallic properties
            o.Smoothness = 0.5; // Set the smoothness to a default value

            // Sample the main texture
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo *= c.rgb;
        }
        ENDCG
    }

    FallBack "Diffuse"
}