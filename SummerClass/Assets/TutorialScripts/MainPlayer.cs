using UnityEngine;
using System.Collections;

public class MainPlayer : MonoBehaviour
{
    public string myName;

    //initialization
    void Start()
    {
        Debug.Log("I am alive and my name is " + myName);
    }
}