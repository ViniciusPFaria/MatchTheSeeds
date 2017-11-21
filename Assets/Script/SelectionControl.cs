using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionControl : MonoBehaviour
{

    public bool isRespostaType = false;
    public LineRenderer lineRender;
    public LayerMask rayLayer;
    public GameObject canvasMultiplicador;
    public GameObject prefabMultiplicador;
    public GameObject prefabPontos;
    List<Block> selectedObjs = new List<Block>();

    public static bool bugStop = false;

    BlockVariables.BlockType SelectedBlockType;

    public Sprite[] multicaodrArrayBlue;
    public Sprite[] multicaodrArrayRed;
    public Sprite[] multicaodrArrayPink;
    public Sprite[] multicaodrArrayGreen;


    bool seletionStarted = false;
    bool isMobile = false;

    GameObject objPontosCache;

    private void Start()
    {
        objPontosCache = Instantiate(prefabPontos, canvasMultiplicador.transform, false);
        objPontosCache.SetActive(false);
    }

    void Update()
    {

        if (Input.touchCount > 0)
        {
            isMobile = true;
            mainButtonPress(Input.GetTouch(0).position);
        }

        if (Input.GetMouseButton(0) && !isMobile)
        {
            mainButtonPress(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && !isMobile)
        {
            mainButtonRealease();
        }

        if (Input.touchCount < 1 && isMobile)
        {
            isMobile = false;
            mainButtonRealease();
        }

       
       
    }



    void mainButtonPress(Vector3 rawPos)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(rawPos);
        RaycastHit2D hit = Physics2D.CircleCast(mousePos, 0.05f, Vector2.zero, 10, rayLayer);

        if (hit && hit.collider.gameObject.GetComponent<Block>().enabled)
        {
            seletionStarted = true;
            gridButtonClick(hit.collider.gameObject.GetComponent<Block>());
        }
    }

    void mainButtonRealease()
    {
        seletionStarted = false;

        if (selectedObjs.Count > 2)
        {
            if (selectedObjs.Count > 7)
                AudioControl.INSTANCE.PlaySound(6);
            addPontos();

            if (isRespostaType)//se for o gameplay de resposta destroy tudo se nao destroy so os escolhidos
            {
                PoolGems.changeBlockColors();//muda todas as cores em vez de destruir
                //PoolGems.resetAll(true);//destroy todos index
            }
            else
            {
                selectedObjs.ForEach(delegate (Block blockObj)
                {
                    PoolGems.destroyFromPool(blockObj);
                    
                });
            }

        }
        else if (selectedObjs.Count > 0) AudioControl.INSTANCE.PlaySound(7);

        selectedObjs.Clear();
        lineRender.numPositions = 0;
    }


    public void gridButtonClick(Block block)
    {
        if (!selectedObjs.Contains(block))
        {
            lineRenderAddNode(block);
        }
    }

    private void lineRenderAddNode(Block block)
    {
        if (!seletionStarted)
            return;

        int total;
        total = selectedObjs.Count;

        if (total == 0)
        {
            selectedObjs.Add(block);//se for o primeiro adiciona e sai
            SelectedBlockType = block.myVariables.blockType;
            inserteNode();

            return;
        }


        //verifica a distancia entre o novo bloco e o ultimo e se eh do mesmo tipo
        if (Vector2.Distance(block.transform.position, selectedObjs[total - 1].transform.position) < 1.8f &&
            block.myVariables.blockType == SelectedBlockType)
        {
            selectedObjs.Add(block);
            inserteNode();
        }

    }

    //insere um no ao line render e cria o obj de exibir o multiplicador
    private void inserteNode()
    {
        int total;
        total = selectedObjs.Count;


        lineRender.numPositions = total;
        Vector3 blockPos = selectedObjs[total - 1].transform.position;
        blockPos.z = -2;
        lineRender.SetPosition(total - 1, blockPos);

        if (total >= 15)//spirtes só vao até 15x
            total = 15;

        AudioControl.INSTANCE.PlayFXSeletion(total);//so tb so vai ate 15
        //multiplicador
        GameObject obj = PoolGems.multiInstantiate(total, canvasMultiplicador);
        obj.transform.position = Camera.main.WorldToScreenPoint(blockPos);
        if (SelectedBlockType == BlockVariables.BlockType.Orange)
            obj.GetComponent<Image>().sprite = multicaodrArrayRed[total - 1];
        if (SelectedBlockType == BlockVariables.BlockType.Blue)
            obj.GetComponent<Image>().sprite = multicaodrArrayBlue[total - 1];
        if (SelectedBlockType == BlockVariables.BlockType.Green)
            obj.GetComponent<Image>().sprite = multicaodrArrayGreen[total - 1];
        if (SelectedBlockType == BlockVariables.BlockType.Purple)
            obj.GetComponent<Image>().sprite = multicaodrArrayPink[total - 1];
        StartCoroutine("waitAndDestroy", obj);
    }

    private void addPontos()
    {
        float formulaPontos = selectedObjs.Count * 200 * selectedObjs.Count / 2;

        //modificado para encaixar os dois tipos de gameplay
        selectedObjs[0].myVariables.addForca(formulaPontos, isRespostaType);
        if (bugStop)//whithout this the gameobj create a new courotine afeter been disabled
            return;
        //obj  de pontos sendo criado
        Vector3 blockPos = selectedObjs[selectedObjs.Count - 1].transform.position;
        objPontosCache.SetActive(true);
        objPontosCache.transform.position = Camera.main.WorldToScreenPoint(blockPos);
        objPontosCache.GetComponentInChildren<Text>().text = formulaPontos.ToString();
        StartCoroutine("waitAndDestroy", objPontosCache);

    }

    IEnumerator waitAndDestroy(GameObject obj)
    {
        yield return new WaitForSeconds(0.7f);
        obj.SetActive(false);
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}