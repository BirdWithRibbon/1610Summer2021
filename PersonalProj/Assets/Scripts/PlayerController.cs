using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Statistics
    public float jumpPower = 28.0f;
    public float horizontalSpeed = 12.0f;
    public float gravMod = 4.0f;
    public bool isOnGround = false;
    public bool isFalling = true;
    public float fallTime = 1.0f;

    //These are inputs
    private float horizontalInput;
    private float jumpInput; //Weird choice of using gamepad's topmost face button.


    //Game parameters
    public float stageSideLimit = 20.0f; //When wrapping around sides.
    public float groundFloor = -20.0f; //When falling off stage.
    private Vector3 lastPosition;
    private Vector3 currentPosition;

    //Components
    private Rigidbody playerRb;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravMod;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get current position for future reference.
        currentPosition = transform.position;

        //Harvest current inputs
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Jump");

        //Move
        playerRb.velocity = new Vector3(horizontalInput * 20, playerRb.velocity.y, 0);
        
        //Rotate object based on Input
        if (horizontalInput != 0)
        {
            if (horizontalInput > 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if(horizontalInput < 0)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }

        //Jump if on ground
        if (jumpInput != 0 && isOnGround)
        {
            //Destroy downward momentum.
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, 0);
            //Add force!
            playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isOnGround = false;
            //playerAnim.SetTrigger("Jump_trig");
            //dirtParticle.Stop();
            //playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        //Jump through platforms the lazy way.
        if (playerRb.velocity.y > 0)
        {
            Physics.IgnoreLayerCollision(6, 7, true);
            Physics.IgnoreLayerCollision(6, 10, true);
        }
        else
        {
            Physics.IgnoreLayerCollision(6, 7, false);
            Physics.IgnoreLayerCollision(6, 10, false);
        }


        //Wrapping around stage behavior
        if (currentPosition.x > stageSideLimit)
        {
            transform.position = new Vector3(-stageSideLimit + 2, currentPosition.y, currentPosition.z);
        }
        else if (currentPosition.x < -stageSideLimit)
        {
            transform.position = new Vector3(stageSideLimit - 2, currentPosition.y, currentPosition.z);
        }

        //Collect the player if they fall off stage.
        if (currentPosition.y < groundFloor)
        {
            transform.position = new Vector3(0, 0, 0);
            playerRb.velocity = new Vector3(0, 0, 0);
        }

    }
    
    //Colliding with objects!
    private void OnCollisionEnter(Collision collision)
    {
        //Check to see if player is on ground.
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        //Is this Money?
        if (collision.gameObject.CompareTag("MoneyBag"))
        {
            Destroy(collision.gameObject);
        }
    }

    //When leaving shelf
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
