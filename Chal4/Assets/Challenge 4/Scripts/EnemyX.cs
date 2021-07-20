using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private GameObject spawnMan;
    private SpawnManagerX spawnies;

    //waveBooster is multiplied by wavecount to determine speed bonus.
    private float waveBooster = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Player Goal");
        
        //Gets Wave Count for Speed Boost
        spawnMan = GameObject.Find("Spawn Manager");
        spawnies = spawnMan.GetComponent<SpawnManagerX>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * (speed + (spawnies.waveCount*waveBooster)) * Time.deltaTime, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }

    }

}
