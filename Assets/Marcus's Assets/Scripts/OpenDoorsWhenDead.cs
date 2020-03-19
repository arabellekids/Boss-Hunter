using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class OpenDoorsWhenDead : MonoBehaviour
{
    Health health;
    public OpenDoors doors;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        health.onDie += OnDieOpenDoors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDieOpenDoors()
    {
        doors.doorsUnlocked = true;
    }
}
