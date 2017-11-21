using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

//essa classe nasce como monoBehaviour mas vive só como instancia (dontdestroyOnload para mudar isso)
public class ManagerLevelLoad : MonoBehaviour
{

    private static ManagerLevelLoad _Instance;
    public static ManagerLevelLoad INSTANCE { get { return _Instance; } }

    [HideInInspector]
    public int questionNumber =1;

    public int[] arrayRespostas;

    private string questions;
    private string[] questionsSplit;

    // Use this for initialization
    void Awake()
    {
        _Instance = this;

        arrayRespostas = new int[30] { 3,4,3,1,4,1,2,2,4,2,2,4,3,2,2,3,4,3,3,3,2,3,2,3,3,1,3,4,3,3 };

        string questionPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Questions.txt");

        StartCoroutine(getQuestions(questionPath));
    }

    IEnumerator getQuestions(string filePath)
    {
        if (filePath.Contains("://"))
        {
            WWW www = new WWW(filePath);
            yield return www;
            questions = www.text;
        }
        else
            questions = System.IO.File.ReadAllText(filePath);

        questionsSplit = questions.Split('*');//0 é valor vazio
    }



    public string readQuestion()
    {

        string[] quetionAndAnswers = questionsSplit[questionNumber].Split('.');
        return quetionAndAnswers[0];
    }

    public string readAnswers(int answersNumber)
    {
        string[] quetionAndAnswers = questionsSplit[questionNumber].Split('.');
        return quetionAndAnswers[answersNumber].TrimEnd(new char[] { ' ', '\n' });
        //return quetionAndAnswers[answersNumber];
    }

    public void setQuestion(int question)
    {
        AudioControl.INSTANCE.PlaySound(3);
        questionNumber = question;
    }
}
