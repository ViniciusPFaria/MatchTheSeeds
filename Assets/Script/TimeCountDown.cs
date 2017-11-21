using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountDown : MonoBehaviour {

    public int timeTotal;
    //public ManagerUI managerUIref;
    public int time;
    Text text;
    
    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        text.text = timeTotal.ToString();
        time = timeTotal;
        InvokeRepeating("timeDecrease", 1, 1);
	}
	
	// Update is called once per frame
	void timeDecrease() {

        if (time <= 0)
            return;

        time--;

        text.text = time.ToString();
            //if (time <0)
            //{
            //    timeOver();
            //}
        }
	

    //void timeOver()
    //{
    //    managerUIref.timeOver();
    //}

}
