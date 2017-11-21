using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LoLSDK;
using UnityEngine.UI;

public class ManagerMainMenuUI : MonoBehaviour
{
    public GameObject panelMensage;
    public GameObject exitPathButton;
    public Text totalPointsMainMenu;
    private static bool showMensageFirstTIme = true;

    private void Start()
    {
        totalPointsMainMenu.text = string.Concat("Total Score ", ManagerProgress.INSTANCE.totalScore);
    }

    public void LoadLevel()
    {
        int randLevel = Random.Range(0, 2);
        string levelName = "";
        if (randLevel == 0)
            levelName = "MathThreeResposta";
        else levelName = "MathThree";

        SceneManager.LoadScene(levelName);
    }

    public void setSound(bool isOn)
    {
        if (isOn)
            LOLSDK.Instance.ConfigureSound(0, 0, 0);
        else
            LOLSDK.Instance.ConfigureSound(1, 1, 1);
    }

    public void exitGame()
    {
        LOLSDK.Instance.CompleteGame();
    }

    void OnLevelWasLoaded(int level)
    {
        
        //if (ManagerProgress.INSTANCE.getAtualLevel() > 10)//fases agora so vao ate level 15 entao desativei isso
        //{
        //    if (showMensageFirstTIme)
        //    {
        //        panelMensage.SetActive(true);
        //        showMensageFirstTIme = false;
        //    }
        //    exitPathButton.SetActive(true);
        //}
    }

    public void LateUpdate()
    {
        if (panelMensage.activeSelf)
            if (Input.GetMouseButtonDown(0) || Input.touchCount >0)
                panelMensage.SetActive(false);
    }
}
