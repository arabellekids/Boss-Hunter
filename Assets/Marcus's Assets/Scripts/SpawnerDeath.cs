using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDeath : MonoBehaviour
{
    public SpawnerRespawner respawner;
    Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        health.onDie += OnDie;
    }

    void OnDie()
    {
        respawner.timeOfDeath = Time.time;
    }
}
