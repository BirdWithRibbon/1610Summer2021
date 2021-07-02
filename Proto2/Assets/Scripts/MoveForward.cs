using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 40.0f;
    //Cause animals to move towards bottom portion of the screen.
        void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
