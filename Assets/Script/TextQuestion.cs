using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextQuestion : MonoBehaviour {

    public static TextQuestion _MYInstance;

    private Text textComponete;
    private string questString;
    private string[] arrayStringQuest = new string[4];
    private string auxString;
    int individualSize;

    private int contArray1 =0, contArray2 = 0, contArray3 = 0, contArray4 = 0;
    int stringsize;
    // Use this for initialization
    void Start () {

        _MYInstance = this;

        textComponete = GetComponent<Text>();
        separarQuestString();

        // InvokeRepeating("FPS", 1, 1);
    }
	

    private void separarQuestString()
    {
        questString = textComponete.text;
        questString = questString.Trim();
        stringsize = questString.Length;
        individualSize = (int)(stringsize / 4);

        arrayStringQuest[0] = questString.Substring(0, individualSize);
        arrayStringQuest[1] = questString.Substring(individualSize, individualSize);
        arrayStringQuest[2] = questString.Substring(individualSize * 2, individualSize);
        int mod4 = stringsize % 4;
        switch (mod4)
        {
            case 0:
                arrayStringQuest[3] = questString.Substring(individualSize * 3, individualSize); break;
            case 1:
                arrayStringQuest[3] = questString.Substring(individualSize * 3, individualSize + 1); break;
            case 2:
                arrayStringQuest[3] = questString.Substring(individualSize * 3, individualSize + 2); break;
            case 3:
                arrayStringQuest[3] = questString.Substring(individualSize * 3, individualSize + 3); break;
        }


        auxString = new string('*', stringsize);


        //numeros adicionais sao as strings das tag
        int numTagOpen = 15;
        int numTagClose = 8;
        int numTotalSize = individualSize + numTagOpen + numTagClose;

        auxString = auxString.Insert(0, "<color=#66c2ff>");//blue
        auxString = auxString.Insert(individualSize + numTagOpen, "</color>");

        auxString = auxString.Insert(numTotalSize, "<color=#5cd65c>");//green
        auxString = auxString.Insert(numTotalSize*2 - numTagClose, "</color>");

        auxString = auxString.Insert(numTotalSize*2, "<color=#b300b3>");//purple
        auxString = auxString.Insert(numTotalSize * 3 - numTagClose, "</color>");

        auxString = auxString.Insert(numTotalSize*3, "<color=#ff6600>");//orange
        auxString = auxString.Insert(auxString.Length, "</color>");

        textComponete.text = auxString;
    }
	
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stringQuadrante">qual parte da string vai ser liberada 0,1,2 ou 3</param>
    public void liberarLetra(int stringQuadrante)
    {
        // numeros adicionais sao as strings das tag
        int numTagOpen = 15;
        int numTagClose = 8;
        int numTotalSize = individualSize + numTagOpen + numTagClose;

        int auxCont = 0;
        string letraEscolhida = "";

        switch (stringQuadrante)
        {
            case 0:
                auxCont = contArray1 + numTagOpen;
                if (contArray1 >= individualSize)
                    return;
                letraEscolhida = arrayStringQuest[stringQuadrante][contArray1].ToString();
                break;
            case 1:
                auxCont = contArray2 + numTotalSize + numTagOpen;
                if (contArray2 >= individualSize)
                    return;
                letraEscolhida = arrayStringQuest[stringQuadrante][contArray2].ToString();
                break;
            case 2:
                auxCont = contArray3 + numTotalSize * 2 + numTagOpen;
                if (contArray3 >= individualSize)
                    return;
                letraEscolhida = arrayStringQuest[stringQuadrante][contArray3].ToString();
                break;
            case 3:
                auxCont = contArray4 + numTotalSize * 3 + numTagOpen;
                if (contArray4 >= arrayStringQuest[stringQuadrante].Length)
                    return;
                letraEscolhida = arrayStringQuest[stringQuadrante][contArray4].ToString();
                break;
        }

        auxString = auxString.Remove(auxCont, 1);
        auxString = auxString.Insert(auxCont, letraEscolhida);

        textComponete.text = auxString;

        switch (stringQuadrante)
        {
            case 0: contArray1++; break;
            case 1: contArray2++; break;
            case 2: contArray3++; break;
            case 3: contArray4++; break;
        }
    }

    //void Update()
    //{
    //    textComponete.text = string.Concat("Fps  ", (int) (1 / Time.deltaTime));
    //}
}
