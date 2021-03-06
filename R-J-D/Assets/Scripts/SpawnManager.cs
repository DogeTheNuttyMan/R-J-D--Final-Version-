using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] barrierPrefabs;
    private float spawnX = -3;
    private float spawnTime = 3;
    private float obstacleInterval = 1;
    private PlayerController playerControllerScript;
    public Button startButton;
    public Button developerButton;
    public TextMeshProUGUI startGameText;
    public GameObject titleScreen;

    // Start is called before the first framdde update
    void Start()
    {
        //InvokeRepeating("spawnObstacle", spawnTime, obstacleInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(startGame);
        startButton.gameObject.SetActive(false);

        developerButton = GetComponent<Button>();
        developerButton.onClick.AddListener(OpenChannel);
        developerButton.gameObject.SetActive(false);
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
    public void startGame()
    {
        startGameText.gameObject.SetActive(false);
        InvokeRepeating("spawnObstacle", spawnTime, obstacleInterval);
        titleScreen.gameObject.SetActive(false);
    }

    public void OpenChannel()
    {
        Application.OpenURL("https://github.com/DogeTheNuttyMan/R-J-D--Final-Version-");
    }
}
