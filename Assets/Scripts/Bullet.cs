using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    Rigidbody rb;

    public float lifeTime = 2;
    public float speed = 10;
    public float dmg = 2f;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = lifeTime;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * speed,ForceMode.Acceleration);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Health>() != null)
        {
            Health targetHp = collision.gameObject.GetComponent<Health>();
            targetHp.TakeDamage(dmg);
        }
        Destroy(gameObject);
    }

}
