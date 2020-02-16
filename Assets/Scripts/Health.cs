﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Health : MonoBehaviour
{
    public AudioClip hitSound;
    public AudioClip dieSound;
    public GameObject dieEffect;

    public float explosionRange = 1000;
    public float explosionForce = 10000;
    public float maxHp = 10;
    [HideInInspector]
    public float currentHp = 10;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float dmg)
    {
        if(currentHp - dmg <= 0)
        {
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position,1);
            currentHp -= dmg;
        }
    }
    void Die()
    {
        AudioSource.PlayClipAtPoint(dieSound, transform.position, 1);
        currentHp = 0;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRange);
        Instantiate(dieEffect, transform.position, transform.rotation, null);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionForce, explosionPos, explosionRange, 3.0F);
        }
        Destroy(gameObject);
    }
}
