using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class LaserPistolWeapon : MonoBehaviour
{
    public float fireRate = 1;

    public PlayerController player;

    public float maxLaserCharge = 1;
    public float rechargeTime = 20;
    public float laserCost = 1;

    public TextMeshProUGUI laserChargeText;
    public Slider laserChargeSlider;
    public ParticleSystem shootEffect;

    public AudioClip fireSound;

    Animator anim;
    public GameObject bullet;
    public Transform bulletSpwnPos;

    private float laserCarge = 1;
    private float timer = 0;
    private void Start()
    {
        laserCarge = maxLaserCharge;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        laserChargeText.text = laserCarge.ToString()+"/"+maxLaserCharge.ToString();
        laserChargeSlider.value = laserCarge / maxLaserCharge;
        if (laserCarge < maxLaserCharge)
        {
            timer += Time.deltaTime;
            if (timer >= rechargeTime)
            {
                timer = 0;
                laserCarge += maxLaserCharge / 5;
                if (laserCarge > maxLaserCharge)
                {
                    laserCarge = maxLaserCharge;
                }
            }
        }

        if(laserCarge >= laserCost && Input.GetButtonDown("Fire1") && player.timer >= fireRate)
        {
            Fire();
            player.timer = 0;
        }
    }
    public void Fire()
    {
        laserCarge -= laserCost;
        if(laserCarge < 0)
        {
            laserCarge = 0;
        }

        anim.SetTrigger("Firing");
        shootEffect.Play();
        AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.3f);
        Instantiate(bullet, bulletSpwnPos.position, bulletSpwnPos.rotation, null);
    }
}
