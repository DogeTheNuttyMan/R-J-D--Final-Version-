using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private BoxCollider playerCol;

    public bool onRoad;
    public bool gameOver = false;
    private PlayerController playerControllerScript;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button startButton;
    float timeNow = 0f;
    float startTime = 20f;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI winText;

    public bool hasPowerup = false;
    public GameObject powerupIndicator;
    public GameObject powerupPrefab;

    private float spawnPosX = -3;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip collisionSoundEffect;
    public AudioClip winningSoundEffect;
    private AudioSource playerAudio;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCol = GetComponent<BoxCollider>();
        
        onRoad = true;

        timeNow = startTime;

        Vector3 spawnPos = new Vector3(Random.Range(-spawnPosX, spawnPosX), 1, 8);
        Instantiate(powerupPrefab, spawnPos, powerupPrefab.transform.rotation);

        playerAudio = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {

        timeNow -= 1 * Time.deltaTime;
        countdownText.text = timeNow.ToString("0");

        if (timeNow <= 0)
        {
            timeNow = 0;
        }

        //Left
        if (onRoad && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerRb.AddForce(Vector3.left * 3, ForceMode.Impulse);
        }

        if (transform.position.x < -3)
        {
            transform.position = new Vector3(-3, transform.position.y, transform.position.z);
        }

        //Right
        if (onRoad && Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerRb.AddForce(Vector3.right * 3, ForceMode.Impulse);

        }

        if (transform.position.x > 3)
        {
            transform.position = new Vector3(3, transform.position.y, transform.position.z);
        }

        //Z
        if (transform.position.z != -7)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -7);
        }

        if (timeNow == 0 && gameOver != true)
        {
            gameOver = true;
            winGameCode();
        }
        powerupIndicator.transform.position = transform.position + new Vector3(0, 1, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            GetComponent<BoxCollider>().isTrigger = true;
            StartCoroutine(PowerUpCountdownRoutine());
            StartCoroutine(PowerUpMiniGame());
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
        GetComponent<BoxCollider>().isTrigger = false;

    }

    IEnumerator PowerUpMiniGame()
    {
        yield return new WaitForSeconds(10);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
        GetComponent<BoxCollider>().isTrigger = false;

        Vector3 spawnPos = new Vector3(Random.Range(-spawnPosX, spawnPosX), 1, 8);
        Instantiate(powerupPrefab, spawnPos, powerupPrefab.transform.rotation);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            onRoad = true;
            dirtParticle.Play();
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(collisionSoundEffect, 0.7f);
            dirtParticle.Stop();
            explosionParticle.Play();
            resetGameCode();

        }

        /*
        else if (collision.gameObject.CompareTag("Powerup"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            onRoad = true;
            hasPowerup = true;
            Destroy(collision.gameObject);
            GetComponent<Rigidbody>().isKinematic = false;
        }
        */
    }

    void OnCollisionExit(Collision collison)
    {
        if (collison.gameObject.name == "Road")
        {
            onRoad = false;
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void resetGameCode()
    {
        gameOver = true;
        GetComponent<Rigidbody>().isKinematic = true;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        countdownText.text = timeNow.ToString("0");
    }
    public void winGameCode()
    {
        gameOver = true;
        playerAudio.PlayOneShot(winningSoundEffect, 0.8f);
        GetComponent<Rigidbody>().isKinematic = true;
        winText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
}
