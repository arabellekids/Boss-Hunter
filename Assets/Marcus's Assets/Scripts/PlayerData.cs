using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    float posX;
    float posY;
    float posZ;
    string sceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
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
    void SavePos()
    {
        PlayerPrefs.SetFloat("posX",posX);
        PlayerPrefs.SetFloat("posY", posY);
        PlayerPrefs.SetFloat("posZ", posZ);
    }

    void LoadPos()
    {
        posX = PlayerPrefs.GetFloat("posX");
        posY = PlayerPrefs.GetFloat("posY");
        posZ = PlayerPrefs.GetFloat("posZ");

        Vector3 loadPosition = new Vector3(posX-transform.position.x, posY - transform.position.y, posZ - transform.position.z);
        GetComponent<CharacterController>().Move(loadPosition);
    }
    public void Save()
    {
        SavePos();
        sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("sceneName", sceneName);
    }

    public void Load()
    {
        LoadPos();
        sceneName = PlayerPrefs.GetString("sceneName");
        SceneManager.LoadScene(sceneName);
    }
}
