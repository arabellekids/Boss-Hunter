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
        if(seller.selling && Input.GetButtonDown("BuyItem") && manager.currentScraps - cost >= 0)
        {
            Buy();
        }
        else if(manager.currentScraps - cost < 0 && seller.selling && Input.GetButtonDown("BuyItem"))
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
