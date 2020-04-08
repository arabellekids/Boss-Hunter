using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScript : MonoBehaviour
{
    public GameObject deathVFX;
    public float deathDuration = 0;

    Animator anim;
    WallAndCoreHpUI hp;
    Health health;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
        health.onDie += OnDie;
        health.onDamaged += OnDamaged;
        hp = GetComponent<WallAndCoreHpUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDamaged(float amount, GameObject attacker)
    {
        anim.SetTrigger("Hit");
    }

    void OnDie()
    {
        // spawn a particle system when dying
        var vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(vfx, 5f);
        hp.healthFillImage.fillAmount = 0;
        health.currentHealth = 0;
        // this will call the OnDestroy function
        gameObject.SetActive(false);
    }
}
