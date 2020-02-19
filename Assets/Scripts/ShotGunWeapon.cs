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
    public float fireRate = 1;
    public float reloadTime = 2;

    public PlayerController player;

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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        ammoText.text = ammo.ToString() + "/" + maxAmmo.ToString();
        ammoSlider.value = ammo / maxAmmo;

        if (ammo >= 1 && Input.GetButtonDown("Fire1") && player.timer >= fireRate)
        {
            Fire();
            player.timer = 0;
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

        anim.SetTrigger("Firing");
        shootEffect.Play();
        AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.3f);
        Instantiate(bullet, bulletSpwnPos.position, bulletSpwnPos.rotation, null);
    }
}
