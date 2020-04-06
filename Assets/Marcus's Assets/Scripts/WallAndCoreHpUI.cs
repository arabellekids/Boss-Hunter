using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class WallAndCoreHpUI : MonoBehaviour
{
    public Image healthFillImage;

    Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        healthFillImage.fillAmount = health.currentHealth / health.maxHealth;
    }
}
