using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class FloatData : ScriptableObject
{
    public float shipSpeed;
    public float maxSpeed;

    public void ChangeSpeed(float n)
    {
        if ((shipSpeed += n) < maxSpeed)
        {
            shipSpeed += n;
        }
        else
        {
            shipSpeed = maxSpeed;
        }
    }
    public void StopSpeed()
    {
        shipSpeed = 0;
    }

}
