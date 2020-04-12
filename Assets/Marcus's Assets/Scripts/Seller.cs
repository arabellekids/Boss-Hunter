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

    public bool selling = false;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.onInteract += Interact;

        SellerUI.SetActive(false);
    }

    private void Update()
    {
        if(selling && Input.GetButtonDown(""))
    }

    void Interact()
    {
        SellerUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        selling = true;
        //Time.timeScale = 0;
    }
    public void Exit()
    {
        SellerUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        selling = false;
        Time.timeScale = 1;
    }
}
