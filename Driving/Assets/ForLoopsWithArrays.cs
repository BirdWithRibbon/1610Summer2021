using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForLoopsWithArrays : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        int[] forintarr = { 1, 2, 3, 4 };
        for (int i = 0; i < forintarr.Length; i++)
        {
            Debug.Log(forintarr[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
