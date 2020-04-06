using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyMobile : MonoBehaviour
{
    public enum AIState
    {
        Patrol,
        Follow,
        Attack,
        GoToWall,
        AttackWall,
        GoToCore,
        AttackCore,
    }
    public float stoppingDistFromWall = 5;
    private float defaultStoppingDist;
    public float attackWallRange = 6;

    Transform corePos;
    public Transform wallPos;
    public Animator animator;
    [Tooltip("Fraction of the enemy's attack range at which it will stop moving towards target while attacking")]
    [Range(0f, 1f)]
    public float attackStopDistanceRatio = 0.5f;
    [Tooltip("The random hit damage effects")]
    public ParticleSystem[] randomHitSparks;
    public ParticleSystem[] onDetectVFX;
    public AudioClip onDetectSFX;

    [Header("Sound")]
    public AudioClip MovementSound;
    public MinMaxFloat PitchDistortionMovementSpeed;

    public AIState aiState { get; private set; }
    EnemyController m_EnemyController;
    AudioSource m_AudioSource;

    const string k_AnimMoveSpeedParameter = "MoveSpeed";
    const string k_AnimAttackParameter = "Attack";
    const string k_AnimAlertedParameter = "Alerted";
    const string k_AnimOnDamagedParameter = "OnDamaged";

    void Start()
    {
        m_EnemyController = GetComponent<EnemyController>();
        DebugUtility.HandleErrorIfNullGetComponent<EnemyController, EnemyMobile>(m_EnemyController, this, gameObject);

        m_EnemyController.onAttack += OnAttack;
        m_EnemyController.onDetectedTarget += OnDetectedTarget;
        m_EnemyController.onLostTarget += OnLostTarget;
        m_EnemyController.SetPathDestinationToClosestNode();
        m_EnemyController.onDamaged += OnDamaged;

        // Start patrolling
        aiState = AIState.Patrol;

        // adding a audio source to play the movement sound on it
        m_AudioSource = GetComponent<AudioSource>();
        DebugUtility.HandleErrorIfNullGetComponent<AudioSource, EnemyMobile>(m_AudioSource, this, gameObject);
        m_AudioSource.clip = MovementSound;
        m_AudioSource.Play();
        defaultStoppingDist = m_EnemyController.m_NavMeshAgent.stoppingDistance;
        corePos = GameObject.Find("Core").transform;
        aiState = AIState.GoToWall;
    }

    void Update()
    {
        UpdateAIStateTransitions();
        UpdateCurrentAIState();

        float moveSpeed = m_EnemyController.m_NavMeshAgent.velocity.magnitude;

        // Update animator speed parameter
        animator.SetFloat(k_AnimMoveSpeedParameter, moveSpeed);

        // changing the pitch of the movement sound depending on the movement speed
        m_AudioSource.pitch = Mathf.Lerp(PitchDistortionMovementSpeed.min, PitchDistortionMovementSpeed.max, moveSpeed / m_EnemyController.m_NavMeshAgent.speed);
    }

    void UpdateAIStateTransitions()
    {
        // Handle transitions 
        switch (aiState)
        {
            case AIState.Follow:
                // Transition to attack when there is a line of sight to the target
                if (m_EnemyController.isSeeingTarget && m_EnemyController.isTargetInAttackRange)
                {
                    aiState = AIState.Attack;
                    m_EnemyController.SetNavDestination(transform.position);
                }
                if(m_EnemyController.isTargetInAttackRange == false)
                {
                    aiState = AIState.GoToWall;
                }
                break;
            case AIState.Attack:
                // Transition to follow when no longer a target in attack range
                if (!m_EnemyController.isTargetInAttackRange)
                {
                    aiState = AIState.Follow;
                }
                break;
            case AIState.AttackWall:
                if(m_EnemyController.knownDetectedTarget != wallPos.gameObject)
                {
                    aiState = AIState.Follow;
                }
                if(wallPos.GetComponent<Health>().currentHealth <= 0)
                {
                    aiState = AIState.GoToCore;
                }
                break;
            case AIState.GoToWall:
                if(m_EnemyController.knownDetectedTarget == GameObject.FindGameObjectWithTag("Player"))
                {
                    aiState = AIState.Follow;
                }
                else if (Vector3.Distance(transform.position, wallPos.position) <= m_EnemyController.attackRange)
                {
                    aiState = AIState.AttackWall;
                }
                if (wallPos.GetComponent<Health>().currentHealth <= 0)
                {
                    aiState = AIState.GoToCore;
                }
                break;
            case AIState.GoToCore:
                if (Vector3.Distance(transform.position, corePos.position) <= m_EnemyController.attackRange)
                {
                    aiState = AIState.AttackCore;
                }
                break;
        }
    }

    void UpdateCurrentAIState()
    {
        // Handle logic 
        switch (aiState)
        {
            case AIState.Patrol:
                m_EnemyController.UpdatePathDestination();
                m_EnemyController.SetNavDestination(m_EnemyController.GetDestinationOnPath());
                break;
            case AIState.Follow:
                m_EnemyController.SetNavDestination(m_EnemyController.knownDetectedTarget.transform.position);
                m_EnemyController.OrientTowards(m_EnemyController.knownDetectedTarget.transform.position);
                break;
            case AIState.Attack:
                if(Vector3.Distance(m_EnemyController.knownDetectedTarget.transform.position, m_EnemyController.detectionSourcePoint.position) >= (attackStopDistanceRatio * m_EnemyController.attackRange))
                {
                    m_EnemyController.SetNavDestination(m_EnemyController.knownDetectedTarget.transform.position);
                }
                else
                {
                    m_EnemyController.SetNavDestination(transform.position);
                }
                m_EnemyController.OrientTowards(m_EnemyController.knownDetectedTarget.transform.position);
                m_EnemyController.TryAtack((wallPos.position - m_EnemyController.weapon.transform.position).normalized);
                break;
            case AIState.AttackWall:
                m_EnemyController.OrientTowards(wallPos.position);
                m_EnemyController.TryAtack((wallPos.position - m_EnemyController.weapon.transform.position).normalized);
                break;
            case AIState.GoToWall:
                m_EnemyController.SetNavDestination(wallPos.GetComponent<Actor>().aimPoint.position);
                m_EnemyController.OrientTowards(wallPos.GetComponent<Actor>().aimPoint.position);
                break;
            case AIState.GoToCore:
                m_EnemyController.SetNavDestination(corePos.GetComponent<Actor>().aimPoint.position);
                m_EnemyController.OrientTowards(corePos.GetComponent<Actor>().aimPoint.position);
                break;
            case AIState.AttackCore:
                m_EnemyController.OrientTowards(corePos.position);
                m_EnemyController.TryAtack((corePos.position - m_EnemyController.weapon.transform.position).normalized);
                break;
        }
    }

    void OnAttack()
    {
        animator.SetTrigger(k_AnimAttackParameter);
    }

    void OnDetectedTarget()
    {
        if (aiState == AIState.Patrol)
        {
            aiState = AIState.Follow;
        }
        
        for (int i = 0; i < onDetectVFX.Length; i++)
        {
            onDetectVFX[i].Play();
        }

        if (onDetectSFX)
        {
            AudioUtility.CreateSFX(onDetectSFX, transform.position, AudioUtility.AudioGroups.EnemyDetection, 1f);
        }

        animator.SetBool(k_AnimAlertedParameter, true);
    }

    void OnLostTarget()
    {
        if (aiState == AIState.Follow || aiState == AIState.Attack)
        {
            aiState = AIState.Patrol;
        }

        for (int i = 0; i < onDetectVFX.Length; i++)
        {
            onDetectVFX[i].Stop();
        }

        animator.SetBool(k_AnimAlertedParameter, false);
    }

    void OnDamaged()
    {
        if (randomHitSparks.Length > 0)
        {
            int n = Random.Range(0, randomHitSparks.Length - 1);
            randomHitSparks[n].Play();
        }

        animator.SetTrigger(k_AnimOnDamagedParameter);
    }
}
