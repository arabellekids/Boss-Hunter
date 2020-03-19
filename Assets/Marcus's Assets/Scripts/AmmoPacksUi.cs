using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoPacksUi : MonoBehaviour
{
    public TextMeshProUGUI ammoPacksUI;

    public AmmoCounter ammoCounter;
    public GameObject ammoPackImage;

    private AmmoPackScript weaponAmmoPacks;
    // Start is called before the first frame update
    void Start()
    {
        if(ammoCounter.m_Weapon != null)
        {
            if(ammoCounter.m_Weapon.GetComponent<AmmoPackScript>() != null)
            {
                ammoPackImage.SetActive(true);
                weaponAmmoPacks = ammoCounter.m_Weapon.GetComponent<AmmoPackScript>();
                ammoPacksUI.text = weaponAmmoPacks.ammoPacks.ToString();
            }
        }
        else
        {
            ammoPackImage.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(weaponAmmoPacks != null)
        {
            ammoPacksUI.text = weaponAmmoPacks.ammoPacks.ToString();
        }
    }
}
