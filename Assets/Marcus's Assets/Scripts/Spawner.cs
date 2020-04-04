using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPos;

    public float spawningRate = 2;
    [HideInInspector]
    public int totalObjectsSpawned;
    private float lastSpawnTime = Mathf.NegativeInfinity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lastSpawnTime + spawningRate < Time.time)
        {
            var thing = Instantiate(objectToSpawn, spawnPos.position, spawnPos.rotation, null);
            if(thing.GetComponent<RefreshSpawner>() != null)
            {
                thing.GetComponent<RefreshSpawner>().spawner = this;
            }
            totalObjectsSpawned++;
            lastSpawnTime = Time.time;
        }
    }
}
