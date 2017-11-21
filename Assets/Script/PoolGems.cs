using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolGems : MonoBehaviour
{

    [SerializeField]
    public static GameObject prefabGem;
    public static GameObject prefabMulti;

    private static List<Block> listGems;
    private static List<int> freeIndex;

    private static List<GameObject> multiPoolList;
    static int poolSize = 25;
    // Use this for initialization
    public static void StartPool(GameObject prefab, GameObject prefabMultiPar, GameObject prefabCanvas)
    {



        prefabGem = prefab;
        prefabMulti = prefabMultiPar;
        listGems = new List<Block>();
        multiPoolList = new List<GameObject>();
        freeIndex = new List<int>();
        GameObject canvas = Instantiate(prefabCanvas);
        DontDestroyOnLoad(canvas);

        for (int i = 0; i < poolSize; i++)
        {
            listGems.Add(Instantiate(prefabGem).GetComponent<Block>());
            listGems[i].gameObject.SetActive(false);
            DontDestroyOnLoad(listGems[i]);
            freeIndex.Add(i);
        }


        for(int i = 0; i < 15; i++)
        {
            multiPoolList.Add(Instantiate(prefabMulti, canvas.transform,false));
            multiPoolList[i].SetActive(false);
            DontDestroyOnLoad(multiPoolList[i]);
        }
    }

    static public void newGemFromPool(Transform spawnLine)
    {

        int freeGemIndex = freeIndex[0];
        freeIndex.RemoveAt(0);

        Block nGem = listGems[freeGemIndex];

        nGem.transform.position = spawnLine.position;
        nGem.defineType(spawnLine.GetComponent<SpawnLine>());
        nGem.gameObject.SetActive(true);

        nGem.GetComponent<SpriteRenderer>().enabled = true;//antiga função do blockMask
        nGem.enabled = true;

    }

    static public void destroyFromPool(Block block)
    {
        if (freeIndex.Count > 0)
            freeIndex.Insert(freeIndex.Count - 1, listGems.IndexOf(block));
        if (freeIndex.Count <= 0)
            freeIndex.Insert(freeIndex.Count, listGems.IndexOf(block));

        block.gameObject.SetActive(false);

        newGemFromPool(block.mySpawnLine.transform);
    }

    public static void resetAll(bool reCreate)
    {
        freeIndex.Clear();
        for (int i = 0; i < poolSize; i++)
        {
            listGems[i].gameObject.SetActive(false);
            freeIndex.Add(i);
            if (reCreate)
            {
                newGemFromPool(listGems[i].mySpawnLine.transform);
            }
        }

        for (int i = 0; i < 15; i++)
        {
            multiPoolList[i].SetActive(false);
        }
    }

    public static void changeBlockColors()
    {
        for (int i = 0; i < poolSize; i++)
        {
            listGems[i].changeColor();
        }

    }

    public static void resetMulti()
    {
        for (int i = 0; i < 15; i++)
        {
            multiPoolList[i].SetActive(false);
        }
    }

    public static GameObject multiInstantiate(int index, GameObject uicanvas)
    {
        multiPoolList[index-1].SetActive(true);
        return multiPoolList[index-1];
    }

}
