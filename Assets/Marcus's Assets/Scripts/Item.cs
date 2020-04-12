using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public GameObject item;
    public int cost = 1;
    public string itemText;
    public TextMeshProUGUI itemsToSellText;
    public Transform rewrdPos;
    public Seller seller;
    public int itemIndex = 0;

    ScrapManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ScrapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        itemsToSellText.text = itemText;
        if(seller.selling && Input.GetButtonDown("BuyItem") && manager.currentScraps - cost >= 0 && seller.currentItem == itemIndex)
        {
            Buy();
        }
        else if(manager.currentScraps - cost < 0 && seller.selling && Input.GetButtonDown("BuyItem") && seller.currentItem == itemIndex)
        {
            Instantiate(seller.failedBuy);
        }
    }
    public void Buy()
    {
        manager.currentScraps -= cost;
        Instantiate(item, rewrdPos.position, rewrdPos.rotation, rewrdPos);
        seller.Exit();
        Debug.Log("buying");
    }
}
