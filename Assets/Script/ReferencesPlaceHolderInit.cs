using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferencesPlaceHolderInit : MonoBehaviour {

    public Button[] buttonsArray;
    public Image[] trophyGameObjects;
    public Sprite[] trophyActiveArray;

    public Text liberadosText;
    public Text liberadosLastArvore;
    public GameObject esterEggButton;


    public void liberarButton(int atualLevel)
    {
        if (atualLevel == 31)
            atualLevel = 30;
        for (int cont = 0; cont < atualLevel-1; cont++)
        {
            //buttonsArray[cont].interactable = true;
            buttonsArray[cont].image.color = Color.green;
        }

        buttonsArray[atualLevel-1].interactable = true;
    }

    public void liberarArvores()
    {
        int atualLevel = ManagerProgress.INSTANCE.getAtualLevel();
        atualLevel--;//-1 pq o level atual nao esta liberado
        liberadosText.text = atualLevel + "/15";

        if (atualLevel == 15)
        {
            liberadosLastArvore.text = atualLevel + "/15";
            esterEggButton.SetActive(true);
        }

        int contReverse = 28;//estou liberando duas arvores por vez
        for (int cont = 0; cont < 15; cont++)
        {
            

            if (cont < atualLevel)
            {
                trophyGameObjects[cont].sprite = trophyActiveArray[cont];
                trophyGameObjects[cont].enabled = true;

                trophyGameObjects[contReverse].sprite = trophyActiveArray[contReverse];
                trophyGameObjects[contReverse].enabled = true;
                contReverse--;
            }
            else trophyGameObjects[cont].enabled = false;
        }
    }
}
