using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grenade : MonoBehaviour
{
    Rigidbody rb;
    public float throwForce;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddRelativeForce(Vector3.forward * throwForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, collision.GetContact(0).point, transform.rotation, null);
        Destroy(gameObject);
    }
}
