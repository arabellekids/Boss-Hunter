using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShotGunWeapon : MonoBehaviour
{
    public RectTransform aimCircle;
    public ParticleSystem reloadEffect;
    public Transform reloadPos;
    public Transform ammoCartrige;

    private bool reloading = false;
    public float reloadTime = 2;

    public int maxAmmo = 1;
    public float rechargeTime = 20;

    public float maxAccuracyOffset = 1;
    public float accuracyChange = 0.2f;
    public float accuracyWaitTime = 1;

    public TextMeshProUGUI ammoText;
    public Slider ammoSlider;
    public ParticleSystem shootEffect;

    public AudioClip fireSound;

    Animator anim;
    public GameObject bullet;
    public Transform bulletSpwnPos;

    private int ammo = 1;
    private float timer = 0;
    private float accuracyOffset = 0;
    private float accuracyTimer = 0;
    private void Start()
    {
        ammo = maxAmmo;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        ammoText.text = ammo.ToString() + "/" + maxAmmo.ToString();
        ammoSlider.value = ammo / maxAmmo;
        ammoSlider.gameObject.SetActive(true);

        if (ammo >= 1)
        {
            anim.SetBool("Firing", Input.GetButton("Fire1"));
        }
        else
        {
            anim.SetBool("Firing", false);
        }

        if(accuracyOffset > 0)
        {
            accuracyTimer += Time.deltaTime;
            if(accuracyTimer >= accuracyWaitTime)
            {
                accuracyTimer = 0;
                accuracyOffset = 0;
                aimCircle.localScale = new Vector3(1, 1, 1);
            }
        }

        if(ammo <= 0)
        {
            anim.SetBool("reloading", true);
        }

        if(ammo > 0)
        {
            anim.SetBool("reloading", false);
        }
    }
    public void Fire()
    {
        ammo -= 1;
        if(ammo < 0)
        {
            ammo = 0;
        }
        if(ammo > 0)
        {
            accuracyTimer = 0;
            aimCircle.localScale = new Vector3(0.5f + accuracyOffset, 0.5f + accuracyOffset, 1);
#if false
            Vector3 offsetPos = new Vector3(Random.Range(bulletSpwnPos.position.x - accuracyOffset, bulletSpwnPos.position.x + accuracyOffset), 
                Random.Range(bulletSpwnPos.position.y - accuracyOffset, bulletSpwnPos.position.y + accuracyOffset), 
                bulletSpwnPos.position.z);

            var offsetDirection = bulletSpwnPos.rotation;
#else
            var offsetPos = bulletSpwnPos.position;

            var offsetDirection = bulletSpwnPos.rotation
                * Quaternion.AngleAxis(Random.Range(-2 * accuracyOffset, 2 * accuracyOffset), bulletSpwnPos.right)
                * Quaternion.AngleAxis(Random.Range(-2 * accuracyOffset, 2 * accuracyOffset), bulletSpwnPos.up);
#endif

            ammoCartrige.Rotate(0, 10, 0);
            shootEffect.Play();
            AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.3f);
            Instantiate(bullet, offsetPos, offsetDirection, null);
            accuracyOffset += accuracyChange;
            if(accuracyOffset > maxAccuracyOffset)
            {
                accuracyOffset = maxAccuracyOffset;
            }
        }
    }

    public void beginReloading()
    {
        reloadEffect.Play();
    }

    public void Reload()
    {
        ammo = maxAmmo;
    }
}
