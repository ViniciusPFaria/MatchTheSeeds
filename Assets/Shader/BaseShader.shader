Shader "Custom/BaseShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
	}
		SubShader
	{
			Tags{ "RenderType" = "Opaque" }
			LOD 100

			Pass
		{
			CGPROGRAM//abre a parte de programacao em CG
				#pragma vertex vertexFuntion//define qual funcao de vertex
				#pragma fragment fragFuntion//define qual funcao de fragment

				#include "UnityCG.cginc"

						//pega os dados do objeto para ser usado la na frente
					struct appdata
					{
						float4 vertex : POSITION;
						float2 uv : TEXCOORD0;
					};

					//estrutura para passar as informacoes do appdata
					struct v2f
					{
						float2 uv : TEXCOORD0;
						float4 vertex : SV_POSITION;
					};

					//variaveis tem que ser declaradas duas vezes. Uma na unity lab(properties) e outra na CG (aqui)
					sampler2D _MainTex;
					float4 _MainTex_ST;// Unity provides value for float4 with "_ST" suffix. The x,y contains texture scale, and z,w contains translation (offset).
					float4 _Color;

					//monta as coisas (usada para manipular posições e afins)
					v2f vertexFuntion(appdata IN)//appdata é a data que pegamos do obj ali em cima e v2f é a estrutura para guarda essa data
					{
						v2f OUT;
						OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
						OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
						return OUT;
					}

					//colore as coisas e modifica texturas
					fixed4 fragFuntion(v2f i) : SV_Target
					{
						// sample the texture
						fixed4 tex = tex2D(_MainTex, i.uv);
						return tex * _Color;
					}
			ENDCG//fecha a parte de CG e volta para a parte de unity lab
		}
	}
}
