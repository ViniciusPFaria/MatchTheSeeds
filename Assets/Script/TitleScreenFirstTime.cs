using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenFirstTime : MonoBehaviour {

    static bool firstTime = true;

	// Use this for initialization
	void Start () {
        firstTime = false;
	}
	
	void OnEnable()
    {
        if (!firstTime)
            gameObject.SetActive(false);
    }
}
