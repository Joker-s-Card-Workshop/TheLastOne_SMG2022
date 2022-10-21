// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Noise"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_DistortTex("DistortTex", 2D) = "white" {}
		_Vector1("Vector 1", Vector) = (0,0,0,0)
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_DistortAmount("DistortAmount", Range( 0 , 0.1)) = 0.04304536
		_DistortPannerXY("DistortPanner X/Y", Vector) = (0.25,0,0,0)
		_Vector2("Vector 2", Vector) = (0.25,0,0,0)
		_Mask("Mask", 2D) = "white" {}
		[HideInInspector] _tex4coord( "", 2D ) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Background+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		Blend One One , One One
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float4 uv_tex4coord;
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _TextureSample1;
		uniform float2 _Vector2;
		uniform sampler2D _TextureSample0;
		uniform float2 _Vector1;
		uniform float _DistortAmount;
		uniform sampler2D _DistortTex;
		uniform float2 _DistortPannerXY;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 _Vector0 = float2(3.35,3.05);
			float4 temp_cast_0 = (( _Vector0.y + i.uv_tex4coord.w )).xxxx;
			float2 panner27 = ( 1.0 * _Time.y * _Vector2 + i.uv_texcoord);
			float4 smoothstepResult31 = smoothstep( float4( 0,0,0,0 ) , temp_cast_0 , tex2D( _TextureSample1, panner27 ));
			float2 panner11 = ( 1.0 * _Time.y * _Vector1 + i.uv_texcoord);
			float2 panner4 = ( 1.0 * _Time.y * _DistortPannerXY + i.uv_texcoord);
			float4 tex2DNode5 = tex2D( _DistortTex, panner4 );
			float4 tex2DNode1 = tex2D( _TextureSample0, ( panner11 + ( _DistortAmount * (-1.0 + (tex2DNode5.r - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) ) ) );
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			o.Emission = ( smoothstepResult31 * ( _Vector0.x + i.uv_tex4coord.z ) * tex2DNode1 * i.vertexColor * tex2D( _Mask, uv_Mask ) ).rgb;
			o.Alpha = ( tex2DNode1.a * i.vertexColor.a * tex2DNode5.a );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18301
-12;29;1873;999;1735.799;-324.6219;1;True;False
Node;AmplifyShaderEditor.Vector2Node;2;-2538.408,643.1216;Inherit;False;Property;_DistortPannerXY;DistortPanner X/Y;6;0;Create;True;0;0;False;0;False;0.25,0;0,-0.01;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-2572.407,508.1215;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;4;-2323.408,585.1216;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;5;-2071.052,548.5481;Inherit;True;Property;_DistortTex;DistortTex;2;0;Create;True;0;0;False;0;False;-1;31c22b83d43217b4d9d3c7349f1cc160;31c22b83d43217b4d9d3c7349f1cc160;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;26;-3040.35,933.1816;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;25;-3006.351,1068.182;Inherit;False;Property;_Vector2;Vector 2;7;0;Create;True;0;0;False;0;False;0.25,0;0,0.05;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;8;-1810.177,224.9947;Inherit;False;Property;_Vector1;Vector 1;3;0;Create;True;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-1881.677,76.19469;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;6;-1781.322,576.6063;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1959.176,380.6476;Inherit;False;Property;_DistortAmount;DistortAmount;5;0;Create;True;0;0;False;0;False;0.04304536;0.04304536;0;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;23;-2270.185,1353.394;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;18;-2189.593,1179.346;Inherit;False;Constant;_Vector0;Vector 0;5;0;Create;True;0;0;False;0;False;3.35,3.05;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-1626.295,562.1514;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;27;-2791.351,1010.182;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;11;-1646.38,117.7945;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-1819.185,1369.394;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;28;-2597.995,982.6082;Inherit;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;0;False;0;False;-1;31c22b83d43217b4d9d3c7349f1cc160;31c22b83d43217b4d9d3c7349f1cc160;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-1429.332,318.5916;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-1860.028,1156.681;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1201.754,328.9043;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;False;-1;31c22b83d43217b4d9d3c7349f1cc160;31c22b83d43217b4d9d3c7349f1cc160;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;31;-1249.017,1018.143;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;14;-1221.218,589.9303;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;33;-777.0241,651.749;Inherit;True;Property;_Mask;Mask;8;0;Create;True;0;0;False;0;False;-1;None;d00e5288bde489f47848c713e551d485;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-724.1132,401.209;Inherit;True;5;5;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-643.2073,823.0133;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-319.1,335.9;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Custom/Noise;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Opaque;;Background;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;3;0
WireConnection;4;2;2;0
WireConnection;5;1;4;0
WireConnection;6;0;5;1
WireConnection;10;0;9;0
WireConnection;10;1;6;0
WireConnection;27;0;26;0
WireConnection;27;2;25;0
WireConnection;11;0;7;0
WireConnection;11;2;8;0
WireConnection;19;0;18;2
WireConnection;19;1;23;4
WireConnection;28;1;27;0
WireConnection;12;0;11;0
WireConnection;12;1;10;0
WireConnection;16;0;18;1
WireConnection;16;1;23;3
WireConnection;1;1;12;0
WireConnection;31;0;28;0
WireConnection;31;2;19;0
WireConnection;24;0;31;0
WireConnection;24;1;16;0
WireConnection;24;2;1;0
WireConnection;24;3;14;0
WireConnection;24;4;33;0
WireConnection;32;0;1;4
WireConnection;32;1;14;4
WireConnection;32;2;5;4
WireConnection;0;2;24;0
WireConnection;0;9;32;0
ASEEND*/
//CHKSM=CEC974E3A25E41828330FC7CA18E406797BB3AA1