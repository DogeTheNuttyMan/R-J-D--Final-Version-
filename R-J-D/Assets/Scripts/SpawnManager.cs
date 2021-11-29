using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] barrierPrefabs;
    private float spawnX = -3;
    private float spawnTime = 3;
    private float obstacleInterval = 1;
    private PlayerController playerControllerScript;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnObstacle", spawnTime, obstacleInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnObstacle()
    {
        int barrierIndex = Random.Range(0, barrierPrefabs.Length);
        if(barrierIndex != 1)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnX, spawnX), 1, 8);

            if (playerControllerScript.gameOver == false)
            {
                Instantiate(barrierPrefabs[barrierIndex], spawnPos, barrierPrefabs[barrierIndex].transform.rotation);
            }
        }
        else
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnX, spawnX), 2, 8);
            if (playerControllerScript.gameOver == false)
            {
                Instantiate(barrierPrefabs[barrierIndex], spawnPos, barrierPrefabs[barrierIndex].transform.rotation);
            }
        }
    }
}
