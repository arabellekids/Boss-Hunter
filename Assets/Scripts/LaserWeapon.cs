using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class LaserWeapon : MonoBehaviour
{
    public float maxLaserCharge = 1;
    public float rechargeTime = 20;
    public float laserCost = 1;

    public RectTransform aimCircle;
    public TextMeshProUGUI laserChargeText;
    public Slider laserChargeSlider;
    public ParticleSystem shootEffect;

    public AudioClip fireSound;

    Animator anim;
    public GameObject bullet;
    public Transform bulletSpwnPos;

    public float laserCarge = 1;

    public float maxAccuracyOffset = 1;
    public float accuracyChange = 0.2f;
    public float accuracyWaitTime = 1;

    private float accuracyOffset = 0;
    private float accuracyTimer = 0;

    private float timer = 0;
    private void Start()
    {
        laserCarge = maxLaserCharge;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        laserChargeText.text = laserCarge.ToString()+"/"+maxLaserCharge.ToString();
        laserChargeSlider.gameObject.SetActive(true);
        laserChargeSlider.value = laserCarge / maxLaserCharge;
        if (laserCarge < maxLaserCharge)
        {
            timer += Time.deltaTime;
            if (timer >= rechargeTime)
            {
                timer = 0;
                laserCarge = Mathf.Round(laserCarge += maxLaserCharge / 5);
                if (laserCarge > maxLaserCharge)
                {
                    laserCarge = maxLaserCharge;
                }
            }
        }

        if(laserCarge >= laserCost)
        {
            anim.SetBool("Firing", Input.GetButton("Fire1"));
        }

        else
        {
            anim.SetBool("Firing", false);
        }

        if (accuracyOffset > 0)
        {
            accuracyTimer += Time.deltaTime;
            if (accuracyTimer >= accuracyWaitTime)
            {
                accuracyTimer = 0;
                accuracyOffset = 0;
                aimCircle.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    public void Fire()
    {
        laserCarge -= laserCost;
        if(laserCarge < 0)
        {
            laserCarge = 0;
        }
        accuracyTimer = 0;
        aimCircle.localScale = new Vector3(1 + (0.25f * accuracyOffset), 1 + (0.25f * accuracyOffset), 1);

        var offsetPos = bulletSpwnPos.position;
        var offsetDirection = bulletSpwnPos.rotation
                * Quaternion.AngleAxis(Random.Range(-2 * accuracyOffset, 2 * accuracyOffset), bulletSpwnPos.right)
                * Quaternion.AngleAxis(Random.Range(-2 * accuracyOffset, 2 * accuracyOffset), bulletSpwnPos.up);

        shootEffect.Play();
        AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.3f);
        Instantiate(bullet, offsetPos, offsetDirection, null);
        accuracyOffset += accuracyChange;
        if (accuracyOffset > maxAccuracyOffset)
        {
            accuracyOffset = maxAccuracyOffset;
        }
    }
}
