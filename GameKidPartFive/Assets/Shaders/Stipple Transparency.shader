Shader "Stipple Transparency" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	_Transparency("Transparency", Range(0,1)) = 1.0
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 150
		CGPROGRAM
#pragma surface surf Lambert noforwardadd
		sampler2D _MainTex;
	struct Input {
		float2 uv_MainTex;
		float4 screenPos;
	};
	half _Transparency;
	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = c.rgb;
		o.Alpha = c.a;
		// Screen-door transparency: Discard pixel if below threshold.
		float2x2 thresholdMatrix =
		{ 
			1.0/5.0, 3.0 /5.0,
			4.0 /5.0, 2.0 /5.0
		};
		//float4x4 thresholdMatrix =
		//{ 1.0 / 17.0,  9.0 / 17.0,  3.0 / 17.0, 11.0 / 17.0,
		//	13.0 / 17.0,  5.0 / 17.0, 15.0 / 17.0,  7.0 / 17.0,
		//	4.0 / 17.0, 12.0 / 17.0,  2.0 / 17.0, 10.0 / 17.0,
		//	16.0 / 17.0,  8.0 / 17.0, 14.0 / 17.0,  6.0 / 17.0
		//};

		//float4x4 thresholdMatrix =
		//{
		//	 1.0/65.0, 49.0/65.0, 13.0/65.0, 61.0/65.0,  4.0/65.0, 52.0/65.0, 16.0/65.0, 64.0/65.0,
		//	33.0/65.0, 17.0/65.0, 45.0/65.0, 29.0/65.0, 36.0/65.0, 20.0/65.0, 48.0/65.0, 32.0/65.0,
		//	 9.0/65.0, 57.0/65.0,  5.0/65.0, 53.0/65.0, 12.0/65.0, 60.0/65.0,  8.0/65.0, 56.0/65.0,
		//	41.0/65.0, 25.0/65.0, 37.0/65.0, 21.0/65.0, 44.0/65.0, 28.0/65.0, 40.0/65.0, 24.0/65.0,
		//	 3.0/65.0, 51.0/65.0, 15.0/65.0, 63.0/65.0,  2.0/65.0, 50.0/65.0, 14.0/65.0, 62.0/65.0,
		//	35.0/65.0, 19.0/65.0, 47.0/65.0, 31.0/65.0, 34.0/65.0, 18.0/65.0, 46.0/65.0, 30.0/65.0,
		//	11.0/65.0, 59.0/65.0,  7.0/65.0, 55.0/65.0, 10.0/65.0, 58.0/65.0,  6.0/65.0, 54.0/65.0,
		//	43.0/65.0, 27.0/65.0, 39.0/65.0, 23.0/65.0, 42.0/65.0, 26.0/65.0, 38.0/65.0, 22.0/65.0
		//}

		//float4x4 _RowAccess = { 1,0,0,0, 0,1,0,0, 0,0,1,0, 0,0,0,1 };
		float2x2 _RowAccess = { 1,0, 0,1};

		float2 pos = IN.screenPos.xy / IN.screenPos.w;
		pos *= _ScreenParams.xy; // pixel position
		clip((_Transparency * c.w) - thresholdMatrix[fmod(pos.x, 2)] * _RowAccess[fmod(pos.y, 2)]);
	}
	ENDCG
	}
		Fallback "Mobile/VertexLit"
}