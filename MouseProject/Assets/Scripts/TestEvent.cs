using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    public void TestFunc()
    {
        targetObject.SetActive(false);
    }
}
