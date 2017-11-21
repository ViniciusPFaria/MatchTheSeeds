using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPlants : MonoBehaviour {

    public float fatorReproducao = 1;
    public GameObject plataPrefab;

	// Use this for initialization
	void Start () {

        InvokeRepeating("reproduzir", 1, 1);
	}
	
	
    void reproduzir()
    {
        Instantiate(plataPrefab);
    }
}
