using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private Transform player;
    public Transform teleportPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            var teleportVector = new Vector3(teleportPoint.position.x - other.transform.position.x, teleportPoint.position.y - other.transform.position.y, teleportPoint.position.z - other.transform.position.z);
            other.GetComponent<CharacterController>().Move(teleportVector);
        }
    }
}
