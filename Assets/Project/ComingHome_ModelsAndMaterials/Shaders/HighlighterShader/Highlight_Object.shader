// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Highlight Object"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (0.9044118,0.6640914,0.03325041,0)
		[Toggle]_Usemap("Use map", Float) = 0
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Normalstrength("Normal strength", Range( 0 , 1)) = 1
		[HDR]_HighlightColor("Highlight Color", Color) = (0.7065311,0.9705882,0.9596617,1)
		_Highlightstrength("Highlight strength", Range( 0 , 1)) = 0.5
		[Toggle]_UseEmisionMap("UseEmisionMap", Float) = 0
		_Emission("Emission", 2D) = "black" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Oclussion("Oclussion", 2D) = "white" {}
		_EdgeOpacity("Edge Opacity", Range( 0 , 1)) = 1
		_Opacity("Opacity", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		BlendOp Add
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 viewDir;
			INTERNAL_DATA
		};

		uniform sampler2D _Normal;
		uniform float _Normalstrength;
		uniform float _Usemap;
		uniform float4 _Color;
		uniform sampler2D _Albedo;
		uniform float _UseEmisionMap;
		uniform float _Highlightstrength;
		uniform float4 _HighlightColor;
		uniform sampler2D _Emission;
		uniform float _Metallic;
		uniform sampler2D _Oclussion;
		uniform float _EdgeOpacity;
		uniform float _Opacity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 Normal67 = UnpackScaleNormal( tex2D( _Normal, i.uv_texcoord ), _Normalstrength );
			o.Normal = Normal67;
			float4 Albedo65 = (( _Usemap )?( ( _Color * tex2D( _Albedo, i.uv_texcoord ) ) ):( _Color ));
			o.Albedo = Albedo65.rgb;
			float3 normalizeResult78 = normalize( i.viewDir );
			float dotResult79 = dot( Normal67 , normalizeResult78 );
			float temp_output_82_0 = pow( ( 1.0 - saturate( dotResult79 ) ) , (10.0 + (_Highlightstrength - 0.0) * (0.0 - 10.0) / (1.0 - 0.0)) );
			float4 Highlight_Color95 = _HighlightColor;
			float4 Highlight_Rim94 = ( temp_output_82_0 * Highlight_Color95 );
			float4 Emision75 = ( tex2D( _Emission, i.uv_texcoord ) * Highlight_Color95 );
			float4 Final_Emision114 = (( _UseEmisionMap )?( Emision75 ):( Highlight_Rim94 ));
			o.Emission = Final_Emision114.rgb;
			float Metalic145 = _Metallic;
			o.Metallic = Metalic145;
			float4 Oclussion86 = tex2D( _Oclussion, i.uv_texcoord );
			o.Occlusion = Oclussion86.r;
			float Rim152 = temp_output_82_0;
			float Opacity160 = ( _EdgeOpacity * ( Rim152 + _Opacity ) );
			o.Alpha = Opacity160;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = IN.tSpace0.xyz * worldViewDir.x + IN.tSpace1.xyz * worldViewDir.y + IN.tSpace2.xyz * worldViewDir.z;
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
1920;0;1920;1019;3885.678;1539.746;2.382085;True;True
Node;AmplifyShaderEditor.CommentaryNode;137;-3085.496,-1114.421;Inherit;False;1244.203;1368.811;Comment;15;51;58;67;63;52;64;85;65;86;74;75;139;140;158;159;Textures;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;140;-2990.717,-421.1262;Inherit;False;Property;_Normalstrength;Normal strength;5;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;51;-3045.496,-593.7198;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;122;-1774.419,-357.309;Inherit;False;1900.698;589.5023;Comment;13;77;90;78;79;80;99;81;97;82;83;94;142;152;Highlight (Rim);1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;58;-2625.094,-649.5204;Inherit;True;Property;_Normal;Normal;4;0;Create;True;0;0;False;0;False;-1;11f03d9db1a617e40b7ece71f0a84f6f;11f03d9db1a617e40b7ece71f0a84f6f;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;77;-1724.419,-184.7088;Float;False;Tangent;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;67;-2229.994,-607.2198;Float;False;Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;78;-1513.018,-150.7087;Inherit;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;90;-1482.923,-316.8993;Inherit;False;67;Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DotProductOpNode;79;-1252.857,-226.6494;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;133;-3053.912,330.0851;Inherit;False;642.599;257;Comment;2;55;95;Highlight Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;80;-1019.619,-276.509;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;142;-1333.102,57.51184;Inherit;False;Property;_Highlightstrength;Highlight strength;7;0;Create;True;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;55;-3003.912,380.0851;Float;False;Property;_HighlightColor;Highlight Color;6;1;[HDR];Create;True;0;0;False;0;False;0.7065311,0.9705882,0.9596617,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;95;-2678.313,386.3863;Float;False;Highlight_Color;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;99;-955.725,19.29256;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;10;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;81;-833.4186,-321.509;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;82;-610.6185,-305.309;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;158;-2522.133,-196.1979;Inherit;False;95;Highlight_Color;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;97;-573.3085,-54.00653;Inherit;False;95;Highlight_Color;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;74;-2613.095,-439.5192;Inherit;True;Property;_Emission;Emission;9;0;Create;True;0;0;False;0;False;-1;7a170cdb7cc88024cb628cfcdbb6705c;7a170cdb7cc88024cb628cfcdbb6705c;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;159;-2228.133,-307.1979;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;162;-1764.698,-843.358;Inherit;False;962.5648;360.4866;Opacity;6;155;147;154;146;148;160;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;152;-305.2184,-311.9803;Inherit;False;Rim;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;-294.3889,-210.0838;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;147;-1608.812,-690.4399;Inherit;False;152;Rim;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;155;-1714.698,-598.8714;Inherit;False;Property;_Opacity;Opacity;13;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;75;-2190.695,-441.8194;Float;False;Emision;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;94;-111.9873,-198.5623;Float;False;Highlight_Rim;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;63;-2803.555,-1054.458;Float;False;Property;_Color;Color;0;1;[HDR];Create;True;0;0;False;0;False;0.9044118,0.6640914,0.03325041,0;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;52;-2814.155,-858.2577;Inherit;True;Property;_Albedo;Albedo;3;0;Create;True;0;0;False;0;False;-1;7130c16fd8005b546b111d341310a9a4;7130c16fd8005b546b111d341310a9a4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;120;-2305.992,331.5047;Inherit;False;987.1003;293;Comment;5;115;113;111;114;157;Emission Mix & Highlight Switching;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;-2467.707,-893.6651;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;115;-2250.572,403.2604;Inherit;False;94;Highlight_Rim;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;113;-2241.304,512.9952;Inherit;False;75;Emision;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;146;-1651.643,-793.358;Inherit;False;Property;_EdgeOpacity;Edge Opacity;12;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;154;-1359.38,-639.0563;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;111;-1796.653,376.7408;Float;False;Property;_UseEmisionMap;UseEmisionMap;8;0;Create;True;0;0;False;0;False;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;144;-1772.43,-1077.861;Inherit;False;Property;_Metallic;Metallic;10;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;85;-2694.594,-19.0179;Inherit;True;Property;_Oclussion;Oclussion;11;0;Create;True;0;0;False;0;False;-1;a8de9c9c15d9c7e4eaa883c727391bee;a8de9c9c15d9c7e4eaa883c727391bee;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;139;-2297.448,-957.4717;Inherit;False;Property;_Usemap;Use map;1;0;Create;True;0;0;False;0;False;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;148;-1200.33,-747.6528;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;160;-1026.133,-749.2032;Inherit;False;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;114;-1562.831,375.5044;Float;False;Final_Emision;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;145;-1413.76,-1077.373;Inherit;False;Metalic;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;65;-2075.293,-902.6206;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;86;-2319.594,30.9821;Float;False;Oclussion;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;76;-352.862,-666.5278;Inherit;False;86;Oclussion;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;68;-349.5197,-1040.513;Inherit;False;65;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;69;-344.4087,-950.9017;Inherit;False;67;Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;143;-334.4165,-762.4468;Inherit;False;145;Metalic;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;161;-328.5752,-568.2523;Inherit;False;160;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;157;-1819.843,518.6727;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;70;-362.5474,-871.6461;Inherit;False;114;Final_Emision;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-3.219753,-841.6202;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Highlight Object;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;1;False;-1;1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;2;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;58;1;51;0
WireConnection;58;5;140;0
WireConnection;67;0;58;0
WireConnection;78;0;77;0
WireConnection;79;0;90;0
WireConnection;79;1;78;0
WireConnection;80;0;79;0
WireConnection;95;0;55;0
WireConnection;99;0;142;0
WireConnection;81;0;80;0
WireConnection;82;0;81;0
WireConnection;82;1;99;0
WireConnection;74;1;51;0
WireConnection;159;0;74;0
WireConnection;159;1;158;0
WireConnection;152;0;82;0
WireConnection;83;0;82;0
WireConnection;83;1;97;0
WireConnection;75;0;159;0
WireConnection;94;0;83;0
WireConnection;52;1;51;0
WireConnection;64;0;63;0
WireConnection;64;1;52;0
WireConnection;154;0;147;0
WireConnection;154;1;155;0
WireConnection;111;0;115;0
WireConnection;111;1;113;0
WireConnection;85;1;51;0
WireConnection;139;0;63;0
WireConnection;139;1;64;0
WireConnection;148;0;146;0
WireConnection;148;1;154;0
WireConnection;160;0;148;0
WireConnection;114;0;111;0
WireConnection;145;0;144;0
WireConnection;65;0;139;0
WireConnection;86;0;85;0
WireConnection;157;0;113;0
WireConnection;157;1;115;0
WireConnection;0;0;68;0
WireConnection;0;1;69;0
WireConnection;0;2;70;0
WireConnection;0;3;143;0
WireConnection;0;5;76;0
WireConnection;0;9;161;0
ASEEND*/
//CHKSM=6257A3C6E414E95E5088AABE0636812CB8693850