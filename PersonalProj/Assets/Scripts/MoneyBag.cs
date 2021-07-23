using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    public float value = 1;
    private Vector3 currentPosition;

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        if (currentPosition.y < -20)
        {
            Destroy(this.gameObject);
        }
    }
}
