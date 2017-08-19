Shader "Custom/waveline" {
	Properties {
		_Color ("Color", Color) = (1,1,1,0.5)
		_Color2 ("Color2", Color) = (0.5, 0.5, 1.0, 0.5)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_HandPos ("HandPos", Vector) = (-20,0,0,0)
		_HandPos2 ("HandPos", Vector) = (20,0,0,0)
		_Alpha ("Alpha", Range(0,1)) = 0.9
		_Level ("Level", Range(0,1)) = 0.9
	}
	SubShader {

		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard alpha
		#pragma target 3.0

		float4 _Color;
        float4 _Color2;
        fixed3 _HandPos;
        fixed3 _HandPos2;
        float _Alpha;
        float _Level;

        float rand(float2 co) {
		return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
		}
        float2 mod(float2 a, float2 b)
		{
			return a - floor(a / b) * b;
		}
		struct Input {
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			_HandPos.y += _CosTime.a*rand(3);
			float dist = distance( _HandPos , IN.worldPos); //fixed3(0,0,0), IN.worldPos);
			_HandPos2.y += _SinTime.a;
			float dist2 = distance( _HandPos2 , IN.worldPos); //fixed3(0,0,0), IN.worldPos);
			_HandPos2.y += _SinTime.x;
			float dist3 = distance( _HandPos2 , IN.worldPos); //fixed3(0,0,0), IN.worldPos);
			float val = abs(sin(dist*3.0-_SinTime.x*1))*0.4 + abs(sin(dist2*3.0-_SinTime.a*1))*0.4 + abs(sin(dist3-_SinTime.a*0.1))*0.2;
			if( val > _Level ){
				o.Albedo = _Color;// + _SinTime.z*0.05;//fixed4(1, 1, 1, 1);
				o.Alpha = 1;//_Alpha;
			} else if( val > _Level/4 ){
				o.Albedo = _Color*0.85;//*val + _SinTime.x*0.05;//fixed4(1, 1, 1, 1);
				o.Alpha = 0.75;//_Alpha;
			} else {
				o.Albedo = _Color2;//fixed4(110/255.0, 87/255.0, 139/255.0, 1);
				o.Alpha = 0.5;//_Alpha;
			}
		}
		ENDCG
	}
	FallBack "Diffuse"
}
