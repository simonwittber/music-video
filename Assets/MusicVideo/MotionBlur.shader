Shader "MotionBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LastFrameTex ("Last Frame", 2D) = "black" {}
        _TurbulenceTex ("Turbulence", 2D) = "black" {}
        _Color ("Color", Color) = (0,0,0,1)
    }

    CGINCLUDE

    #include "UnityCG.cginc"
    

    sampler2D _MainTex;
    
    float _BlurFade;
    sampler2D _LastFrameTex;

    struct v2f
    {
        float2 uv : TEXCOORD0;
        float2 uv2 : TEXCOORD1;
        float4 vertex : SV_POSITION;
    };

    struct appdata
    {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
    };


    v2f vert (appdata v)
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        o.uv2 = v.uv;
        return o;
    }

    
    float4 frag(v2f i): SV_Target
    {
        float4 prev = tex2D(_LastFrameTex, i.uv) * _BlurFade;
        
        float4 col = tex2D(_MainTex, i.uv);
        col = col + prev;
        
        return col;
    
    }

    ENDCG
    
    Subshader
    {
        Pass
        {
            ZTest  Always
            Cull   Off
            ZWrite Off
            CGPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #pragma target   5.0
            ENDCG
        }
    }
}
    
    

