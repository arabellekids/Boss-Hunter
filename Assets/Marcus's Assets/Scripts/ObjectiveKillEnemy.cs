using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Objective))]
public class ObjectiveKillEnemy : MonoBehaviour
{
    Health health;
    Objective m_Objective;
    public bool rewardAvaliable = true;

    public string bossName;
    public Transform rewardPos;
    public GameObject reward;

    private GameObject objectiveEnemy;
    // Start is called before the first frame update
    void Start()
    {
        m_Objective = GetComponent<Objective>();
        DebugUtility.HandleErrorIfNullGetComponent<Objective, ObjectiveKillEnemies>(m_Objective, this, gameObject);

        health = objectiveEnemy.GetComponent<Health>();
        health.onDie += OnDie;

        objectiveEnemy = GameObject.Find(bossName);
        rewardPos = GameObject.Find("rewardPos").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDie()
    {
        if (rewardAvaliable)
        {
            Instantiate(reward,rewardPos.position, rewardPos.rotation, null);
        }
        m_Objective.CompleteObjective(string.Empty, "1", "Objective complete : " + m_Objective.title);
    }
}
