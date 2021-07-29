using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float spawnBounds = 1.5f;
    private int maxBags = 8;
    private float spawnDelayMin = 0.5f;
    private float spawnDelayMax = 1.5f;
    private float spawnCurrent;
    private float spawnTimer;
    private int attempts;
    private int attemptMax = 20;
    public GameObject bagPrefab;
    private AudioSource gmAudio;
    public int score = 0;


    void Start()
    {
        spawnTimer = Random.Range(spawnDelayMin, spawnDelayMax);
    }
    //Summon bag from the deep
    void MoneyBagMake()
    {
        var spawnTarget = new Vector3(Random.Range(-spawnBounds, spawnBounds), 2, 0);
        //float curFloor;
        RaycastHit rayHit;
        LayerMask layerMask = LayerMask.GetMask("Game");
        layerMask |= (1 << LayerMask.NameToLayer("Filth"));
        if (Physics.Raycast(spawnTarget, Vector3.down, out rayHit, 15, layerMask))
        {
            if (rayHit.transform.gameObject.layer == 6)
            {
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
        spawnCurrent += Time.deltaTime;
        if (spawnCurrent >= spawnTimer)
        {
            spawnCurrent = 0;
            spawnTimer = Random.Range(spawnDelayMin, spawnDelayMax);
            Debug.Log(spawnTimer);
            MoneyBagMake();
        }
    }
}
