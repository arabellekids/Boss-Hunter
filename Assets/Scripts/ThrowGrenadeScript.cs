using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class ThrowGrenadeScript : MonoBehaviour
{
    private Animator anim;
    public TextMeshProUGUI ammoText;
    public GameObject ammoSlider;

    public Transform bulletSpwnPos;
    public GameObject grenade;

    private int grenadeAmmo = 10;
    public int maxGrenadeAmmo = 10;

    private void Start()
    {
        anim = GetComponent<Animator>();
        grenadeAmmo = maxGrenadeAmmo;
        ammoText.text = grenadeAmmo.ToString() + "/" + maxGrenadeAmmo.ToString();
        ammoSlider.SetActive(false);
    }

    private void Update()
    {
        if (grenadeAmmo > 0)
        {
            anim.SetBool("ThrowGrenade",Input.GetButtonDown("Fire1"));
        }
        ammoText.text = grenadeAmmo.ToString() + "/" + maxGrenadeAmmo.ToString();
        ammoSlider.SetActive(false);
    }

    public void ThrowGrenade()
    {
        Instantiate(grenade, bulletSpwnPos.position, bulletSpwnPos.rotation, null);
        grenadeAmmo--;
    }
}
