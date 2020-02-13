using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerShooter : MonoBehaviour
{
    Animator anim;
    public GameObject bullet;
    public Transform bulletSpwnPos;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        anim.SetBool("Firing", Input.GetAxis("Fire1") != 0);
    }
    public void Fire()
    {
        Instantiate(bullet, bulletSpwnPos.position, bulletSpwnPos.rotation, null);
        Debug.Log("Fired");
    }
}
