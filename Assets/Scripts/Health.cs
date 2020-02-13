using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHp = 10;
    private float currentHp = 10;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float dmg)
    {
        if(currentHp - dmg <= 0)
        {
            currentHp = 0;
            Destroy(gameObject);
        }
        else
        {
            currentHp -= dmg;
        }
    }
}
