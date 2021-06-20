using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditionals : MonoBehaviour
{
    int spagh = 200;
    int spleg = 230;

        // Start is called before the first frame update
        void Start()
        {
            if (spagh == spleg)
            {
            Console.WriteLine(spagh + " is equal to " + spleg);
            }
            else if (spagh < spleg)
            {
            Console.WriteLine(spagh + " is less than " + spleg);
        }
            else
            {
            Console.WriteLine(spagh + " is greater than " + spleg);
        }
        }

        // Update is called once per frame
        void Update()
        {

        }
}