using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyFunctions : MonoBehaviour
{
    GameFlowManager manager;
    public WeaponController weapon;
    public float attackRange = 6;
    public float detectionRange = 8;
    public float stopAttackingTargetTime = 5;

    [Header("VFX")]
    [Tooltip("The VFX prefab spawned when the enemy dies")]
    public GameObject deathVFX;
    [Tooltip("The point at which the death VFX is spawned")]
    public Transform deathVFXSpawnPoint;

    [Header("Loot")]
    [Tooltip("The object this enemy can drop when dying")]
    public GameObject lootPrefab;
    [Tooltip("The chance the object has to drop")]
    [Range(0, 1)]
    public float dropRate = 1f;

    public float deathDuration = 0f;

    public UnityAction onAttack;

    NavMeshAgent agent;
    Health health;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        manager = GameObject.FindObjectOfType<GameFlowManager>();

        health = GetComponent<Health>();
        health.onDie += OnDie;

        weapon.owner = gameObject;
    }

    public void MoveTo(Vector3 pos)
    {
        agent.SetDestination(pos);
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

        // this will call the OnDestroy function
        Destroy(gameObject, deathDuration);
    }

    public bool TryAtack(Vector3 weaponForward)
    {
        if (manager.gameIsEnding)
        {
            return false;
        }
        // point weapon towards player
        weapon.transform.forward = weaponForward;

        // Shoot the weapon
        bool didFire = weapon.HandleShootInputs(false, true, false);

        if (didFire && onAttack != null)
        {
            onAttack.Invoke();
        }

        return didFire;
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
