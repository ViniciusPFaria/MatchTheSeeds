using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LoLSDK;

public class ManagerProgress : MonoBehaviour
{

    private static ManagerProgress _Instance;
    public static ManagerProgress INSTANCE { get { return _Instance; } }

    private int atualLevel = 1;
    public int totalScore = 0;

    public ReferencesPlaceHolderInit referencesInit;

    // Use this for initialization
    void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

  

    

    public void setNewMaxLevel(int questionNumber)
    {
        if (questionNumber < atualLevel)
            return;

        

        atualLevel = questionNumber + 1;
        if (atualLevel > 30)
        {
            atualLevel = 31;
        }

        if(atualLevel<=15)
            LOLSDK.Instance.SubmitProgress(totalScore, atualLevel, 15);
    }

    public int getAtualLevel()
    {
        return atualLevel;
    }

    //arg0=null, arg1 loaded scene
    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        Time.timeScale = 1;
        PoolGems.resetAll(false);//toda vez que troca de scena reseta todas as pecas

        if(arg1.buildIndex == 0)
        {
            referencesInit = GameObject.FindObjectOfType<ReferencesPlaceHolderInit>();
            referencesInit.liberarButton(atualLevel);
        }
    }
}
