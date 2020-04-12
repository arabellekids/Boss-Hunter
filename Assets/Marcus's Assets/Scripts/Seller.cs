using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Seller : MonoBehaviour
{
    
    public Transform rewrdPos;
    public TextMeshProUGUI[] itemsToSellText;
    [HideInInspector]
    public Item itemSelling;
    ScrapManager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ScrapManager>();
    }
    void Update()
    {
        
    }

    public void Buy(Item item)
    {
        manager.currentScraps -= item.cost;
        Instantiate(item.item, rewrdPos.position, rewrdPos.rotation, rewrdPos);
    }
}
