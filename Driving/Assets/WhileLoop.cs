using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileLoop : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        while (i < 5)
        {
            Debug.Log(i);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
