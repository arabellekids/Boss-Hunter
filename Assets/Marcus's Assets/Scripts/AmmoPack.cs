using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AmmoPack : MonoBehaviour
{
    public int ammoToRestore = 1;
    public float rotateSpeed = 1f;
    public string weaponAmmoName;
    public GameObject errorMessage;
    public float errorLifeTime = 3;

    private bool error = false;
    private float lastError = Mathf.NegativeInfinity;
    private Transform messageSpot;
    private void Start()
    {
        messageSpot = GameObject.FindGameObjectWithTag("DisplayMessage").transform;
    }
    private void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime, rotateSpeed * Time.deltaTime, rotateSpeed * Time.deltaTime);
        if (error)
        {
            if (lastError + errorLifeTime < Time.time)
            {
                error = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameObject.Find(weaponAmmoName + "(Clone)") != null)
            {
                var weapon = GameObject.Find(weaponAmmoName + "(Clone)").GetComponent<AmmoPackScript>();
                weapon.ammoPacks += ammoToRestore;
                Destroy(gameObject);
            }
            else if (GameObject.Find(weaponAmmoName + "(Clone)") == null)
            {
                if (GameObject.FindGameObjectsWithTag("ErrorMessage").Length == 0 && error == false)
                {
                    Vector3 messageOffset = new Vector3(messageSpot.position.x, messageSpot.position.y + 68.6f, messageSpot.position.z);
                    var message = Instantiate(errorMessage, messageOffset, Quaternion.identity, messageSpot);
                    message.tag = "ErrorMessage";
                    error = true;
                    lastError = Time.time;
                }
            }
        }
    }
}
