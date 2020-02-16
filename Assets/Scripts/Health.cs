using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Health : MonoBehaviour
{
    public AudioClip hitSound;
    public AudioClip dieSound;
    public GameObject dieEffect;

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
        Instantiate(dieEffect, transform.position, transform.rotation, null);
        Destroy(gameObject);
    }
}
