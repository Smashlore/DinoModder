// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/Building" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Noise ("Nose (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Test("Test", float) = 0.5
		_Color0("Color0", Color) = (1,1,1,1)
		_Color1("Color1", Color) = (1,1,1,1)
		_Color2("Color2", Color) = (1,1,1,1)
		_Color3("Color3", Color) = (1,1,1,1)
		_GlowColor("Glow Color", Color) = (1,1,1,1)
		_Damage("Damage",Range(0,1)) = 0.0
		_UpgradeGo("Upgrade",Range(0,500)) = 500.0
		_RemoveLerp("Remove Lerp",Range(0,1)) = 0.0
 
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows  vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 4.0

		sampler2D _MainTex;
		sampler2D _Noise;

		struct Input {
			float3 worldNormal;
			float3 worldPos;
			float3 localPos;
			float2 uv_MainTex;
			float3 customColor;

		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _GlowColor;
		
		float4 _Color0;
		float4 _Color1;
		float4 _Color2;
		float _Snow;
		float _Damage;
		float4 _Color3;
		float _UpgradeGo;
		float _RemoveLerp;
		float _Test;
	
		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
			UNITY_DEFINE_INSTANCED_PROP(float, _Select)
			UNITY_DEFINE_INSTANCED_PROP(float, _Add)
			UNITY_DEFINE_INSTANCED_PROP(float, _Delete)
 
		UNITY_INSTANCING_BUFFER_END(Props)

		void vert(inout appdata_full v, out Input o) 
		{
		
			v.vertex.xyz += v.normal * _Damage*sin(v.vertex.x*10000.5252f+v.vertex.z*12252.420f)*0.001f;			
			UNITY_INITIALIZE_OUTPUT(Input, o);
			float2 tex = v.texcoord;
			o.customColor = float3(1.0f, 0.0f, 0.0f);
			o.localPos = v.vertex.xyz;
			if (tex.x < 0.5f)
			{
				if (tex.y < 0.5f)
				{
					o.customColor = _Color0;
				}
				else
				{
					o.customColor = _Color1;
				}
			}
			else				 
			{
				if (tex.y < 0.5f)
				{
					o.customColor = _Color2;
				}
				else
				{
					o.customColor = _Color3;
				}
			}
			
		}

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = IN.customColor;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
		 
			fixed snow = step(_Test,dot(WorldNormalVector(IN,o.Normal),float3(0.0f,1.0f,0.0f)));
			o.Albedo.rgb =  lerp(o.Albedo.rgb,float3(0.6f,0.75,1.0f),snow*_Snow);
			fixed avg = o.Albedo.r+o.Albedo.g+o.Albedo.b;
			o.Albedo.rgb = lerp(o.Albedo.rgb ,avg.xxx*0.4f,_Damage);

			float4 glow = _GlowColor*2.0f;
			float glowPow = -(IN.localPos.z - (_UpgradeGo)*1.0f);
			float downGrade = (IN.localPos.z - (_RemoveLerp-1000.0f)*1.0f);
			o.Emission += glow * step(saturate(glowPow),0.3f);
			clip(glowPow);
			clip(downGrade);
			


			o.Albedo.rgb = lerp(o.Albedo.rgb, float3(0.0f, 0.4f, 1.0f), UNITY_ACCESS_INSTANCED_PROP(Props, _Select) );
			o.Albedo.rgb = lerp(o.Albedo.rgb, float3(1.0f, 0.0f, 0.1f), UNITY_ACCESS_INSTANCED_PROP(Props, _Delete));
			o.Emission = lerp(float3(0.0f, 0.4f, 1.0f)*0.1f*UNITY_ACCESS_INSTANCED_PROP(Props, _Select), float3(1.0f, 0.0f, 0.1f)*0.4f, UNITY_ACCESS_INSTANCED_PROP(Props, _Delete))+ UNITY_ACCESS_INSTANCED_PROP(Props, _Add);

			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
