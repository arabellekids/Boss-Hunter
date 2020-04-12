using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapGiver : MonoBehaviour
{
    public int scrapsToGive = 1;

    Health health;
    ScrapManager manager;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        health.onDie += OnDie;

        manager = FindObjectOfType<ScrapManager>();
    }

    void OnDie()
    {
        manager.currentScraps += scrapsToGive;
    }
}
