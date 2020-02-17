using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float dmg = 20;
    public bool isBomb = false;

    public float explosionRange = 10;
    public float explosionForce = 10;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRange);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            Health health = hit.GetComponent<Health>();

            if (rb != null)
                rb.AddExplosionForce(explosionForce, explosionPos, explosionRange, 3.0F);
            if (health != null && isBomb == true)
                health.TakeDamage(dmg);
        }
    }
}
