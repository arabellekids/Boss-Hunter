using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Quest : MonoBehaviour
{
    Interactable interactable;
    public GameObject questMenu;

    private bool questAccepted = false;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.onInteract += AcceptQuest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AcceptQuest()
    {
        if(questAccepted == false && GameObject.FindGameObjectsWithTag("errorMessage").Length == 0)
        {
            Instantiate(questMenu);
            questAccepted = true;
        }
    }
}
