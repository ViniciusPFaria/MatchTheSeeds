using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LoLSDK;

public class ManagerUI : MonoBehaviour {

    public Text questionText, AnswersText1, AnswersText2, AnswersText3, AnswersText4;
    public GameObject telaAcerto, telaErro, telaTimeOver, seletionControl;
    public TimeCountDown timerCountDownObj;
    public Image stickImg;
    public Text stickContText;
    public Text earnPointsText;
    public Text TopBarText;
    public Sprite[] stickSpriteList;

    private int earnPoints = 1000;
    private int contError = 0;
	
	/// <summary>
    /// 1=next,2=menu,3Retry
    /// </summary>
    /// <param name="opcaoIndex"></param>
    public void LoadSceneMenuFinal(int opcaoIndex)
    {
        if (ManagerLevelLoad.INSTANCE.questionNumber == 15)
        {
            if(opcaoIndex==1 || opcaoIndex == 2)
            {
                SceneManager.LoadScene("LastScene");
                return;
            }
        }

        if (opcaoIndex == 1)
        {
            if (ManagerProgress.INSTANCE.getAtualLevel() == 11)
            {
                SceneManager.LoadScene("_Init");
                return;
            }
            int randLevel = Random.Range(0, 2);
            string levelName = "";
            if (randLevel == 0)
                levelName = "MathThreeResposta";
            else levelName = "MathThree";


            ManagerLevelLoad.INSTANCE.setQuestion(
            ManagerLevelLoad.INSTANCE.questionNumber + 1);

            SceneManager.LoadScene(levelName);
        }
        else if (opcaoIndex == 2)
            SceneManager.LoadScene("_Init");
        else SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void setStrings()
    {
        questionText.text = ManagerLevelLoad.INSTANCE.readQuestion();
        AnswersText1.text = ManagerLevelLoad.INSTANCE.readAnswers(1);
        AnswersText2.text = ManagerLevelLoad.INSTANCE.readAnswers(2);
        AnswersText3.text = ManagerLevelLoad.INSTANCE.readAnswers(3);
        AnswersText4.text = ManagerLevelLoad.INSTANCE.readAnswers(4);
    }

    void OnLevelWasLoaded(int level)
    {
        if (ManagerLevelLoad.INSTANCE.questionNumber > 30)
            return;

                setStrings();
    }

    public void checkAwerns(int number)
    {
        int questionNumber = ManagerLevelLoad.INSTANCE.questionNumber;
        if (ManagerLevelLoad.INSTANCE.arrayRespostas[questionNumber - 1] == number)
        {//tela acerto
            Time.timeScale = 0;
            setStickImg();
            telaAcerto.SetActive(true);

            int totalScore = ManagerProgress.INSTANCE.totalScore;
            int levelScore = earnPoints - 250 * contError;
            int timeScore = timerCountDownObj.time * 4;
            string topbarTextString = "My score: " + totalScore + "\n <color=#00ff00ff>+" + levelScore +
                " Level Score" + " \n +" + timeScore + " Time score</color> \n" + "= New score: " + (totalScore + levelScore + timeScore);

            TopBarText.text = topbarTextString;

            ManagerProgress.INSTANCE.totalScore += timeScore+ earnPoints - (250 * contError);//adiciona a pontuacao menos o total de erros
            SelectionControl.bugStop = true;
            seletionControl.SetActive(false);
            ManagerProgress.INSTANCE.setNewMaxLevel(questionNumber);

            AudioControl.INSTANCE.PlaySound(4);
        }
        else//tela error
        {
            //Time.timeScale = 0; nao pausa mais. só avisa que perdeu ponto e volta

            contError++;
            setEarnsPointsText();

            telaErro.SetActive(true);
            StartCoroutine(desativarErrorScreen());

            SelectionControl.bugStop = true;
            PoolGems.resetMulti();
            seletionControl.SetActive(false);

            AudioControl.INSTANCE.PlaySound(5);

        }

    }

    public void buttonAwerns(Button btOption)
    {
        btOption.interactable = false;
        btOption.GetComponentInChildren<Image>().color = new Color(0.35f, 0.35f, 0.35f);
    }

    private IEnumerator desativarErrorScreen()
    {
        yield return new WaitForSeconds(3);
        telaErro.SetActive(false);

        SelectionControl.bugStop = false;
        seletionControl.SetActive(true);
    }
    public void timeOver()
    {//tempo nao acaba mais fica travado em 1 ou 0. Tempo so vale ponto
        //telaTimeOver.SetActive(true);
        //SelectionControl.bugStop = true;
        //seletionControl.SetActive(false);
        //AudioControl.INSTANCE.PlaySound(5);
        //Time.timeScale = 0;
    }

    public void setSound(bool isOn)
    {
        if (isOn)
            LOLSDK.Instance.ConfigureSound(0, 0, 0);
        else
            LOLSDK.Instance.ConfigureSound(1, 1, 1);
    }


    public void setStickImg()
    {
        stickContText.text = "Trophy Unlocked \n " + ManagerProgress.INSTANCE.getAtualLevel() + " / 15";
        stickImg.sprite = stickSpriteList[ManagerProgress.INSTANCE.getAtualLevel() - 1];

    }

    private void setEarnsPointsText()
    {
        earnPointsText.text = "Earn " + (earnPoints - 250 * contError) + " Points";
    }

    public void resetGems(Button btReset)
    {
        PoolGems.changeBlockColors();
        btReset.GetComponent<Image>().color = Color.gray;
        btReset.interactable = false;

        StartCoroutine(cooldownResetOver(btReset));
    }

    IEnumerator cooldownResetOver(Button btReset)
    {
        yield return new WaitForSeconds(2);
        btReset.GetComponent<Image>().color = Color.white;
        btReset.interactable = true;

    }
}
