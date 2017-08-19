Shader "Custom/FloorShader" 
{
    Properties
    {
        _Density ("Density", Range(2,50)) = 30
		_Color ("Color", Color) = (0.5,0.2,0.4,1)
		_Color2 ("Color2", Color) = (0.2, 0.25, 0.4, 1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag 
            #include "UnityCG.cginc"
            float4 _Color;
        	float4 _Color2;

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Density;
//
            v2f vert (float4 pos : POSITION, float2 uv : TEXCOORD0)
            {
                v2f o;
                pos.x = pos.x + _SinTime.y;
                pos.z = pos.z + _CosTime.y;
                o.vertex = UnityObjectToClipPos(pos);
               
                o.uv = uv * _Density;
                return o;
            }


            fixed4 frag (v2f i) : SV_Target
            {
                float2 c = i.uv;
                c = floor(c) / 2;
                float checker = frac(c.x + c.y) * 2;
                if(checker > 0)
                {
                	return _Color*0.9 + 0.1* _SinTime.z;
                } 
                return _Color2*0.9 + 0.1* _CosTime.z;
            }
            ENDCG
        }
    }
}