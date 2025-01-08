using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{

    public bool grass;
    public bool dirt;
    public bool stone;
    public bool cobbleStone;

    public bool isBreakable;

    public Transform playerTransform;

    public GameObject breakBlock;
    public Color materialColor;

    public Camera cam;

    public LayerMask collisionMask;

    public float blockLifeSpan;

    public Material d0, d1, d2, d3, d4, d5, d6, d7, d8, d9;

    //public GameObject grassPrefab, dirtPrefab, stonePrefab, cobblePrefab;

    public GameObject droppedBlock;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //materialColor.r = 1;
        //materialColor.g = 1;
        //materialColor.b = 1;
        //materialColor.a = blockLifeSpan;

        if ((transform.position - playerTransform.position).sqrMagnitude <= 10)
        {
            isBreakable = true;
        }

        if((transform.position - playerTransform.position).sqrMagnitude > 10)
        {
            isBreakable = false;
        }

        //if (Input.GetButtonDown("Attack"))
        //{
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //
        //    if (Physics.Raycast(ray, out hit, 1f, collisionMask))
        //    {
       //         SelectedObject();
       //     }
       // }

       if(blockLifeSpan >= 0.1f)
        {
            breakBlock.GetComponent<MeshRenderer>().material = d0;
        }

        if (blockLifeSpan >= 0.2f)
        {
            breakBlock.GetComponent<MeshRenderer>().material = d1;
        }

        if (blockLifeSpan >= 0.3f)
        {
            breakBlock.GetComponent<MeshRenderer>().material = d2;
        }

        if (blockLifeSpan >= 0.4f)
        {
            breakBlock.GetComponent<MeshRenderer>().material = d3;
        }

        if (blockLifeSpan >= 0.5f)
        {
            breakBlock.GetComponent<MeshRenderer>().material = d4;
        }

        if (blockLifeSpan >= 0.6f)
        {
            breakBlock.GetComponent<MeshRenderer>().material = d5;
        }

        if (blockLifeSpan >= 0.7f)
        {
            breakBlock.GetComponent<MeshRenderer>().material = d6;
        }

        if (blockLifeSpan >= 0.8f)
        {
            breakBlock.GetComponent<MeshRenderer>().material = d7;
        }

        if (blockLifeSpan >= 0.9f)
        {
            breakBlock.GetComponent<MeshRenderer>().material = d8;
        }

        if (blockLifeSpan >= 1f)
        {
            breakBlock.GetComponent<MeshRenderer>().material = d9;
        }
    }

    public void OnMouseOver()
    {
        if (isBreakable == true)
        {
            if (Input.GetButton("Attack"))
            {
                blockLifeSpan += Time.deltaTime;
                breakBlock.SetActive(true);
                //breakBlock.GetComponent<MeshRenderer>().material.shader = Shader.Find("HDRenderPipeline/Lit");
                //breakBlock.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", materialColor);

                if (blockLifeSpan >= 1f)
                {
                    if (grass || dirt)

                    {
                        droppedBlock.GetComponent<DroppedItem>().dirt = true;
                        droppedBlock.GetComponent<DroppedItem>().grass = false;
                        droppedBlock.GetComponent<DroppedItem>().cobble = false;
                        droppedBlock.GetComponent<DroppedItem>().stone = false;
                        droppedBlock.GetComponent<MeshRenderer>().material = droppedBlock.GetComponent<DroppedItem>().dirtM;
                        Instantiate(droppedBlock, gameObject.transform.position, Quaternion.identity);
                    }
                    if((stone || cobbleStone) && playerTransform.gameObject.GetComponent<CharacterInventory>().pickWoodActive)
                    {
                        droppedBlock.GetComponent<DroppedItem>().dirt = false;
                        droppedBlock.GetComponent<DroppedItem>().grass = false;
                        droppedBlock.GetComponent<DroppedItem>().cobble = true;
                        droppedBlock.GetComponent<DroppedItem>().stone = false;
                        droppedBlock.GetComponent<MeshRenderer>().material = droppedBlock.GetComponent<DroppedItem>().cobbleM;
                        Instantiate(droppedBlock, gameObject.transform.position, Quaternion.identity);
                    }
                    Destroy(gameObject);

                    if(playerTransform.gameObject.GetComponent<CharacterInventory>().pickWoodActive == true)
                    {
                        playerTransform.gameObject.GetComponent<CharacterInventory>().pickWoodDurability -= 1f;
                    }
                    
                }
                
            }

            if (Input.GetButtonUp("Attack"))
            {
                blockLifeSpan = 0f;
                breakBlock.SetActive(false);
                breakBlock.GetComponent<MeshRenderer>().material = d0;
            }
            
        }
    }

    public void OnMouseExit()
    {
        blockLifeSpan = 0f;
        breakBlock.SetActive(false);
        breakBlock.GetComponent<MeshRenderer>().material = d0;
    }
}
