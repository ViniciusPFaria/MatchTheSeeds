using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLine : MonoBehaviour {

    public GameObject prefabBlock;
    public LayerMask layer;
	
    void Start()
    {
        for(int cont = 0; cont<5;cont++ )
            setGem();
    }

    public void setGem()
    {
        PoolGems.newGemFromPool(transform);
    }
}
