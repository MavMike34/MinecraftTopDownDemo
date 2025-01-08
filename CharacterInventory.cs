using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{

    public CharacterMovement cM;
    public bool inInventory;
    public GameObject inventoryCanvas, craftingCanvas;

    public bool grassActive;
    public float grassAmount;
    public Text grassAT;

    public bool dirtActive;
    public float dirtAmount;
    public Text dirtAT;

    public bool stoneActive;
    public float stoneAmount;
    public Text stoneAT;

    public bool cobbleActive;
    public float cobbleAmount;
    public Text cobbleAT;

    public GameObject grass, dirt, stone, wood, woodP, cobble, craftB, furnace;

    public GameObject currentHandBlock;
    public GameObject pickAxeWooden;

    public bool pickWoodActive;
    public float pickWoodDurability;
    public Text pickWoodDurText;

    public Material grassT, dirtT, stoneT, woodT, woodPT, cobbleT, craftBT, furnanceT;


    // Start is called before the first frame update
    void Start()
    {
        cM = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cM.isDead == false)
        {
            if (Input.GetButtonDown("Inventory"))
            {
                inInventory = !inInventory;
            }
        }

        if (inInventory)
        {
            inventoryCanvas.SetActive(true);
            craftingCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            grassAT.text = grassAmount.ToString();
            dirtAT.text = dirtAmount.ToString();
            stoneAT.text = stoneAmount.ToString();
            cobbleAT.text = cobbleAmount.ToString();
            pickWoodDurText.text = pickWoodDurability.ToString() +("/60");
        }

        if (inInventory == false)
        {
            inventoryCanvas.SetActive(false);
            craftingCanvas.SetActive(false);
            if (cM.fps)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if(grassAmount <= 0 && grassActive)
        {
            EmptyHand();
            grassAmount = 0;
            grassActive = false;
        }

        if (dirtAmount <= 0 && dirtActive)
        {
            EmptyHand();
            dirtAmount = 0;
            dirtActive = false;
        }

        if (stoneAmount <= 0 && stoneActive)
        {
            EmptyHand();
            stoneAmount = 0;
            stoneActive = false;
        }

        if (cobbleAmount <= 0 && cobbleActive)
        {
            EmptyHand();
            cobbleAmount = 0;
            cobbleActive = false;
        }

        if(pickWoodDurability <= 0 && pickWoodActive)
        {
            EmptyHand();
            pickWoodDurability = 0;
            pickWoodActive = false;
        }
    }

    public void EmptyHand()
    {
        cM.block = null;
        grassActive = false;
        dirtActive = false;
        stoneActive = false;
        cobbleActive = false;
        pickWoodActive = false;
    }

    public void clickedGrass()
    {
        if(grassAmount > 0)
        {
            cM.block = grass;
            currentHandBlock.GetComponent<MeshRenderer>().material = grassT;
            grassActive = true;
            dirtActive = false;
            stoneActive = false;
            cobbleActive = false;
            pickWoodActive = false;
        }
    }

    public void clickedDirt()
    {
        if (dirtAmount > 0)
        {
            currentHandBlock.GetComponent<MeshRenderer>().material = dirtT;
            cM.block = dirt;
            grassActive = false;
            dirtActive = true;
            stoneActive = false;
            cobbleActive = false;
            pickWoodActive = false;
        }
    }

    public void clickedStone()
    {
        if (stoneAmount > 0)
        {
            currentHandBlock.GetComponent<MeshRenderer>().material = stoneT;
            cM.block = stone;
            grassActive = false;
            dirtActive = false;
            stoneActive = true;
            cobbleActive = false;
            pickWoodActive = false;
        }
    }

    public void clickedCobble()
    {
        if (cobbleAmount > 0)
        {
            currentHandBlock.GetComponent<MeshRenderer>().material = cobbleT;
            cM.block = cobble;
            grassActive = false;
            dirtActive = false;
            stoneActive = false;
            cobbleActive = true;
            pickWoodActive = false;
        }
    }

    public void clickedPickWood()
    {
        if (pickWoodDurability > 0)
        {
            cM.block = null;
            grassActive = false;
            dirtActive = false;
            stoneActive = false;
            cobbleActive = false;
            pickWoodActive = true;
        }
    }

    public void takeAwayPlacedBlocks()
    {
        if (grassActive)
        {
            grassAmount -= 1f;
        }

        if (dirtActive)
        {
            dirtAmount -= 1f;
        }

        if (stoneActive)
        {
            stoneAmount -= 1f;
        }

        if (cobbleActive)
        {
            cobbleAmount -= 1f;
        }
    }
}
