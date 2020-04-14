using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(Interactable))]
public class Seller : MonoBehaviour
{
    [Header("Item Database")]
    public Item[] items;

    [Header("Seller variables")]
    public Transform itemBoughtPos;
    public GameObject shopUI;
    public GameObject failedBuy;

    Interactable interactable;
    ScrapManager manager;

    public int currentItemIndex = 1;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.onInteract += OpenShop;
        interactable.onExit += ExitShop;

        manager = FindObjectOfType<ScrapManager>();

        shopUI.SetActive(false);
    }

    private void Update()
    {
        if (shopUI.activeSelf && Input.GetButtonDown("ExitSeller"))
        {
            ExitShop();
        }
        if (Input.GetButtonDown("CycleItems"))
        {
            if(Input.GetAxis("CycleItems") > 0)
            {
                currentItemIndex++;
                if (currentItemIndex >= items.Length)
                {
                    currentItemIndex = 0;
                }
            }
            if (Input.GetAxis("CycleItems") < 0)
            {
                currentItemIndex--;
                if (currentItemIndex < 0)
                {
                    currentItemIndex = items.Length - 1;
                }
            }
        }

        if (Input.GetButtonDown("BuyItem"))
        {
            BuyItem(currentItemIndex);
        }

        foreach(Item item in items)
        {
            item.itemUIImage.color = item.defaultColor;
        }

        items[currentItemIndex].itemUIImage.color = items[currentItemIndex].selectedColor;
    }

    void OpenShop()
    {
        shopUI.SetActive(true);
        Debug.Log("interacting");
    }
    public void ExitShop()
    {
        shopUI.SetActive(false);
        currentItemIndex = 0;
        Debug.Log("exiting");
    }
    void BuyItem(int index)
    {
        if(manager.currentScraps - items[index].cost >= 0)
        {
            manager.currentScraps -= items[index].cost;
            Instantiate(items[index].item, itemBoughtPos.position, itemBoughtPos.rotation, itemBoughtPos);
        }
        else if(GameObject.FindGameObjectsWithTag("Message").Length == 0)
        {
            Instantiate(failedBuy);
        }
    }
}
