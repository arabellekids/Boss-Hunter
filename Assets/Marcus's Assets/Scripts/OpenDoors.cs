using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OpenDoors : MonoBehaviour
{
    public bool doorsUnlocked = false;
    public AudioClip openSound;
    private bool doorsState = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (doorsUnlocked)
        {
            anim.SetBool("OpenDoors", true);
            doorsState = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && doorsState == false && doorsUnlocked)
        {
            anim.SetBool("OpenDoors", true);
            AudioSource.PlayClipAtPoint(openSound, transform.position, 1);
            doorsState = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && doorsState == true && doorsUnlocked)
        {
            anim.SetBool("OpenDoors", false);
            doorsState = false;
        }
    }
}
