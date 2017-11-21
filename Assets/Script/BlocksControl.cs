using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksControl : MonoBehaviour {


    private static BlocksControl _Instance;
    public static BlocksControl INSTANCE { get { return _Instance; } }


    public List<BlockVariables> blockList;

    public List<Block> allBlocks;

    void Awake()
    {
        if (_Instance == null)
            _Instance = this;
        else
        {
            print("mais de uma instancia do blocksControl");
            Destroy(this.gameObject);
        }

        allBlocks = new List<Block>();

        //reseta todos os valores
        blockList.ForEach(delegate (BlockVariables block)
        {
            block.resetarForca();
        });
    }

    public BlockVariables getRandomVariables()
    {
        int randomBlock = Random.Range(0, blockList.Count);
        return blockList[randomBlock];
    }
	
}
