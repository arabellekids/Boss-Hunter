using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(Interactable))]
public class Seller : MonoBehaviour
{
    Interactable interactable;
    public GameObject SellerUI;
    public int currentItem = 1;
    public int maxItems = 1;

    public bool selling = false;

    public GameObject failedBuy;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.onInteract += Interact;
        interactable.onExit += Exit;

        SellerUI.SetActive(false);
    }

    private void Update()
    {
        if (selling && Input.GetButtonDown("ExitSeller"))
        {
            Exit();
        }
        if (Input.GetButtonDown("CycleItems"))
        {           
            currentItem += (int)Input.GetAxis("CycleItems");
        }
        if(currentItem > maxItems)
        {
            currentItem = 0;
        }
        if(currentItem < 0)
        {
            currentItem = maxItems;
        }
    }

    void Interact()
    {
        SellerUI.SetActive(true);
        //Time.timeScale = 0;
        Debug.Log("interacting");
        selling = true;
    }
    public void Exit()
    {
        SellerUI.SetActive(false);
        //Time.timeScale = 1;
        Debug.Log("exiting");
        selling = false;
    }
}
