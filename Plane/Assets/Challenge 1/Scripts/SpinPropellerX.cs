using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPropellerX : MonoBehaviour
{
    private float propSpeed = 1000f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, propSpeed * Time.deltaTime);
    }
}
