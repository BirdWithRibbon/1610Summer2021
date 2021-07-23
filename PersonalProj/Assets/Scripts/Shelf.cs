using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public float weightLimit = 3;
    public float respawnTime = 5;
    public float currentWeight = 0;
    public float timeToDie = 1;
    private float currentDieCounter = 0;
    private bool refreshing = false;
    private float refreshTimer = 0;
    public BoxCollider peg1;
    public BoxCollider peg2;
    public List<GameObject> bagsList = new List<GameObject>();
    private MeshRenderer shelfMesh;
    private BoxCollider shelfCollide;
    public ParticleSystem debrisParticles;

    // Start is called before the first frame update
    void Start()
    {
        peg1.isTrigger = true;
        peg2.isTrigger = true;
        shelfMesh = GetComponent<MeshRenderer>();
        shelfCollide = GetComponent<BoxCollider>();
        //debrisParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var emission = debrisParticles.emission;
        emission.rateOverTime = bagsList.Count * 3;
        
        if (bagsList.Count > 0)
        {
            for (int i = 0; i < bagsList.Count; i++)
            {
                if (bagsList[i] == null)
                {
                    bagsList.RemoveAt(i);
                    Debug.Log(bagsList.Count);
                }
            }
        }

        if (bagsList.Count >= weightLimit && currentDieCounter < timeToDie)
        {
            currentDieCounter += Time.deltaTime;
        }else if (bagsList.Count >= weightLimit && currentDieCounter >= timeToDie)
        {
            Shatter();
        }

        if (refreshing == true && refreshTimer < respawnTime)
        {
            refreshTimer += Time.deltaTime;
        }else if (refreshTimer >= respawnTime)
        {
            Refresh();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Is this Money?
        if (collision.gameObject.CompareTag("MoneyBag"))
        {
            bagsList.Add(collision.gameObject);
        }
    }

    void Shatter()
    {
        shelfMesh.enabled = false;
        peg1.isTrigger = false;
        peg2.isTrigger = false;
        shelfCollide.isTrigger = true;
        currentDieCounter = 0;
        bagsList.Clear();
        refreshing = true;
    }
    void Refresh()
    {
        shelfMesh.enabled = true;
        peg1.isTrigger = true;
        peg2.isTrigger = true;
        shelfCollide.isTrigger = false;
        currentDieCounter = 0;
        refreshing = false;
    }
}
