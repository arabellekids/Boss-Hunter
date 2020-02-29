using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public GameObject weapon;
    public Transform weaponSpwnpos;
    public Transform weaponParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Instantiate(weapon, weaponSpwnpos.position, weaponSpwnpos.rotation, weaponParent);
        }
    }
}
