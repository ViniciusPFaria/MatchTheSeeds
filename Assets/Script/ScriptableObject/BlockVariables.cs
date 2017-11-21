using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BlockVariables : ScriptableObject
{

    public enum BlockType { Green, Blue, Purple, Orange }
    //public enum BlockType { Green, Blue, Purple, Orange }
    public BlockType blockType;
    //public Color cor;
    public Sprite imagemBlock;
    private int liberarLetra = 600;

    private float forcaType = 0;
    private float auxLiberarLetra = 0;

    private SliderGetForce mySlider;

    public void listening(SliderGetForce blockSlider)
    {
        mySlider = blockSlider;
    }


    public void addForca(float value, bool isRespostaType)
    {
        forcaType += value;
        auxLiberarLetra += value;

        if (mySlider)//se tiver slider é porque é cena de resposta
        {
            mySlider.changeForce(forcaType);
        }

        if (!isRespostaType)
            checkLiberaLetra();
    }

    void checkLiberaLetra()
    {
        if (auxLiberarLetra > liberarLetra)
        {
            auxLiberarLetra = auxLiberarLetra - liberarLetra;

            switch (blockType)
            {
                case BlockType.Blue: TextQuestion._MYInstance.liberarLetra(0); break;//re adicionar
                case BlockType.Green: TextQuestion._MYInstance.liberarLetra(1); break;
                case BlockType.Purple: TextQuestion._MYInstance.liberarLetra(2); break;
                case BlockType.Orange: TextQuestion._MYInstance.liberarLetra(3); break;
            }

            checkLiberaLetra();//chama recursivamente ate nao entrar no if
        }
    }

    public void resetarForca()
    {
        forcaType = 0;
    }

    //public Color getTypeColor()
    //{
    //    switch (blockType)
    //    {
    //        case BlockType.ar: return Color.cyan;
    //        case BlockType.chuva: return Color.blue;
    //        case BlockType.terra: return Color.yellow;
    //        case BlockType.fogo: return Color.red;
    //        default: return Color.black;
    //    }

    //}


}
