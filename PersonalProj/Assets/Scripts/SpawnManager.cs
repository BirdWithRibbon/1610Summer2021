using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float spawnRange = 18;
    public float timeMin = 2;
    public float timeMax = 3;
    private float currentSpawnTime = 0;
    private float spawnTimer = 2;
    private float attemptAllowance = 20; //This is a desperate attempt to avoid overflows if all shelves are lost.
    private float attemptCount = 0;
    public GameObject bagPrefab;
    // Start is called before the first frame update
    void Start()
    {
        MoneyBagMake();
    }

    void MoneyBagMake()
    {
        
        //Choose a spot.
        var spawnzies = new Vector3(Random.Range(-spawnRange, spawnRange), 30, 0); 
        
        //Check if it will land on a shelf. If not, retry.
        Vector3 fwd = transform.TransformDirection(-Vector3.up);
        RaycastHit hit;
        int layerMask = 1 << 7;
        layerMask = ~layerMask;
        if (Physics.Raycast(spawnzies, fwd, out hit, 200, layerMask))
        {
            Instantiate(bagPrefab, spawnzies, bagPrefab.transform.rotation);
            attemptCount = 0;
        }
        else if (attemptCount < attemptAllowance)
        {
            attemptCount++;
            MoneyBagMake();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime >= spawnTimer)
        {
            currentSpawnTime = 0;
            spawnTimer = Random.Range(timeMin, timeMax);
            MoneyBagMake();
        }
    }
}
