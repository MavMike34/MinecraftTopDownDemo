using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public Material grassM, dirtM, stoneM, cobbleM;

    public bool grass, dirt, stone, cobble;

    public Transform playerTransform;

    public bool getCollected;

    public float collectionSpeed;
    public float collectionDist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (getCollected)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position,collectionSpeed * Time.deltaTime);

            if((transform.position - playerTransform.position).sqrMagnitude <= collectionDist)
            {
                if (grass)
                {
                    playerTransform.gameObject.GetComponent<CharacterInventory>().grassAmount += 1f;
                }

                if (dirt)
                {
                    playerTransform.gameObject.GetComponent<CharacterInventory>().dirtAmount += 1f;
                }

                if (stone)
                {
                    playerTransform.gameObject.GetComponent<CharacterInventory>().stoneAmount += 1f;
                }

                if (cobble)
                {
                    playerTransform.gameObject.GetComponent<CharacterInventory>().cobbleAmount += 1f;
                }

                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerTransform = other.transform;
            getCollected = true;
            
        }
    }
}
