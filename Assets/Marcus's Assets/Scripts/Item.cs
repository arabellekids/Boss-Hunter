using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public GameObject item;
    public int cost = 1;
    public string itemName;
    public Seller seller;
    public TextMeshProUGUI itemsToSellText;

    ScrapManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ScrapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Buy()
    {
        manager.currentScraps -= cost;
        Instantiate(item, seller.rewrdPos.position, seller.rewrdPos.rotation, seller.rewrdPos);
    }
}
