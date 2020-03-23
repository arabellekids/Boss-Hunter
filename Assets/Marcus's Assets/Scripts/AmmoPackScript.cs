using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(WeaponController))]
public class AmmoPackScript : MonoBehaviour
{
    public int ammoPacks = 2;
    public float ammoReloadRate = 1f;
    public float ammoReloadDelay = 0f;

    public bool infiniteAmmo = false;

    [HideInInspector]
    public bool isReloading = false;

    float reloadStart = Mathf.NegativeInfinity;

    [HideInInspector]
    public WeaponController weapon;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        Reload();
    }
    void BeginReload()
    {
        reloadStart = Time.time;
        weapon.canShoot = false;
        isReloading = true;
        if (weapon.isCharging)
        {
            weapon.HandleShoot();
            weapon.isCharging = false;
            weapon.currentCharge = 0;
        }
        
        if (infiniteAmmo == false)
        {
            ammoPacks--;
        }
        else
        {
            weapon.m_CurrentAmmo = weapon.maxAmmo;
            weapon.currentAmmoRatio = 1f;
            weapon.canShoot = true;
            isReloading = false;
        }
    }
    public void Reload()
    {
        if (weapon.m_CurrentAmmo >= weapon.maxAmmo && isReloading)
        {
            isReloading = false;
            weapon.canShoot = true;
        }
        if (weapon.m_CurrentAmmo <= 0 && isReloading == false && ammoPacks > 0)
        {
            BeginReload();
            isReloading = true;
        }
        if (reloadStart + ammoReloadDelay < Time.time && isReloading && !weapon.isCharging && ammoPacks > 0)
        {
            // reloads weapon over time
            weapon.m_CurrentAmmo += ammoReloadRate * Time.deltaTime;
            // limits ammo to max value
            weapon.m_CurrentAmmo = Mathf.Clamp(weapon.m_CurrentAmmo, 0, weapon.maxAmmo);

            weapon.isCooling = true;
        }
        else
        {
            weapon.isCooling = false;
        }

        if (weapon.maxAmmo == Mathf.Infinity)
        {
            weapon.currentAmmoRatio = 1f;
        }
        else
        {
            weapon.currentAmmoRatio = weapon.m_CurrentAmmo / weapon.maxAmmo;
        }
    }

}
