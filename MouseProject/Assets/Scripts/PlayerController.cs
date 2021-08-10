using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Controller Input
    private float horizontalInput;
    private float jumpInput;

    //Character Attributes
    private float runSpeed = 2f;
    private float moveThresh = 0.1f;
    private float jumpStrength = 3f;
    private float fallAllowance = 0.1f;
    private float myGravity = -6f;
    private float maxVertSpeed = 3.5f;
    private float maxHoriSpeed = 10f;
    private float stageBottom = -3f;
    private float stageSide = 2.5f;
    private Vector3 heightOffset = new Vector3(0.0f, 0.1f, 0.0f);

    //States
    public bool inAir = true;
    public bool jumpAvailable = false;
    private bool isGameRunning = true;


    //Hold
    private float fallingTime;
    private float curVertSpeed = 0;
    private float curHoriSpeed = 0;
    public float curFloor;
    private float priorFloor;
    private Vector3 curPosition;

    //Components
    private Animator playerAnim;
    private BoxCollider playerCol;
    private ParticleSystem playerPart;

    public GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAnim = GetComponent<Animator>();
        playerCol = GetComponent<BoxCollider>();
        playerPart = GetComponent<ParticleSystem>();
        playerPart.Stop();
        curPosition = transform.position;
        RayCaster();
        priorFloor = curFloor;
    }
    void Update()
    {
        
        //Get Input
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Jump");

        //Set Current Position
        curPosition = transform.position;
        
        //Raycast for Falling;
        RayCaster();

        //Input for Jumping
        if (jumpInput >= .1 && jumpAvailable)
        {
            curVertSpeed = jumpStrength;
            jumpAvailable = false;
        }

        //Is Player in Air?
        if (curPosition.y <= priorFloor && curVertSpeed <= 0)
        {
            inAir = false;
            playerAnim.SetBool("inAir", false);
            transform.position = new Vector3(curPosition.x, priorFloor, curPosition.z);
            fallingTime = 0;
        }
        else
        {
            inAir = true;
            playerAnim.SetBool("inAir", true);
        }

        //Count Airtime
        if (inAir && fallingTime < fallAllowance && jumpAvailable != false)
        {
            fallingTime += Time.deltaTime;
            jumpAvailable = true;
        }
        else if (inAir)
        {
            fallingTime = 0;
            jumpAvailable = false;
        }

        //Fall if in Air
        if (inAir)
        {
            curVertSpeed += myGravity * Time.deltaTime;
            transform.Translate(Vector3.up * curVertSpeed * Time.deltaTime);
            curVertSpeed = Mathf.Clamp(curVertSpeed, -maxVertSpeed, maxVertSpeed);
            playerAnim.SetFloat("airSpeed", curVertSpeed);
        }
        else
        {
            curVertSpeed = 0;
            playerAnim.SetFloat("airSpeed", curVertSpeed);
            jumpAvailable = true;
        }

        //Setup Future
        priorFloor = curFloor;

        //Move Horizontally
        if (Mathf.Abs(horizontalInput) >= moveThresh)
        {
            curHoriSpeed = horizontalInput * Time.deltaTime * runSpeed;
            playerAnim.SetFloat("moveSpeed", Mathf.Abs(horizontalInput));
        }
        else
        {
            curHoriSpeed = 0;
            playerAnim.SetFloat("moveSpeed", 0);
        }
        curHoriSpeed = Mathf.Clamp(curHoriSpeed, -maxHoriSpeed, maxHoriSpeed); //Clamp Speed
        transform.position = transform.position + new Vector3(curHoriSpeed, 0, 0); //Update Position
        
        if (Mathf.Abs(curHoriSpeed) > 0) 
        {
            transform.eulerAngles = new Vector3(0, 90 * Mathf.Sign(curHoriSpeed), 0);
        }

        if (transform.position.y < stageBottom)
        {
            transform.position = Vector3.zero;
            gameManager.LoseLife();
            curHoriSpeed = 0;
            curVertSpeed = 0;
        }

        if (Mathf.Abs(transform.position.x) > stageSide)
        {
            transform.position = new Vector3( (Mathf.Sign(transform.position.x) * -stageSide) + (Mathf.Sign(transform.position.x) * 0.1f), transform.position.y, transform.position.z);
        }
        //Murder Bags
    
    }
    void RayCaster()
    {
        RaycastHit rayHit;
        LayerMask layerMask = LayerMask.GetMask("Game");
        //layerMask |= (1 << LayerMask.NameToLayer("Filth"));
        if (Physics.Raycast(curPosition + heightOffset, Vector3.down, out rayHit, 2000, layerMask))
        {
            curFloor = Mathf.Round(rayHit.point.y*1000) * 0.001f;
        }
        else
        {
            curFloor = -2000;
        }
    }
}
