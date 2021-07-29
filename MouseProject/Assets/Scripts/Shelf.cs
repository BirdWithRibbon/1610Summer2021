using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    //GameObjects
    private GameObject main;
    private GameObject mainDestroyed;
    private GameObject bracket1;
    private GameObject bracket2;
    private GameObject debris1;
    private GameObject debris2;
    private GameObject debris3;
    private GameObject debris4;

    //Others
    private ParticleSystem debrisParticles;

    //Attributes
    private int maxBags = 3;
    private float respawnTime = 6f;
    private float deathTime = 1f;
    private float currentDeath = 0;
    private float currentRespawn = 0;
    private bool respawning = false;
    public float bagsCount;
    public List<GameObject> currentBags = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        //Setup Own Components
        debrisParticles = GetComponent<ParticleSystem>();

        //Gather Children
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.gameObject.name == "main")
            {
                main = child.gameObject;
            }
            else if (child.gameObject.name == "mainDestroyed")
            {
                mainDestroyed = child.gameObject;
            }
            else if (child.gameObject.name == "bracketLeft")
            {
                bracket1 = child.gameObject;
            }
            else if (child.gameObject.name == "bracketRight")
            {
                bracket2 = child.gameObject;
            }
            else if (child.gameObject.name == "debris1")
            {
                debris1 = child.gameObject;
            }
            else if (child.gameObject.name == "debris2")
            {
                debris2 = child.gameObject;
            }
            else if (child.gameObject.name == "debris3")
            {
                debris3 = child.gameObject;
            }
            else if (child.gameObject.name == "debris4")
            {
                debris4 = child.gameObject;
            }
        }
        mainDestroyed.SetActive(false);
        bracket1.gameObject.GetComponent<BoxCollider>().enabled = false;
        bracket2.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var emission = debrisParticles.emission;
        if (!respawning)
        {
            emission.rateOverTime = currentBags.Count * 2;
        }
        else
        {
            emission.rateOverTime = 0;
        }

        //Check if bags still exist.
        bagsCount = currentBags.Count;

        if (currentBags.Count > 0)
        {
            for (int i = 0; i < currentBags.Count; i++)
            {
                if (currentBags[i] == null)
                {
                    currentBags.RemoveAt(i);
                }
                else if (currentBags[i].transform.position.y < transform.position.y - 0.2f)
                {
                    currentBags.RemoveAt(i);
                }
            }
        }
        if (!respawning)
        {
            //If more bags than is good.
            if (currentBags.Count >= maxBags && currentDeath <= deathTime)
            {
                currentDeath += Time.deltaTime;
            }
            else if (currentBags.Count >= maxBags)
            {
                //currentBags.Clear(); //Clear list and die.
                currentDeath = 0;
                Death();
            }
            else
            {
                currentDeath = 0;
            }
        }
        else if (respawning && currentRespawn <= respawnTime)
        {
            currentRespawn += Time.deltaTime;
        }
        else if (respawning)
        {
            //Respawn();
        }
    }

    void Death()
    {
        //currentBags.Clear();
        currentDeath = 0;
        respawning = true;
        main.SetActive(false);
        mainDestroyed.SetActive(true);
        //debris1.SetActive(true);
        //debris2.SetActive(true);
        bracket1.gameObject.GetComponent<BoxCollider>().enabled = true;
        bracket2.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    void Respawn()
    {
        //currentBags.Clear();
        currentRespawn = 0;
        respawning = false;
        main.SetActive(true);
        mainDestroyed.SetActive(false);
        bracket1.gameObject.GetComponent<BoxCollider>().enabled = false;
        bracket2.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Is this Money?
        if (collision.gameObject.CompareTag("MoneyBag") && !respawning)
        {
            currentBags.Add(collision.gameObject);
        }
    }
}
