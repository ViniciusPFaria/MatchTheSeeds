using LoLSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneBackButton : MonoBehaviour {

	

    public void exitGame()
    {
        LOLSDK.Instance.CompleteGame();
    }
}
