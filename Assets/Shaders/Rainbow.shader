// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Rainbow" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _FogColor ("Fog Color", Color) = (0.3, 0.4, 0.7, 1.0)
       _WaveCycle ("Wave Cycle", Range(0.0,5.0) ) = 0.02
    _WaveAmount ("Wave Amount", Range(0,0.1) ) = 0.020
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert finalcolor:mycolor vertex:myvert
      struct Input {
          float2 uv_MainTex;
          float fog;
          float3 worldPos;
      };
      float _WaveCycle;
float _WaveAmount;
      
      fixed4 rainbow(float x)
		{	
		float level = (x * 6) - 6 * floor((x * 6)/6);//mod(x * 6 , 6.0);//floor(x * 6.0);
		float r = float(level <= 2.0) + float(level > 4.0) * 0.5;
		float g = max(1.0 - abs(level - 2.0) * 0.5, 0.0);
		float b = (1.0 - (level - 4.0) * 0.5) * float(level >= 4.0);
		return fixed4(r, g, b,1);
		}
      void myvert (inout appdata_full v, out Input data)
      {
          UNITY_INITIALIZE_OUTPUT(Input,data);
          float4 hpos = UnityObjectToClipPos (v.vertex);
          data.fog = dot (hpos.z*0.28, hpos.z*1) * 0.1;//min (8, dot (hpos.z*0.04, hpos.z*1) * 0.1);
          
          float4 p = v.vertex;
		    float sy = p.y;
		    p.x += sin(((_Time.y) * _WaveCycle)) *  _WaveAmount * sy;
		    p.z += cos(((_Time.y) * _WaveCycle)) *  _WaveAmount * sy;
		    
		    p.y = sin(p.y + (p.z + p.x)*0.05*sin((_Time.y * _WaveCycle)));

		    v.vertex = p;   
      }
      fixed4 _FogColor;
      void mycolor (Input IN, SurfaceOutput o, inout fixed4 color)
      {
          fixed3 fogColor = _FogColor.rgb;
          #ifdef UNITY_PASS_FORWARDADD
          fogColor = 0;
          #endif
          color = rainbow(IN.fog)*0.25 + color*0.75;//rainbow(IN.fog);//lerp (color.rgb, fogColor, IN.fog);
      }
      sampler2D _MainTex;
      void surf (Input IN, inout SurfaceOutput o) {
           o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }