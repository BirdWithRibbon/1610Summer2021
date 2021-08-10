using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    //Declare Some Variables
    public float curFloor;
    private float priorFloor;

    public GameManager gameManager;

    private Vector3 curPosition;
    private Vector3 heightOffset = new Vector3(0.0f, 0.2f, 0.0f);
    
    private float maxVelocity = 10f;
    private float curVelocity = 0f;
    private float myGravity = -4f;
    private bool inAir = true;
    private bool collected = false;
    private float timeToDie = 0.6f;
    private float dieTime;
    private float killFloor = -3f;

    private SkinnedMeshRenderer bagMesh;
    private Animator bagAnimator;
    private ParticleSystem partSys;
    private AudioSource bagAudio;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bagAnimator = GetComponent<Animator>();
        bagAudio = GetComponent<AudioSource>();
        curPosition = transform.position;
        partSys = GetComponent<ParticleSystem>();
        bagMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        partSys.Stop();
        RayCaster();
        priorFloor = curFloor;
    }

    // Update is called once per frame
    void Update()
    {
       if (transform.position.y < killFloor)
        {
            Destroy(gameObject);
        }
        if (collected == false)
        {
            curPosition = transform.position;

            RayCaster();

            if (transform.position.y <= priorFloor)
            {
                inAir = false;
                bagAnimator.SetBool("isFalling", false);
            }
            else
            {
                inAir = true;
                bagAnimator.SetBool("isFalling", true);
            }

            //Determine Gravity
            if (inAir)
            {
                curVelocity = myGravity * Time.deltaTime;
                curVelocity = Mathf.Clamp(curVelocity, -maxVelocity, maxVelocity);
                transform.position = transform.position + new Vector3(0, curVelocity, 0);
            }
            else
            {
                curVelocity = 0;
                transform.position = new Vector3(transform.position.x, priorFloor, transform.position.z);
            }

            //Move
            priorFloor = curFloor;
        }
        else if (dieTime < timeToDie)
        {
            dieTime += Time.deltaTime;
        }
        else
        {
            //Debug.Log("I AM DEAD!");
            Destroy(gameObject);
        }

        if (!gameManager.gameRunning)
        {
            Destroy(gameObject);
        }

    }

    //RayCaster
    void RayCaster()
    {
        RaycastHit rayHit;
        LayerMask layerMask = LayerMask.GetMask("Game");
        if (Physics.Raycast(curPosition + heightOffset, Vector3.down, out rayHit, 2000, layerMask))
        {
            curFloor = Mathf.Round(rayHit.point.y * 1000) * 0.001f;
        }
        else
        {
            curFloor = -2000;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mouse") && !collected)
        {
            Collected();
        }
    }

    void Collected()
    {
        collected = true;
        bagAudio.Play();
        bagMesh.enabled = false;
        partSys.Play();
        gameManager.IncreaseScore();
        //WaitForSeconds(2);
        //Destroy(gameObject);
        //Destroy(gameManager);
    }
}
