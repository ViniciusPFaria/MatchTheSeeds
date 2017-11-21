using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour {

    public bool isFocus;

    static bool firstTimeJam = true;
    static bool firstTimeFocus = true;

    // Use this for initialization
    void Start () {

        if (firstTimeJam && !isFocus)
        {
            firstTimeJam = false;
            Time.timeScale = 0;
            return;
        }

        if (firstTimeFocus && isFocus)
        {
            firstTimeFocus = false;
            Time.timeScale = 0;
            return;
        }
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetMouseButton(0) || Input.touchCount >0)
        {
            Time.timeScale = 1;
            this.gameObject.SetActive(false);
        }
	}
}
