using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Objective))]
public class ObjectiveKillSpawners : MonoBehaviour
{
    //[HideInInspector]
    public int remainingSpawners = 1;

    Objective m_Objective;
    // Start is called before the first frame update
    void Start()
    {
        m_Objective = GetComponent<Objective>();
        DebugUtility.HandleErrorIfNullGetComponent<Objective, ObjectiveKillSpawners>(m_Objective, this, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CompleteObjective();
    }
    void CompleteObjective()
    {
        if (remainingSpawners <= 0)
        {
            m_Objective.CompleteObjective(string.Empty, "1", "Objective complete : " + m_Objective.title);
            //objective.CompleteObjective("Killed all enemy spawners!", "1", "Objective complete : " + objective.title);
        }
    }
}
