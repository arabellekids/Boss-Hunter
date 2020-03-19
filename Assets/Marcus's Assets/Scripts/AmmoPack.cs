using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AmmoPack : MonoBehaviour
{
    public int ammoToRestore = 1;
    public float rotateSpeed = 1f;
    public string weaponAmmoName;

    private void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime, rotateSpeed * Time.deltaTime, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(GameObject.Find(weaponAmmoName + "(Clone)") != null)
            {
                var weapon = GameObject.Find(weaponAmmoName + "(Clone)").GetComponent<AmmoPackScript>();
                weapon.ammoPacks += ammoToRestore;
                Destroy(gameObject);
            }
        }
    }
}
