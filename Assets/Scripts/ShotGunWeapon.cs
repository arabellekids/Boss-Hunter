using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShotGunWeapon : MonoBehaviour
{
    public ParticleSystem reloadEffect;
    public Transform reloadPos;

    private bool reloading = false;
    public float reloadTime = 2;

    public float maxAmmo = 1;
    public float rechargeTime = 20;

    public TextMeshProUGUI ammoText;
    public Slider ammoSlider;
    public ParticleSystem shootEffect;

    public AudioClip fireSound;

    Animator anim;
    public GameObject bullet;
    public Transform bulletSpwnPos;

    private float ammo = 1;
    private float timer = 0;
    private void Start()
    {
        ammo = maxAmmo;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        ammoText.text = ammo.ToString() + "/" + maxAmmo.ToString();
        ammoSlider.value = ammo / maxAmmo;

        if (ammo >= 1)
        {
            anim.SetBool("Firing", Input.GetAxis("Fire1") != 0);
        }
        if(ammo <= 0)
        {
            timer += Time.deltaTime;
        }
        if (ammo <= 0 && reloading == false)
        {
            reloadEffect.Play();
            reloading = true;
        }
        if(ammo <= 0 && timer >= reloadTime)
        {
            ammo = maxAmmo;
        }
    }
    public void Fire()
    {
        ammo -= 1;
        if(ammo < 0)
        {
            ammo = 0;
        }

        shootEffect.Play();
        AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.3f);
        Instantiate(bullet, bulletSpwnPos.position, bulletSpwnPos.rotation, null);
    }
}
