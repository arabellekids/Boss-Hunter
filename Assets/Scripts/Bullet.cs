﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public GameObject hurtEffect;

    public float lifeTime = 2;
    public float speed = 10;
    public float dmg = 2f;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = lifeTime;
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Health>() != null)
        {
            Health targetHp = collision.gameObject.GetComponent<Health>();
            if(targetHp.currentHp < targetHp.maxHp / 3)
            {
                Instantiate(hurtEffect, collision.GetContact(0).point, transform.rotation, collision.transform);
            }
            targetHp.TakeDamage(dmg);
        }
        Destroy(gameObject);
    }

}
