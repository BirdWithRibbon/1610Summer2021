using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float dogTimer = 0;
    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog!
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dogTimer <= 0)
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                dogTimer = 0.75f;
            }
        }
        if (dogTimer > 0){
            dogTimer = dogTimer - Time.deltaTime;
        }
        else
        {
            dogTimer = 0;
        }
        
    }
}
