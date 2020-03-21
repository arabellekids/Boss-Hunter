using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Objective))]
public class ObjectiveKillEnemy : MonoBehaviour
{
    Health health;
    Objective m_Objective;

    public Transform rewardPos;
    public GameObject reward;

    public AudioClip completeSound;

    public GameObject objectiveEnemy;
    // Start is called before the first frame update
    void Start()
    {
        m_Objective = GetComponent<Objective>();
        DebugUtility.HandleErrorIfNullGetComponent<Objective, ObjectiveKillEnemies>(m_Objective, this, gameObject);

        health = objectiveEnemy.GetComponent<Health>();
        health.onDie += CompleteObjective;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CompleteObjective()
    {
        if (reward != null)
        {
            Instantiate(reward,rewardPos.position, rewardPos.rotation, null);
        }
        if(completeSound != null)
        {
            AudioSource.PlayClipAtPoint(completeSound, GameObject.FindGameObjectWithTag("Player").transform.position, 1);
        }
        m_Objective.CompleteObjective(string.Empty, "1", "Objective complete : " + m_Objective.title);
    }
}
