using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    float posX;
    float posY;
    float posZ;
    
    // Start is called before the first frame update
    void Start()
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
    }
    public void SavePos()
    {
        PlayerPrefs.SetFloat("posX",posX);
        PlayerPrefs.SetFloat("posY", posY);
        PlayerPrefs.SetFloat("posZ", posZ);
    }

    public void LoadPos()
    {
        posX = PlayerPrefs.GetFloat("posX");
        posY = PlayerPrefs.GetFloat("posY");
        posZ = PlayerPrefs.GetFloat("posZ");

        Vector3 loadPosition = new Vector3(posX-transform.position.x, posY - transform.position.y, posZ - transform.position.z);
        GetComponent<CharacterController>().Move(loadPosition);
    }
}
