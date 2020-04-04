using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Objective))]
public class ObjectiveKillSpawners : MonoBehaviour
{
    //[HideInInspector]
    public int remainingSpawners = 0;

    Objective m_Objective;
    private bool objectiveCompleted = false;
    private int maxSpawners;
    private int notificationEnemiesRemainingThreshold;

    // Start is called before the first frame update
    void Start()
    {
        m_Objective = GetComponent<Objective>();
        DebugUtility.HandleErrorIfNullGetComponent<Objective, ObjectiveKillSpawners>(m_Objective, this, gameObject);

        maxSpawners = remainingSpawners;

        if (string.IsNullOrEmpty(m_Objective.description))
            m_Objective.description = GetUpdatedCounterAmount();
    }

    // Update is called once per frame
    void Update()
    {
        CompleteObjective();
    }
    void CompleteObjective()
    {
        if (remainingSpawners <= 0 && objectiveCompleted == false)
        {
            m_Objective.CompleteObjective("Killed all enemy spawners!", "1", "Objective complete : " + m_Objective.title);
            objectiveCompleted = true;
            //objective.CompleteObjective("Killed all enemy spawners!", "1", "Objective complete : " + objective.title);
        }
    }
    string GetUpdatedCounterAmount()
    {
        return remainingSpawners + " / " + maxSpawners;
    }
    public void UpdateDescription()
    {
        string notificationText = notificationEnemiesRemainingThreshold >= remainingSpawners ? remainingSpawners + " spawners to kill left" : string.Empty;

        m_Objective.UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
        //m_Objective.description = GetUpdatedCounterAmount();
    }
}
