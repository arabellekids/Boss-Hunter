using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    public string weaponName = "BulletShotgun";
    public AudioClip reloadSound;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var weapon = GameObject.Find(weaponName);
            if(weapon.GetComponent<LaserWeapon>() != null)
            {
                AudioSource.PlayClipAtPoint(reloadSound, transform.position, 1);
                weapon.GetComponent<LaserWeapon>().laserCarge = weapon.GetComponent<LaserWeapon>().maxLaserCharge;
            }

            else if(weapon.GetComponent<BulletWeapon>() != null)
            {
                AudioSource.PlayClipAtPoint(reloadSound, weapon.transform.position, 1);
                weapon.GetComponent<BulletWeapon>().ammo = weapon.GetComponent<BulletWeapon>().maxAmmo;
            }
            Destroy(gameObject);
        }
    }
}
