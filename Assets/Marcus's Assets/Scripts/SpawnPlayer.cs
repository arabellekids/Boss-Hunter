using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(player,Vector3.zero,transform.rotation,null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
