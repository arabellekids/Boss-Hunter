using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class ThrowGrenadeScript : MonoBehaviour
{
    private Animator anim;
    public TextMeshProUGUI ammoText;

    public Transform bulletSpwnPos;
    public GameObject grenade;

    private int grenadeAmmo = 1;
    public int maxGrenadeAmmo = 10;

    private void Start()
    {
        anim = GetComponent<Animator>();
        grenadeAmmo = maxGrenadeAmmo;
        ammoText.text = grenadeAmmo.ToString() + "/" + maxGrenadeAmmo.ToString();
    }

    private void Update()
    {
        if (Input.GetButtonUp("ThrowGrenade") && grenadeAmmo > 0)
        {
            anim.SetTrigger("ThrowGrenade");
        }
        ammoText.text = grenadeAmmo.ToString() + "/" + maxGrenadeAmmo.ToString();
    }

    public void ThrowGrenade()
    {
        Instantiate(grenade, bulletSpwnPos.position, bulletSpwnPos.rotation, null);
        grenadeAmmo--;
    }
}
