using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent myEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mouse"))
        {
            myEvent.Invoke();
        }
    }
}
