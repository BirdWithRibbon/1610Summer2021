using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Setting some stuff.
    private GameObject playerGO;
    private float spawnBounds = 1.5f;
    private int maxBags = 8;
    private float spawnDelayMin = 0.5f;
    private float spawnDelayMax = 1.5f;
    private float spawnCurrent;
    private float spawnTimer;
    private int attempts;
    private int attemptMax = 300;
    public GameObject bagPrefab;
    private AudioSource gmAudio;
    public int score = 0;
    private float gameWave = 1;
    private float waveTime;
    public bool gameRunning = false;
    public List<GameObject> shelfList = new List<GameObject>();
    private float shelfRange = 0.4f;
    private float playerLives = 3;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalscoreText;
    public GameObject titleScreen;
    public Button restartButton;
    private float resetTime = 5;
    public bool dead = false;
    private float resetCount;



    void Start()
    {
        spawnTimer = Random.Range(spawnDelayMin, spawnDelayMax);
        playerGO = GameObject.Find("Player");
        playerGO.SetActive(false);
    }
    //Summon bag from the deep
    void MoneyBagMake()
    {
        var objTest = Random.Range(0,shelfList.Count);

        if (shelfList[objTest])
        {

        }
        var spawnTarget = new Vector3(Random.Range(-spawnBounds, spawnBounds), 2, 0);
            //float curFloor;
            RaycastHit rayHit;
            LayerMask layerMask = LayerMask.GetMask("Valid");
            layerMask |= (1 << LayerMask.NameToLayer("Filth"));
            if (Physics.Raycast(spawnTarget, Vector3.down, out rayHit, 15, layerMask))
            {
                if (rayHit.transform.gameObject.layer == 6 && rayHit.collider.gameObject.GetComponent<BoxCollider>().name == "validSpawn")
                {
                    //Debug.Log(rayHit.collider.gameObject.GetComponent<BoxCollider>().name);
                    Instantiate(bagPrefab, spawnTarget, bagPrefab.transform.rotation);
                    attempts = 0;
                }
                else if (attempts < attemptMax)
                {
                    attempts++;
                    MoneyBagMake();
                }else if (attempts >= attemptMax)
                {
                }
            }

    }

    void Update()
    {
        if (gameRunning)
        {
            spawnCurrent += Time.deltaTime;
            if (spawnCurrent >= spawnTimer)
            {
                spawnCurrent = 0;
                spawnTimer = Random.Range(spawnDelayMin, spawnDelayMax);
                //Debug.Log(spawnTimer);
                MoneyBagMake();
            }
            scoreText.text = "Lives: " + playerLives;
            timerText.text = "Score: " + score;
        }

        if (playerLives == 0)
        {
            gameRunning = false;
            playerGO.SetActive(false);
            scoreText.text = " ";
            timerText.text = " ";
            gameOverText.gameObject.SetActive(true);
            finalscoreText.text = ""+score;
            dead = true;
        }

        if (dead == true && resetCount < resetTime)
        {
            resetCount += Time.deltaTime;
        }else if (dead == true)
        {
            dead = false;
            playerLives = 3;
            score = 0;
            gameOverText.gameObject.SetActive(false);
            finalscoreText.text = "";
            titleScreen.SetActive(true);
            for (int i = 0; i < shelfList.Count; i++)
            {
                shelfList[i].GetComponent<Shelf>().Respawn();
            }
        }
    }

    public void StartGame()
    {
        gameRunning = true;
        playerGO.SetActive(true);
        titleScreen.SetActive(false);
        scoreText.text = "Lives: " + playerLives;
        timerText.text = "Score: " + score;
    }

    public void LoseLife()
    {
        playerLives--;
    }
    public void IncreaseScore()
    {
        score++;
    }
}
