using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForEachLoopsWithArrays : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        int[] arraybees = { 1, 2, 3, 4 };
        foreach (int i in arraybees)
        {
            Debug.Log(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
