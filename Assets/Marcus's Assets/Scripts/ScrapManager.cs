using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrapManager : MonoBehaviour
{
    public int currentScraps = 0;

    public TextMeshProUGUI scrapText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scrapText.text = "Scraps : " + currentScraps.ToString();
    }
}
