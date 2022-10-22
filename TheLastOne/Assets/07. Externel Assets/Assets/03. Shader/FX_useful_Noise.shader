// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/FX_useful_Noise"
{
	Properties
	{
		_DistortTex1("DistortTex1", 2D) = "white" {}
		_DistortTex2("DistortTex2", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _tex4coord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float2 uv_texcoord;
			float4 uv_tex4coord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _DistortTex1;
		uniform float4 _DistortTex1_ST;
		uniform sampler2D _DistortTex2;
		uniform float4 _DistortTex2_ST;
		uniform sampler2D _TextureSample0;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv0_DistortTex1 = i.uv_texcoord * _DistortTex1_ST.xy + _DistortTex1_ST.zw;
			float2 panner4 = ( 1.0 * _Time.y * float2( 0,0 ) + uv0_DistortTex1);
			float2 uv0_DistortTex2 = i.uv_texcoord * _DistortTex2_ST.xy + _DistortTex2_ST.zw;
			float2 panner8 = ( 1.0 * _Time.y * float2( 0.01,-0.01 ) + uv0_DistortTex2);
			float2 _Vector3 = float2(0,0.73);
			float2 panner21 = ( 1.0 * _Time.y * float2( -0.01,-0.01 ) + i.uv_texcoord);
			float4 tex2DNode28 = tex2D( _TextureSample0, panner21 );
			float smoothstepResult32 = smoothstep( 0.0 , ( _Vector3.x + i.uv_tex4coord.z ) , tex2DNode28.r);
			float4 temp_output_13_0 = ( tex2D( _DistortTex1, ( panner4 + ( 0.02895365 * (-1.0 + (tex2D( _DistortTex2, panner8 ).r - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) ) ).r * (-1.0 + (smoothstepResult32 - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) * ( tex2DNode28.r * ( _Vector3.y + i.uv_tex4coord.w ) ) * i.vertexColor );
			o.Emission = temp_output_13_0.rgb;
			o.Alpha = temp_output_13_0.a;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18301
7;6;1906;1005;1323.44;-274.5845;1.733173;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-2442.013,354.26;Inherit;False;0;6;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;9;-2415.013,688.2598;Inherit;False;Constant;_Vector1;Vector 1;2;0;Create;True;0;0;False;0;False;0.01,-0.01;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;8;-2173.012,503.2598;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-2067.371,908.1914;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;23;-2069.882,1177.178;Inherit;False;Constant;_Vector2;Vector 2;3;0;Create;True;0;0;False;0;False;-0.01,-0.01;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SamplerNode;6;-1893.121,479.5234;Inherit;True;Property;_DistortTex2;DistortTex2;1;0;Create;True;0;0;False;0;False;-1;441d8bb42833455b8b10dd26ce59fffd;441d8bb42833455b8b10dd26ce59fffd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;25;-1229.688,1089.92;Inherit;False;Constant;_Vector3;Vector 3;3;0;Create;True;0;0;False;0;False;0,0.73;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TFHCRemapNode;14;-1584.002,512.7826;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;26;-1243.929,1400.313;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;21;-1823.882,1028.178;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-1688.277,-347.6915;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;5;-1675.5,3.899426;Inherit;False;Constant;_Vector0;Vector 0;1;0;Create;True;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;15;-1435.08,256.1649;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;False;0.02895365;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-885.4447,1067.17;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-1219.444,462.0856;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;4;-1334.785,57.29021;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;28;-1505.008,987.2375;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;False;-1;441d8bb42833455b8b10dd26ce59fffd;441d8bb42833455b8b10dd26ce59fffd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;27;-863.7025,1289.02;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;32;-705.05,772.0476;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-976.0767,323.5547;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-631.7371,1008.455;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;33;-437.55,798.4474;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-732.1256,401.5117;Inherit;True;Property;_DistortTex1;DistortTex1;0;0;Create;True;0;0;False;0;False;-1;126c2cad90f59334d97a8db3505f7a19;126c2cad90f59334d97a8db3505f7a19;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;34;-213.6219,1048.355;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-33.56647,720.7352;Inherit;True;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;39;-97.52846,1289.299;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;38;144.8457,1219.172;Inherit;True;Property;_MaskTex;MaskTex;3;0;Create;True;0;0;False;0;False;-1;126c2cad90f59334d97a8db3505f7a19;74bbd945fd31fee4ba7280791dd716b8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;35;311.6814,798.6934;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1075.107,408.0112;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Custom/FX_useful_Noise;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;0;7;0
WireConnection;8;2;9;0
WireConnection;6;1;8;0
WireConnection;14;0;6;1
WireConnection;21;0;20;0
WireConnection;21;2;23;0
WireConnection;24;0;25;1
WireConnection;24;1;26;3
WireConnection;16;0;15;0
WireConnection;16;1;14;0
WireConnection;4;0;2;0
WireConnection;4;2;5;0
WireConnection;28;1;21;0
WireConnection;27;0;25;2
WireConnection;27;1;26;4
WireConnection;32;0;28;1
WireConnection;32;2;24;0
WireConnection;12;0;4;0
WireConnection;12;1;16;0
WireConnection;30;0;28;1
WireConnection;30;1;27;0
WireConnection;33;0;32;0
WireConnection;1;1;12;0
WireConnection;13;0;1;1
WireConnection;13;1;33;0
WireConnection;13;2;30;0
WireConnection;13;3;34;0
WireConnection;38;1;39;0
WireConnection;35;0;13;0
WireConnection;0;2;13;0
WireConnection;0;9;35;3
ASEEND*/
//CHKSM=FAF912ED0C6D3A0571B6AEF3CEC8E0B31A3BC298