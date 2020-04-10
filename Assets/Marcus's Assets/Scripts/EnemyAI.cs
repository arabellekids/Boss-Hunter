using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AiStates
{
    GoToAndAttackCore,
    GoToAndAttackTarget,
}

public class EnemyAI : MonoBehaviour
{
    AiStates aiState;
    EnemyFunctions controller;
    Transform corePos;
    Transform targetPos = null;
    

    float distToTarget = Mathf.Infinity;

    float LastSeenTargetTime = Mathf.NegativeInfinity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<EnemyFunctions>();
        corePos = GameObject.Find("Core").transform;
        targetPos = null;
        aiState = AiStates.GoToAndAttackCore;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAITransitions();
        UpdateAIStates();
        DetectTargets();
    }
    void DetectTargets()
    {
        var targets = Physics.OverlapSphere(transform.position, controller.detectionRange);
        if(targets.Length > 0)
        {
            float distToHit = Mathf.Infinity;
            Transform closestHit;
            foreach(Collider hit in targets)
            {
                if (Vector3.Distance(transform.position, hit.transform.position) < distToHit && hit.GetComponent<Actor>() != null && hit.GetComponent<Actor>().affiliation == 1)
                {
                    distToHit = Vector3.Distance(transform.position, hit.transform.position);
                    closestHit = hit.transform;
                    if(targetPos == null)
                    {
                        targetPos = hit.transform;
                        distToTarget = distToHit;
                    }
                }
            }
        }
    }

    void UpdateAITransitions()
    {
        switch(aiState)
        {
            case AiStates.GoToAndAttackCore:
                if(targetPos != null && distToTarget < Vector3.Distance(transform.position, corePos.position))
                {
                    aiState = AiStates.GoToAndAttackTarget;
                    LastSeenTargetTime = Time.time;
                }
                break;
            case AiStates.GoToAndAttackTarget:
                if(Vector3.Distance(transform.position,targetPos.position) > controller.detectionRange && LastSeenTargetTime + controller.stopAttackingTargetTime < Time.time || targetPos.GetComponent<Health>().currentHealth <= 0)
                {
                    targetPos = null;
                    aiState = AiStates.GoToAndAttackCore;
                }
                break;
        }    
    }
    void UpdateAIStates()
    {
        switch (aiState)
        {
            case AiStates.GoToAndAttackCore:
                controller.MoveTo(corePos.GetComponent<Actor>().aimPoint.position);
                if(Vector3.Distance(transform.position,corePos.position) <= controller.attackRange)
                {
                    controller.TryAtack((corePos.position - controller.weapon.transform.position).normalized);
                }
               break;
            case AiStates.GoToAndAttackTarget:
                if(Vector3.Distance(transform.position,targetPos.position) <= controller.detectionRange)
                {
                    LastSeenTargetTime = Time.time;
                }
                if(Vector3.Distance(transform.position,targetPos.position) <= controller.attackRange)
                {
                    controller.TryAtack((targetPos.position - controller.weapon.transform.position).normalized);
                }
                controller.MoveTo(targetPos.GetComponent<Actor>().aimPoint.position);
                break;
        }
    }
}
