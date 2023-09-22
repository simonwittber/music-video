Shader "AudioSpectrum"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,1)
    }

    CGINCLUDE

    #include "UnityCG.cginc"
    
    sampler2D _MainTex;
    
    float _AudioSpectrum[1023];
    float _SpectrumCenter, _SpectrumScale;
    int _BucketCount = 1023;
    float4 _Color;

    struct appdata
    {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
    };

    struct v2f
    {
        float2 uv : TEXCOORD0;
        float2 uv2 : TEXCOORD1;
        float4 vertex : SV_POSITION;
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
        float s = _AudioSpectrum[i.uv.x*(1023)];
        return abs(i.uv.y-_SpectrumCenter) < s*_SpectrumScale ? _Color:0;
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
    
    

