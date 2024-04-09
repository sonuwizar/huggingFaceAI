// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hologram Simple 2"
{
	Properties
	{
		[HDR]_Hologramcolor("Hologram color", Color) = (0.3973832,0.7720588,0.7410512,0)
		_Speed("Speed", Range( -100 , 100)) = 26
		_ScanLines("Scan Lines", Range( 0 , 10)) = 3
		_scanlinespeed("scan line speed", Range( 0 , 2)) = 1
		_scanlinesintensity("scan lines intensity", Range( 0 , 1)) = 1
		_Opacity("Opacity", Range( 0 , 1)) = 1
		_Fill("Fill", Float) = 1
		_RimNormalMap("Rim Normal Map", 2D) = "white" {}
		_tiling("tiling", Vector) = (1,1,0,0)
		_offset("offset", Vector) = (1,1,0,0)
		_RimPower("Rim Power", Range( 0 , 10)) = 5
		_Intensity("Intensity", Range( 1 , 10)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
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
			float3 worldPos;
			float2 uv_texcoord;
			float3 viewDir;
			INTERNAL_DATA
		};

		uniform float4 _Hologramcolor;
		uniform float _ScanLines;
		uniform float _Speed;
		uniform float _scanlinespeed;
		uniform float _scanlinesintensity;
		uniform sampler2D _RimNormalMap;
		uniform float2 _tiling;
		uniform float2 _offset;
		uniform float _RimPower;
		uniform float _Intensity;
		uniform float _Opacity;
		uniform float _Fill;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
		{
			original -= center;
			float C = cos( angle );
			float S = sin( angle );
			float t = 1 - C;
			float m00 = t * u.x * u.x + C;
			float m01 = t * u.x * u.y - S * u.z;
			float m02 = t * u.x * u.z + S * u.y;
			float m10 = t * u.x * u.y + S * u.z;
			float m11 = t * u.y * u.y + C;
			float m12 = t * u.y * u.z - S * u.x;
			float m20 = t * u.x * u.z - S * u.y;
			float m21 = t * u.y * u.z + S * u.x;
			float m22 = t * u.z * u.z + C;
			float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
			return mul( finalMatrix, original ) + center;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = float3(0,0,1);
			float4 HologramColor32 = _Hologramcolor;
			float3 ase_worldPos = i.worldPos;
			float Speed156 = _Speed;
			float temp_output_13_0 = sin( ( ( ( _ScanLines * ase_worldPos.y ) + (( 1.0 - ( Speed156 * _Time ) )).x ) * ( _scanlinespeed * UNITY_PI ) ) );
			float clampResult15 = clamp( (0.0 + (temp_output_13_0 - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)) , 0.0 , 1.0 );
			float4 lerpResult16 = lerp( float4(1,1,1,0) , float4(0,0,0,0) , clampResult15);
			float2 temp_cast_0 = (( ( ase_worldPos.z / 100.0 ) * _Time.x )).xx;
			float simplePerlin2D137 = snoise( temp_cast_0 );
			float myVarName3146 = ( simplePerlin2D137 * temp_output_13_0 );
			float4 temp_cast_1 = (myVarName3146).xxxx;
			float4 ScanLines30 = ( ( lerpResult16 - temp_cast_1 ) * _scanlinesintensity );
			float2 uv_TexCoord158 = i.uv_texcoord * _tiling + _offset;
			float3 normalizeResult57 = normalize( i.viewDir );
			float dotResult55 = dot( tex2D( _RimNormalMap, ( ( ( Speed156 / 1000.0 ) * _Time ) + float4( uv_TexCoord158, 0.0 , 0.0 ) ).xy ) , float4( normalizeResult57 , 0.0 ) );
			float temp_output_60_0 = pow( ( 1.0 - saturate( dotResult55 ) ) , ( 10.0 - _RimPower ) );
			float Rim65 = temp_output_60_0;
			o.Emission = ( ( HologramColor32 * ( ScanLines30 + Rim65 ) ) * _Intensity ).rgb;
			float3 objectSpaceViewDir186 = ObjSpaceViewDir( float4( 0,0,0,1 ) );
			float3 rotatedValue188 = RotateAroundAxis( float3( 1,0,0 ), objectSpaceViewDir186, float3( 0,0,0 ), 90.0 );
			float3 rotatedValue192 = RotateAroundAxis( float3( 0,0,1 ), objectSpaceViewDir186, float3( 0,0,0 ), 90.0 );
			float3 temp_cast_6 = (_Fill).xxx;
			o.Alpha = ( _Opacity * step( ( ( rotatedValue188 + rotatedValue192 ) + ( ase_worldPos - temp_cast_6 ) ).y , _Fill ) );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows 

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
				surfIN.worldPos = worldPos;
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
1920;0;1920;1019;5403.848;1189.604;4.360794;True;True
Node;AmplifyShaderEditor.CommentaryNode;168;-1574.711,-440.4086;Inherit;False;614.0698;167.2261;Comment;2;6;156;Speed;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1524.711,-388.1825;Float;False;Property;_Speed;Speed;1;0;Create;True;0;0;False;0;False;26;-0.2;-100;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;156;-1194.641,-390.4085;Float;False;Speed;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;170;-3580.149,1002.746;Inherit;False;2874.69;945.603;Comment;29;30;17;18;16;155;15;14;107;2;146;149;137;145;13;11;143;150;144;3;106;105;10;8;27;26;157;197;198;200;Scan Lines;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;157;-3532.412,1527.073;Inherit;True;156;Speed;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;26;-3530.149,1721.282;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-3302.111,1683.458;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;169;-3022.51,-122.5578;Inherit;False;2344.672;617.4507;Comment;20;58;57;119;55;63;62;64;68;60;65;59;66;163;158;162;167;166;165;194;201;Rim;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-3316.449,1404.358;Float;False;Property;_ScanLines;Scan Lines;2;0;Create;True;0;0;False;0;False;3;10;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;8;-3144.014,1653.972;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WorldPosInputsNode;2;-3286.942,1497.704;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;162;-2972.51,-72.55784;Inherit;False;156;Speed;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;106;-2933.574,1541.049;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;6.06;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;200;-2984.27,1803.782;Inherit;False;Property;_scanlinespeed;scan line speed;3;0;Create;True;0;0;False;0;False;1;2;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;105;-2967.331,1657.428;Inherit;False;True;False;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;167;-2720.792,-42.13026;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1000;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;201;-2831.159,355.9892;Inherit;False;Property;_offset;offset;9;0;Create;True;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;194;-2826.976,227.809;Inherit;False;Property;_tiling;tiling;8;0;Create;True;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PiNode;107;-2708.08,1719.039;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;3;-2776.913,1524.271;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;165;-2968.947,36.28125;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;144;-3142.566,1052.745;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;166;-2589.954,11.35542;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TimeNode;143;-3230.055,1197.58;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;158;-2583.859,207.5992;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;150;-2895.002,1104.894;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;100;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-2570.902,1527.722;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;163;-2408.413,69.78291;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SinOpNode;13;-2415.288,1640.896;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;58;-2259.62,197.9229;Float;False;Tangent;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;145;-2800.486,1250.8;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;57;-2054.684,223.904;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;119;-2228.099,-27.17607;Inherit;True;Property;_RimNormalMap;Rim Normal Map;7;0;Create;True;0;0;False;0;False;-1;5b653e484c8e303439ef414b62f969f0;596678c53fd54a640bf95ba7dfafd092;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;137;-2596.563,1210.444;Inherit;False;Simplex2D;False;False;2;0;FLOAT2;100,100;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;14;-2171.723,1570.391;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;15;-1896.081,1559.26;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;55;-1875.59,100.9621;Inherit;False;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;18;-2167.024,1376.706;Float;False;Constant;_Color1;Color 1;2;0;Create;True;0;0;False;0;False;0,0,0,0;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;149;-2233.83,1097.51;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;17;-2185.125,1201.978;Float;False;Constant;_Color0;Color 0;2;0;Create;True;0;0;False;0;False;1,1,1,0;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;63;-1687.319,157.5161;Inherit;False;1;0;FLOAT;1.23;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;16;-1646.931,1396.875;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;146;-1914.769,1055.774;Float;True;myVarName3;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-2158.147,356.4128;Float;False;Property;_RimPower;Rim Power;10;0;Create;True;0;0;False;0;False;5;4.01;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ObjSpaceViewDirHlpNode;186;-622.2875,682.5846;Inherit;False;1;0;FLOAT4;0,0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.OneMinusNode;64;-1518.314,199.8154;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;68;-1742.309,285.1235;Inherit;False;2;0;FLOAT;10;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;155;-1387.046,1255.215;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;197;-1399.456,1617.709;Inherit;False;Property;_scanlinesintensity;scan lines intensity;4;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotateAboutAxisNode;188;-337.364,593.7603;Inherit;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;90;False;2;FLOAT3;1,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldPosInputsNode;175;18.97061,785.9694;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PowerNode;60;-1325.516,223.2159;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;35;-2281.517,-488.6837;Inherit;False;590.8936;257.7873;Comment;2;32;28;Hologram Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;198;-1116.74,1460.527;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RotateAboutAxisNode;192;-339.6433,741.9043;Inherit;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;90;False;2;FLOAT3;0,0,1;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;182;34.12952,946.3016;Float;False;Property;_Fill;Fill;6;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;65;-939.8385,80.3701;Float;False;Rim;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;193;21.93207,653.5233;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;179;229.0158,757.2445;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;30;-944.6351,1252.947;Float;True;ScanLines;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;28;-2231.517,-438.6837;Float;False;Property;_Hologramcolor;Hologram color;0;1;[HDR];Create;True;0;0;False;0;False;0.3973832,0.7720588,0.7410512,0;0.3973832,0.7720588,0.7410512,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;32;-1963.584,-399.5394;Float;False;HologramColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;189;377.7769,502.9274;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;33;-776.9076,-324.7064;Inherit;False;65;Rim;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;114;-796.5459,-504.9459;Inherit;False;30;ScanLines;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;180;652.2482,544.928;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;71;-520.487,-338.0593;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;127;-557.7931,-444.785;Inherit;False;32;HologramColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;132;-542.2174,-228.0388;Float;False;Property;_Intensity;Intensity;11;0;Create;True;0;0;False;0;False;1;1;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-302.4253,-399.4778;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;49;-177.1491,-121.091;Float;True;Property;_Opacity;Opacity;5;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;181;902.1108,560.1104;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;-1083.116,238.4155;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;183;491.3531,-170.295;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;133;-142.8775,-291.8895;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;66;-1368.539,379.8929;Inherit;False;32;HologramColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;759.6725,-425.562;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Hologram Simple 2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;1;False;-1;1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;156;0;6;0
WireConnection;27;0;157;0
WireConnection;27;1;26;0
WireConnection;8;0;27;0
WireConnection;106;0;10;0
WireConnection;106;1;2;2
WireConnection;105;0;8;0
WireConnection;167;0;162;0
WireConnection;107;0;200;0
WireConnection;3;0;106;0
WireConnection;3;1;105;0
WireConnection;166;0;167;0
WireConnection;166;1;165;0
WireConnection;158;0;194;0
WireConnection;158;1;201;0
WireConnection;150;0;144;3
WireConnection;11;0;3;0
WireConnection;11;1;107;0
WireConnection;163;0;166;0
WireConnection;163;1;158;0
WireConnection;13;0;11;0
WireConnection;145;0;150;0
WireConnection;145;1;143;1
WireConnection;57;0;58;0
WireConnection;119;1;163;0
WireConnection;137;0;145;0
WireConnection;14;0;13;0
WireConnection;15;0;14;0
WireConnection;55;0;119;0
WireConnection;55;1;57;0
WireConnection;149;0;137;0
WireConnection;149;1;13;0
WireConnection;63;0;55;0
WireConnection;16;0;17;0
WireConnection;16;1;18;0
WireConnection;16;2;15;0
WireConnection;146;0;149;0
WireConnection;64;0;63;0
WireConnection;68;1;62;0
WireConnection;155;0;16;0
WireConnection;155;1;146;0
WireConnection;188;3;186;0
WireConnection;60;0;64;0
WireConnection;60;1;68;0
WireConnection;198;0;155;0
WireConnection;198;1;197;0
WireConnection;192;3;186;0
WireConnection;65;0;60;0
WireConnection;193;0;188;0
WireConnection;193;1;192;0
WireConnection;179;0;175;0
WireConnection;179;1;182;0
WireConnection;30;0;198;0
WireConnection;32;0;28;0
WireConnection;189;0;193;0
WireConnection;189;1;179;0
WireConnection;180;0;189;0
WireConnection;71;0;114;0
WireConnection;71;1;33;0
WireConnection;126;0;127;0
WireConnection;126;1;71;0
WireConnection;181;0;180;1
WireConnection;181;1;182;0
WireConnection;59;0;60;0
WireConnection;59;1;66;0
WireConnection;183;0;49;0
WireConnection;183;1;181;0
WireConnection;133;0;126;0
WireConnection;133;1;132;0
WireConnection;0;2;133;0
WireConnection;0;9;183;0
ASEEND*/
//CHKSM=1BF16094B100ADB5BDAC56C5D8FA3C16893B1E18