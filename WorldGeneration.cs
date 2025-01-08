using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{

    public GameObject playerGO;
    public GameObject playerCamera;
    public GameObject loadingCamera;
    public GameObject buildingTerrainScreen;

    public int sizeX, sizeZ;

    public int groundHeight;

    public float terDetail, terHeight;

    public int seed;

    public GameObject[] worldBlocks;

    public int cubeSize;


    // Start is called before the first frame update
    void Start()
    {
       // playerGO = GameObject.FindGameObjectWithTag("Player");
        seed = Random.Range(100000, 999999);
        GenerateTerrain();
    }

    // Update is called once per frame
    void GenerateTerrain()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                int maxY = (int)(Mathf.PerlinNoise((x / cubeSize + seed) / terDetail, (z / cubeSize + seed) / terDetail) * terHeight);
                maxY += groundHeight;

                GameObject grassBlock = Instantiate(worldBlocks[0], new Vector3(x, maxY, z), Quaternion.identity) as GameObject;
                grassBlock.transform.SetParent(GameObject.FindGameObjectWithTag("Environment").transform);

                for (int y = 0; y < maxY; y++)
                {
                    int dirtLayers = Random.Range(1, 5);
                    if(y >= maxY - dirtLayers)
                    {
                        GameObject dirtBlock = Instantiate(worldBlocks[1], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        dirtBlock.transform.SetParent(GameObject.FindGameObjectWithTag("Environment").transform);
                    }
                    else
                    {
                        GameObject stoneBlock = Instantiate(worldBlocks[2], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        stoneBlock.transform.SetParent(GameObject.FindGameObjectWithTag("Environment").transform);
                    }
    
                }

                if(x == (int)(sizeX / cubeSize) && z == (int)(sizeZ / cubeSize))
                {
                    Instantiate(playerGO, new Vector3(x, maxY + 20, z), Quaternion.identity);
                    Instantiate(playerCamera, new Vector3(x, maxY + 20, z), Quaternion.Euler(new Vector3(45, -90, 0)));
                    loadingCamera.SetActive(false);
                }
            }
        }
    }
}
