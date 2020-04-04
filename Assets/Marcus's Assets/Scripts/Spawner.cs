using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPos;

    private ObjectiveKillSpawners objective;
    private Health health;

    public float spawningRate = 2;

    public GameObject deathVFX;
    [Tooltip("The point at which the death VFX is spawned")]
    public Transform deathVFXSpawnPoint;

    [Header("Loot")]
    [Tooltip("The object this enemy can drop when dying")]
    public GameObject lootPrefab;
    [Tooltip("The chance the object has to drop")]
    [Range(0, 1)]
    public float deathDuration = 0f;

    public float dropRate = 1f;

    [HideInInspector]
    public int totalObjectsSpawned;
    private float lastSpawnTime = Mathf.NegativeInfinity;
    // Start is called before the first frame update
    void Awake()
    {
        objective = FindObjectOfType<ObjectiveKillSpawners>();
        objective.remainingSpawners++;

        health = GetComponent<Health>();
        health.onDie += OnDie;
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
    void OnDie()
    {
        // spawn a particle system when dying
        var vfx = Instantiate(deathVFX, deathVFXSpawnPoint.position, Quaternion.identity);
        Destroy(vfx, 5f);

        // loot an object
        if (TryDropItem())
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
        }
        objective.remainingSpawners--;
        objective.UpdateDescription();
        // this will call the OnDestroy function
        Destroy(gameObject, deathDuration);
    }
    public bool TryDropItem()
    {
        if (dropRate == 0 || lootPrefab == null)
            return false;
        else if (dropRate == 1)
            return true;
        else
            return (Random.value <= dropRate);
    }
}
