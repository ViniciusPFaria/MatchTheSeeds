Shader "Custom/Slider"
{
	Properties
	{
		_MainTex("BackGround", 2D) = "white" {}
		_SliderTex("SliderFull", 2D) = "white" {}
		_Blender("Blender Factor", Range(0,1)) = 1
	}
		SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			
		}
			LOD 100
			Blend One OneMinusSrcAlpha

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
						fixed4 color : COLOR;
					};

					

					//estrutura para passar as informacoes do appdata
					struct v2f
					{
						float2 uv : TEXCOORD0;
						float4 vertex : SV_POSITION;
						fixed4 color : COLOR;
					};

					//variaveis tem que ser declaradas duas vezes. Uma na unity lab(properties) e outra na CG (aqui)
					sampler2D _MainTex;
					sampler2D _SliderTex;
					float _Blender;

					//nao sei pq da erro se nao declarar isso
					float4 _MainTex_ST;// Unity provides value for float4 with "_ST" suffix. The x,y contains texture scale, and z,w contains translation (offset).

					//monta as coisas (usada para manipular posições e afins)
					v2f vertexFuntion(appdata IN)//appdata é a data que pegamos do obj ali em cima e v2f é a estrutura para guarda essa data
					{
						v2f OUT;
						OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
						OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex); 
						OUT.color = IN.color;
						OUT.uv = IN.uv;
						return OUT;
					}

					//copie da sharder default da unity
					fixed4 SampleSpriteTexture(float2 uv, sampler2D text)
					{
						fixed4 color = tex2D(text, uv);

#if ETC1_EXTERNAL_ALPHA
						// get the color from an external texture (usecase: Alpha support for ETC1 on android)
						color.a = tex2D(_AlphaTex, uv).r;
#endif //ETC1_EXTERNAL_ALPHA

						return color;
					}

					//colore as coisas e modifica texturas
					fixed4 fragFuntion(v2f IN) : SV_Target
					{
						
						fixed4 color = SampleSpriteTexture(IN.uv, _MainTex) * IN.color;
						fixed4 color2 = SampleSpriteTexture(IN.uv, _SliderTex) * IN.color;

						color.rgb *= color.a;
						color2.rgb *= color2.a;
						
						

						if (_Blender < IN.uv.x)
							return color;
						else return color2;
						
					}

					
			ENDCG//fecha a parte de CG e volta para a parte de unity lab
		}
	}
}