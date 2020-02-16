using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerShooter : MonoBehaviour
{
    public AudioClip fireSound;

    Animator anim;
    public GameObject bullet;
    public Transform bulletSpwnPos;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
            anim.SetBool("Firing", Input.GetButtonDown("Fire1"));
    }
    public void Fire()
    {
        AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.4f);
        Instantiate(bullet, bulletSpwnPos.position, bulletSpwnPos.rotation, null);
    }
}
