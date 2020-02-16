using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class PlayerShooter : MonoBehaviour
{
    public float maxLaserCharge = 1;
    public float rechargeTime = 20;
    public float laserCost = 1;

    public TextMeshProUGUI laserChargeText;
    public Slider laserChargeSlider;

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

        if(laserCarge >= laserCost)
        {
            anim.SetBool("Firing", Input.GetButtonDown("Fire1"));
        }
    }
    public void Fire()
    {
        laserCarge -= laserCost;
        if(laserCarge < 0)
        {
            laserCarge = 0;
        }
        AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.4f);
        Instantiate(bullet, bulletSpwnPos.position, bulletSpwnPos.rotation, null);
    }
}
