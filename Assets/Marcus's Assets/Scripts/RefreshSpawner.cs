using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health))]
public class RefreshSpawner : MonoBehaviour
{
    [HideInInspector]
    public Spawner spawner;

    Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        health.onDie += InformSpawner;
    }

    void InformSpawner()
    {
        spawner.totalObjectsSpawned--;
    }
}
