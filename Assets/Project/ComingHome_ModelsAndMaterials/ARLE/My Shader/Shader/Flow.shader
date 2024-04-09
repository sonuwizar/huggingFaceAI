// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SEC/CurrentFlow"
{
	Properties
	{
		_MainTexture1("Main Texture", 2D) = "white" {}
		_DistortTexture1("Distort Texture", 2D) = "bump" {}
		_TextureSample1("Texture Sample 0", 2D) = "white" {}
		[HDR]_TintColor1("Tint Color", Color) = (1,0.4196078,0,1)
		_Speed1("Speed", Float) = 0
		_UVDistortIntensity1("UV Distort Intensity", Range( 0 , 0.04)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _TintColor1;
		uniform sampler2D _MainTexture1;
		uniform float _Speed1;
		uniform sampler2D _DistortTexture1;
		uniform float4 _DistortTexture1_ST;
		uniform float _UVDistortIntensity1;
		uniform sampler2D _TextureSample1;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime35 = _Time.y * _Speed1;
			float2 panner36 = ( mulTime35 * float2( -1,-1 ) + float2( 0,0 ));
			float2 uv_TexCoord41 = i.uv_texcoord + panner36;
			float2 uv_DistortTexture1 = i.uv_texcoord * _DistortTexture1_ST.xy + _DistortTexture1_ST.zw;
			float3 tex2DNode40 = UnpackScaleNormal( tex2D( _DistortTexture1, uv_DistortTexture1 ), _UVDistortIntensity1 );
			float mulTime34 = _Time.y * _Speed1;
			float2 panner37 = ( mulTime34 * float2( 1,0.5 ) + float2( 0,0 ));
			float2 uv_TexCoord39 = i.uv_texcoord + panner37;
			float4 temp_output_48_0 = ( _TintColor1 * ( tex2D( _MainTexture1, ( float3( uv_TexCoord41 ,  0.0 ) + tex2DNode40 ).xy ) * tex2D( _TextureSample1, ( float3( uv_TexCoord39 ,  0.0 ) + tex2DNode40 ).xy ) ) );
			o.Albedo = temp_output_48_0.rgb;
			o.Emission = temp_output_48_0.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
620;411;1300;608;1013.393;132.5968;2.242209;True;True
Node;AmplifyShaderEditor.RangedFloatNode;33;-1883.266,672.9502;Float;False;Property;_Speed1;Speed;4;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;35;-1126.927,502.3214;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;34;-1205.25,1104.228;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-1399.851,720.3785;Float;False;Property;_UVDistortIntensity1;UV Distort Intensity;5;0;Create;True;0;0;False;0;False;0;0;0;0.04;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;37;-977.1146,976.6133;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0.5;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;36;-933.0466,362.6284;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;39;-755.7166,968.3276;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;41;-708.7396,361.196;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;40;-1041.241,704.2038;Inherit;True;Property;_DistortTexture1;Distort Texture;2;0;Create;True;0;0;False;0;False;-1;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;43;-496.6686,1062.707;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;42;-461.6082,455.079;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;45;-333.1494,615.8463;Inherit;True;Property;_TextureSample1;Texture Sample 0;2;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;44;-339.9708,431.996;Inherit;True;Property;_MainTexture1;Main Texture;1;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;46;-335.0815,200.2202;Float;False;Property;_TintColor1;Tint Color;3;1;[HDR];Create;True;0;0;False;0;False;1,0.4196078,0,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-13.54779,508.4404;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;31;-791.2538,39.23967;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;215.2521,427.0174;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-91.45929,115.501;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;568.1577,344.3807;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;SEC/CurrentFlow;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;35;0;33;0
WireConnection;34;0;33;0
WireConnection;37;1;34;0
WireConnection;36;1;35;0
WireConnection;39;1;37;0
WireConnection;41;1;36;0
WireConnection;40;5;38;0
WireConnection;43;0;39;0
WireConnection;43;1;40;0
WireConnection;42;0;41;0
WireConnection;42;1;40;0
WireConnection;45;1;43;0
WireConnection;44;1;42;0
WireConnection;47;0;44;0
WireConnection;47;1;45;0
WireConnection;48;0;46;0
WireConnection;48;1;47;0
WireConnection;32;1;31;0
WireConnection;0;0;48;0
WireConnection;0;2;48;0
ASEEND*/
//CHKSM=5BEDDC49AD31EA3284A00E391B37F3C4F07C86AB