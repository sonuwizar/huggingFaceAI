// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SEC/Holder"
{
	Properties
	{
		_colorMap("color Map", 2D) = "white" {}
		_Float0("Float 0", Range( 0 , 4)) = 0.3130466
		_smothness("smothness", Range( 0 , 4)) = 1.024892
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _colorMap;
		uniform float4 _colorMap_ST;
		uniform float _Float0;
		uniform float _smothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_colorMap = i.uv_texcoord * _colorMap_ST.xy + _colorMap_ST.zw;
			o.Albedo = tex2D( _colorMap, uv_colorMap ).rgb;
			o.Metallic = _Float0;
			o.Smoothness = _smothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
124;322;892;712;1466.154;282.2212;1.761773;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-644.2141,-305.1626;Inherit;True;Property;_colorMap;color Map;0;0;Create;True;0;0;False;0;False;-1;a2c2f327bfc7fd743bfc4075fa7b0e8c;a2c2f327bfc7fd743bfc4075fa7b0e8c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-830.6619,-33.20925;Inherit;True;Property;_NormalMap;Normal Map;1;0;Create;True;0;0;False;0;False;-1;56496a07a6029044a85590f142bfc231;87d9fe1299d6b8c4dbab12a0a8952a0f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-612.989,380.0107;Inherit;False;Property;_smothness;smothness;3;0;Create;True;0;0;False;0;False;1.024892;1.024892;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-630.1572,214.0159;Inherit;False;Property;_Float0;Float 0;2;0;Create;True;0;0;False;0;False;0.3130466;0.3130466;0;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;SEC/Holder;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;3;4;0
WireConnection;0;4;3;0
ASEEND*/
//CHKSM=DF6DD46201AD9E85FBAE116D7BCBEF533B56C183