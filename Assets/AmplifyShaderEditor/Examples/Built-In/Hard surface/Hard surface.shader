// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hard surface"
{
	Properties
	{
		[Toggle]_UseColorMap("Use Color Map", Float) = 1
		[HDR]_Color0("Color 0", Color) = (0,1,1,0)
		_ColorMap("Color Map", 2D) = "white" {}
		_Addcolor("Add color", Range( 0 , 1)) = 0
		[HDR]_EmissionColor("Emission Color", Color) = (0,1,1,0)
		_EmissionMap("Emission Map", 2D) = "white" {}
		[Toggle]_UseMetallicMap("Use Metallic Map", Float) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_MetallicMap("Metallic Map", 2D) = "white" {}
		[Normal]_NormalMap("Normal Map", 2D) = "bump" {}
		_NormalStrength("Normal Strength", Range( 0 , 1)) = 1
		[Toggle]_UseRoughnessMap("Use Roughness  Map", Float) = 0
		_Roughness("Roughness", Range( 0 , 1)) = 0.3
		_RoughnessMap("Roughness Map", 2D) = "white" {}
		_Specularcolor("Specular color", Color) = (1,1,1,0)
		_Specularstrength("Specular strength", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _NormalMap;
		uniform float _NormalStrength;
		uniform float _UseColorMap;
		uniform float4 _Color0;
		uniform sampler2D _ColorMap;
		uniform float _Addcolor;
		uniform float4 _EmissionColor;
		uniform sampler2D _EmissionMap;
		uniform float4 _Specularcolor;
		uniform float _Specularstrength;
		uniform float _UseMetallicMap;
		uniform float _Metallic;
		uniform sampler2D _MetallicMap;
		uniform float _UseRoughnessMap;
		uniform float _Roughness;
		uniform sampler2D _RoughnessMap;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			o.Normal = UnpackScaleNormal( tex2D( _NormalMap, i.uv_texcoord ), _NormalStrength );
			float4 tex2DNode10 = tex2D( _ColorMap, i.uv_texcoord );
			float4 blendOpSrc36 = ( _Color0 * tex2DNode10 );
			float4 blendOpDest36 = ( _Color0 + tex2DNode10 );
			float4 lerpResult33 = lerp( tex2DNode10 , ( saturate( (( blendOpSrc36 > 0.5 ) ? max( blendOpDest36, 2.0 * ( blendOpSrc36 - 0.5 ) ) : min( blendOpDest36, 2.0 * blendOpSrc36 ) ) )) , _Addcolor);
			float4 temp_output_14_0 = (( _UseColorMap )?( lerpResult33 ):( _Color0 ));
			o.Albedo = temp_output_14_0.rgb;
			o.Emission = ( _EmissionColor * tex2D( _EmissionMap, i.uv_texcoord ) ).rgb;
			float4 temp_cast_2 = (_Metallic).xxxx;
			float4 lerpResult40 = lerp( ( _Specularcolor * ( pow( ( _Specularstrength - 1.0 ) , 2.0 ) / pow( ( _Specularstrength + 1.0 ) , 2.0 ) ) ) , (( _UseColorMap )?( lerpResult33 ):( _Color0 )) , (( _UseMetallicMap )?( tex2D( _MetallicMap, i.uv_texcoord ) ):( temp_cast_2 )));
			o.Specular = lerpResult40.rgb;
			float4 temp_cast_4 = (_Roughness).xxxx;
			o.Smoothness = (( _UseRoughnessMap )?( ( 1.0 - tex2D( _RoughnessMap, i.uv_texcoord ) ) ):( temp_cast_4 )).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
2430;44;1411;863;1386.013;-184.0428;1.587617;True;True
Node;AmplifyShaderEditor.CommentaryNode;38;-2011.907,-961.8425;Inherit;False;1845.526;948.5177;Color;8;10;1;35;32;36;34;33;14;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;39;-2731.14,84.36539;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;1;-1900.947,-911.8425;Inherit;False;Property;_Color0;Color 0;1;1;[HDR];Create;True;0;0;False;0;False;0,1,1,0;0.03760567,0.1698113,0.0248309,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-1961.907,-732.6855;Inherit;True;Property;_ColorMap;Color Map;2;0;Create;True;0;0;False;0;False;-1;80ab37a9e4f49c842903bb43bdd7bcd2;04372d992d5f2f347a8e4d61ea8014bc;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;48;-1071.614,1302.513;Inherit;False;Property;_Specularstrength;Specular strength;15;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-1497.7,-267.3249;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-1497.087,-483.9308;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;-838.8565,1375.833;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;46;-849.1671,1230.339;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;44;-667.0121,1230.339;Inherit;False;False;2;0;FLOAT;2;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;45;-655.5561,1370.105;Inherit;False;False;2;0;FLOAT;2;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-931.1492,-235.6514;Inherit;False;Property;_Addcolor;Add color;3;0;Create;True;0;0;False;0;False;0;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;36;-1217.433,-357.2684;Inherit;True;PinLight;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;42;-696.7985,992.0478;Inherit;False;Property;_Specularcolor;Specular color;14;0;Create;True;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-1015.086,732.5622;Inherit;False;Property;_Metallic;Metallic;7;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;43;-411.2915,1281.374;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;26;-960.9607,2015.928;Inherit;True;Property;_RoughnessMap;Roughness Map;13;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;33;-666.7313,-726.7027;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;23;-1014.226,897.7396;Inherit;True;Property;_MetallicMap;Metallic Map;8;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;19;-1037.1,509.8874;Inherit;True;Property;_EmissionMap;Emission Map;5;0;Create;True;0;0;False;0;False;-1;None;5efe5da7a3a7af54cbd4afd4dbba9e56;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;21;-932.7706,307.7909;Inherit;False;Property;_EmissionColor;Emission Color;4;1;[HDR];Create;True;0;0;False;0;False;0,1,1,0;0.1254902,1.521569,1.584314,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-1316.233,130.2124;Inherit;False;Property;_NormalStrength;Normal Strength;10;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-339.1229,1013.615;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;24;-602.3896,844.6418;Inherit;False;Property;_UseMetallicMap;Use Metallic Map;6;0;Create;True;0;0;False;0;False;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-962.291,1924.003;Inherit;False;Property;_Roughness;Roughness;12;0;Create;True;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;31;-580.1599,2059.929;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;14;-390.3803,-848.3101;Inherit;False;Property;_UseColorMap;Use Color Map;0;0;Create;True;0;0;False;0;False;1;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-487.7172,450.8345;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;17;-992.6656,22.71296;Inherit;True;Property;_NormalMap;Normal Map;9;1;[Normal];Create;True;0;0;False;0;False;-1;None;e0e1d5bf6b0b0d840b3f87cc83fe4a73;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;27;-383.8688,1906.305;Inherit;False;Property;_UseRoughnessMap;Use Roughness  Map;11;0;Create;True;0;0;False;0;False;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;40;-52.4841,806.1895;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;594.8664,-50.29806;Float;False;True;-1;2;ASEMaterialInspector;0;0;StandardSpecular;Hard surface;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;1;39;0
WireConnection;32;0;1;0
WireConnection;32;1;10;0
WireConnection;35;0;1;0
WireConnection;35;1;10;0
WireConnection;47;0;48;0
WireConnection;46;0;48;0
WireConnection;44;0;46;0
WireConnection;45;0;47;0
WireConnection;36;0;35;0
WireConnection;36;1;32;0
WireConnection;43;0;44;0
WireConnection;43;1;45;0
WireConnection;26;1;39;0
WireConnection;33;0;10;0
WireConnection;33;1;36;0
WireConnection;33;2;34;0
WireConnection;23;1;39;0
WireConnection;19;1;39;0
WireConnection;41;0;42;0
WireConnection;41;1;43;0
WireConnection;24;0;13;0
WireConnection;24;1;23;0
WireConnection;31;0;26;0
WireConnection;14;0;1;0
WireConnection;14;1;33;0
WireConnection;22;0;21;0
WireConnection;22;1;19;0
WireConnection;17;1;39;0
WireConnection;17;5;7;0
WireConnection;27;0;9;0
WireConnection;27;1;31;0
WireConnection;40;0;41;0
WireConnection;40;1;14;0
WireConnection;40;2;24;0
WireConnection;0;0;14;0
WireConnection;0;1;17;0
WireConnection;0;2;22;0
WireConnection;0;3;40;0
WireConnection;0;4;27;0
ASEEND*/
//CHKSM=CE519AB6307C8986C4CA6BBC7B22524C6164EA6A