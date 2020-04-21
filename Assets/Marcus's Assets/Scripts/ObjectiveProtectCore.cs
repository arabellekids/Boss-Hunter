using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Objective))]
public class ObjectiveProtectCore : MonoBehaviour
{
    Objective objective;
    // Start is called before the first frame update
    void Start()
    {
        objective = GetComponent<Objective>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDescription();
    }
    public void UpdateDescription()
    {
        string notificationText = Time.time.ToString();

        objective.UpdateObjective("Core lifetime (In seconds) : "+notificationText, "", "");
    }
}
