using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public UnityAction onInteract;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            onInteract.Invoke();
        }
    }
}
