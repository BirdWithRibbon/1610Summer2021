using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;

    //private float startDelay = 1.0f;
    //private float spawnInterval = 4.0f;
    private float spawnTimer = 4;
    private float currentSpawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnRandomBall", startDelay, spawnInterval);
    }

    void Update()
    {
        currentSpawnTime = currentSpawnTime + Time.deltaTime;
        if (currentSpawnTime >= spawnTimer)
        {
            currentSpawnTime = 0;
            spawnTimer = Random.Range(3, 6);
            SpawnRandomBall();
        } 
    }
    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall ()
    {
        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);
        int rando = Random.Range(0, 3);

        // instantiate ball at random spawn location
        Instantiate(ballPrefabs[rando], spawnPos, ballPrefabs[rando].transform.rotation);
    }
}