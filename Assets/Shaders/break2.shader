Shader "Custom/brake" {
	Properties {
		_Color ("Color", Color) = (1,1,1,0.5)
		_Color2 ("Color2", Color) = (0.5, 0.5, 1.0, 0.5)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_HandPos ("HandPos", Vector) = (0,0,0,0)
		_Alpha ("Alpha", Range(0,1)) = 0.5
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
        float _Alpha;

		struct Input {
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			//_HandPos.x += _CosTime.a;
			float dist = distance( _HandPos , IN.worldPos); //fixed3(0,0,0), IN.worldPos);
			//_HandPos.x += _SinTime.a;
			float dist2 = distance( _HandPos , IN.worldPos); //fixed3(0,0,0), IN.worldPos);
			float val = abs(sin(dist*3.0+_Time*100)) + abs(sin(dist2*3.0+_Time*100));
			if( val > 0.98 ){
				o.Albedo = _Color*0.95 + _SinTime.z*0.05;//fixed4(1, 1, 1, 1);
				o.Alpha = _Alpha;
			} else {
				o.Albedo = _Color2;//fixed4(110/255.0, 87/255.0, 139/255.0, 1);
				o.Alpha = _Alpha;
				o.Emission = _Color*0.95 + _SinTime.z*0.05;
			}
		}
		ENDCG
	}
	FallBack "Diffuse"
}
