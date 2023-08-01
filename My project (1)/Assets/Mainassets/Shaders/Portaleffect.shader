// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Portaleffect"
{
    Properties 
    {   
        _distoretex("Distore Texture", 2D) = "white" {}
        _MainTex ("Main Base (RGB)", 2D) = "white" {}
        _Color ("Color (RGBA)", Color) = (1, 1, 1, 1) // add _Color property
        _offset ("Offset", float) = 0.5
    
        _Transparency ("Transparency", Range(0.0, 1)) = 0.5
        _distortionintensity ("Distortion Intesity", float) = 1
        _distortionspeed ("Distortion Speed", Range(0.0,10.0)) = 1
        _distortionthickness ("Distortion Thickness", Range(0.0,1.0)) = 0.5
        _glowintensity ("Glow Intensity", Range(0.0,2.0)) = 0.5
        _glowthickness ("Glow Thickness", range(0.0,1.0)) = 0.5
    }

    SubShader 
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "RenderType"="Opaque" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        cull off
    
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex, _distoretex;
            float4 _MainTex_TexelSize;
            float4 _MainTex_ST;
            float4 _Color;
            float _Transparency;
            float4 _distoretex_ST;
            float _distortionintensity;
            float _distortionspeed;
            float _distortionthickness;
            float _glowintensity;
            float _glowthickness;
            float _offset;
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {   
              
                fixed4 maintex = tex2D(_MainTex, i.uv);
                

                
                float lumiance = Luminance(maintex);
                float4 maincolor = lumiance * _Color;
            
                
                float3 distoretex = tex2D(_distoretex, i.uv);
                float distoremove = abs(sin(distoretex.r * _distortionintensity +(_Time.y * _distortionspeed)));
             
                float glow = 1- smoothstep(distoremove - _glowthickness, distoremove +_glowthickness , _distortionthickness);
                glow *= _glowintensity;
                

                fixed4 finalmaintex = fixed4(maincolor);
               
                finalmaintex.a = _Transparency;
    
          
                return fixed4(finalmaintex * glow);
            }
            ENDCG
        }
    }
}
