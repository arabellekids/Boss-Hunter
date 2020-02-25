using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchWeapons();
    }
    void SwitchWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && weapons[0] != null)
        {
            for (var i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons[1] != null)
        {
            for (var i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[1].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons[2] != null)
        {
            for (var i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[2].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && weapons[3] != null)
        {
            for (var i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[3].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && weapons[4] != null)
        {
            for (var i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[4].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && weapons[5] != null)
        {
            for (var i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[5].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && weapons[6] != null)
        {
            for (var i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[6].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && weapons[7] != null)
        {
            for (var i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[7].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && weapons[8] != null)
        {
            for (var i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[8].SetActive(true);
        }
    }
}
