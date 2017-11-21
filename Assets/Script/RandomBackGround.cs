using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBackGround : MonoBehaviour {

    public Sprite[] spBackGround;

	// Use this for initialization
	void Start () {
        int rand = Random.Range(0, spBackGround.Length);
        GetComponent<SpriteRenderer>().sprite = spBackGround[rand];
	}
	
}
