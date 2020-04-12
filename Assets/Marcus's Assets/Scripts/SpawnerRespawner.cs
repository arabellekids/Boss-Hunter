using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRespawner : MonoBehaviour
{
    public GameObject guard;
    public Transform[] guardsPos;
    public GameObject spawner;

    public float respawnTime = 20;

    [HideInInspector]
    public float timeOfDeath = Mathf.NegativeInfinity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForRespawn();
    }

    void CheckForRespawn()
    {
        if(spawner.GetComponent<Health>().currentHealth <= 0 && timeOfDeath + respawnTime < Time.time)
        {
            for(var i = 0; i < guardsPos.Length; i++)
            {
                var currentGuard = guardsPos[i].childCount;
                if(currentGuard == 0)
                {
                    Instantiate(guard, guardsPos[i].position, guardsPos[i].rotation, guardsPos[i]);
                }
                else
                {
                    guard.GetComponent<Health>().Heal(9999);
                }
            }
            spawner.SetActive(true);
            spawner.GetComponent<Health>().Heal(9999);
        }
    }
}
