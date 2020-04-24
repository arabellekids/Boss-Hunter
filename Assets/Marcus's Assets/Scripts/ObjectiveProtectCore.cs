using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Objective))]
public class ObjectiveProtectCore : MonoBehaviour
{
    float timeInSeconds = 0;
    float timeInMin = 0;
    float timeInHr = 0;

    GameFlowManager manager;
    Objective objective;
    // Start is called before the first frame update
    void Start()
    {
        objective = GetComponent<Objective>();
        manager = FindObjectOfType<GameFlowManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDescription();
    }
    public void UpdateDescription()
    {
        if(manager.gameIsEnding  == false)
        {
            timeInSeconds += Time.deltaTime;
            if (timeInSeconds >= 60)
            {
                timeInMin++;
                timeInSeconds = 0;
            }
            if (timeInMin >= 60)
            {
                timeInHr++;
                timeInMin = 0;
            }
            string notificationText = "Core lifetime : " + timeInHr + ":" + timeInMin + ":" + Mathf.Floor(timeInSeconds);

            objective.UpdateObjective(notificationText, "", "");
        }
    }
}
