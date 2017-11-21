using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    
    [HideInInspector]
    public BlockVariables myVariables;    

    SpriteRenderer spriteRendererCache;

    public SpawnLine mySpawnLine;

    void Start()
    {
        spriteRendererCache = GetComponent<SpriteRenderer>();
        BlocksControl.INSTANCE.allBlocks.Add(this);
    }

  public  void defineType(SpawnLine mySpawnLine)
    {
        this.mySpawnLine = mySpawnLine;
        
        myVariables = BlocksControl.INSTANCE.getRandomVariables();
        if(spriteRendererCache)
            spriteRendererCache.sprite = myVariables.imagemBlock;
        else
        {
            spriteRendererCache = GetComponent<SpriteRenderer>();
            spriteRendererCache.sprite = myVariables.imagemBlock;
        }
    }

    public void changeColor()
    {
        myVariables = BlocksControl.INSTANCE.getRandomVariables();
        spriteRendererCache.sprite = myVariables.imagemBlock;
    }

   public void OnDisable()
    {
        spriteRendererCache.enabled = false;
        this.enabled = false;
    }

}