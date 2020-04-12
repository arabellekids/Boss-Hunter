using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public UnityAction onInteract;
    public UnityAction onExit;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            onInteract.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        onExit.Invoke();
    }
}
