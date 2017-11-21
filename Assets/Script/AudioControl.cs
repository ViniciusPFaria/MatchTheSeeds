using LoLSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//usando para instanciar alguns objs unicos tb
public class AudioControl : MonoBehaviour
{

    public GameObject prefabBlock;
    public GameObject prefabCanvasMulti;//precisa de um canvas proprio se nao ele acaba sainda da cena de dontdestroy
    public GameObject prefabMulti;

    private static AudioControl _Instance;
    public static AudioControl INSTANCE { get { return _Instance; } }

    void Awake()
    {
        if (_Instance == null)
            _Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        Application.targetFrameRate = -1;
        LOLSDK.Init("Match The Seeds");//start SDK
        PoolGems.StartPool(prefabBlock, prefabMulti, prefabCanvasMulti);
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 1 = mathctheseed, 2 = mainmenu, 3 clickBT1, 4= sfx_success, 5=sfx_fail,6=sfx_cheers, 7 sfx_wrong
    /// ,8 trophyRoom
    /// </summary>
    /// <param name="indexSoundList"></param>
    public void PlaySound(int indexSoundList)
    {
        switch (indexSoundList)
        {
            case 1: LOLSDK.Instance.PlaySound("Music/matchtheseed_theme.mp3", false, true); break;
            case 2: LOLSDK.Instance.PlaySound("Music/matchtheseed_mainmenu.mp3", false, true); break;
            case 3: LOLSDK.Instance.PlaySound("SFX/sfx_click_1.mp3", false, false); break;
            case 4: LOLSDK.Instance.PlaySound("SFX/sfx_success.mp3", false, false); break;
            case 5: LOLSDK.Instance.PlaySound("SFX/sfx_fail.mp3", false, false); break;
            case 6: LOLSDK.Instance.PlaySound("SFX/sfx_cheers.mp3", false, false); break;
            case 7: LOLSDK.Instance.PlaySound("SFX/sfx_wrong.mp3", false, false); break;
            case 8: LOLSDK.Instance.PlaySound("Music/matchtheseed_trophyroom.mp3", false, false); break; 
        }

    }

    public void PlayFXSeletion(int seletionNumber)
    {
        LOLSDK.Instance.PlaySound("SFX/sfx_" + seletionNumber + "x.mp3", false, false);
    }


    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "MathThreeResposta" || SceneManager.GetActiveScene().name == "MathThree")
        {
            stopBackGroundSound();
            PlaySound(1);
        }

        if (SceneManager.GetActiveScene().name == "_Init")
        {
            stopBackGroundSound();
            PlaySound(2);
        }
    }

  public  void stopBackGroundSound()
    {
        LOLSDK.Instance.StopSound("Music/matchtheseed_mainmenu.mp3");
        LOLSDK.Instance.StopSound("Music/matchtheseed_theme.mp3");
        LOLSDK.Instance.StopSound("Music/matchtheseed_trophyroom.mp3");
    }

}
