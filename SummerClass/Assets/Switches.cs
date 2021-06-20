using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switches : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int flavor = 33;
        switch (flavor)
        {
            case 1:
                // code block
                break;
            case 33:
                Console.WriteLine("Yummmm!");
                // code block
                break;
            default:
                // code block
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
