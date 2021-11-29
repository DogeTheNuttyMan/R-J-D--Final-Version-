using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public bool onRoad;
    public bool gameOver = false;
    private PlayerController playerControllerScript;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        onRoad = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Left
        if (onRoad && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerRb.AddForce(Vector3.left * 3, ForceMode.Impulse);
        }

        if(transform.position.x < -3)
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
            transform.position = new Vector3(3 , transform.position.y, transform.position.z);
        }

        //Z
        if (transform.position.z != -7)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -7);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            onRoad = true;
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log("Game Over!");
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
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
}
