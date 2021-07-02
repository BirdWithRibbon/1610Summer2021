using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 15.0f;
    public float xRange = 25f;
    public GameObject projectilePrefab;

    void Update()
    {
       //Harvest input.
       horizontalInput = Input.GetAxis("Horizontal");
       transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
       
        //Detect if movement will put player character out of bounds. 
       if (transform.position.x < -xRange) {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
       if (transform.position.x > xRange) {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Launch projectile.
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}
