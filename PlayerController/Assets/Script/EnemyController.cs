using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int currentPatrolPoints;
    public NavMeshAgent Agent;
    public Animator anim;

    public static EnemyController enemy;

    private void Awake()
    {
        enemy = this;
    }

    public enum AIState
    {
        isIdle,
        isPatrolling,
        isChasing,
        isAttacking
    };

    public AIState currentState;
    public float waitAtPoint = 2f;
    private float waitCounter;

    public float chaseRange;

    public float attackRange = 2f;
    public float timeBetweenAttacks = 2f;
    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        switch (currentState)
        {
            case AIState.isIdle:
                {
                    anim.SetBool("IsMoving", false);

                    if (waitCounter > 0)
                    {
                        waitCounter -= Time.deltaTime;
                    }
                    else
                    {
                        currentState = AIState.isPatrolling;
                        Agent.SetDestination(patrolPoints[currentPatrolPoints].position);
                    }

                    if (distanceToPlayer <= chaseRange)
                    {
                        currentState = AIState.isChasing;
                        anim.SetBool("IsMoving", true);
                    }

                    break;
                }
            case AIState.isPatrolling:
                {
                    if (Agent.remainingDistance <= .2f)
                    {
                        currentPatrolPoints++;
                        if (currentPatrolPoints >= patrolPoints.Length)
                        {
                            currentPatrolPoints = 0;
                        }

                        currentState = AIState.isIdle;
                        waitCounter = waitAtPoint;
                    }

                    if (distanceToPlayer <= chaseRange)
                    {
                        currentState = AIState.isChasing;
                    }

                    anim.SetBool("IsMoving", true);

                    break;
                }

            case AIState.isChasing:
                {
                    Agent.SetDestination(PlayerController.instance.transform.position);
                    if (distanceToPlayer <= attackRange)
                    {
                        currentState = AIState.isAttacking;
                        anim.SetTrigger("Attack");
                        anim.SetBool("IsMoving", false);

                        Agent.velocity = Vector3.zero;
                        Agent.isStopped = true;

                        attackCounter = timeBetweenAttacks;
                    }
                    if (distanceToPlayer > chaseRange)
                    {
                        currentState = AIState.isIdle;
                        waitCounter = waitAtPoint;

                        Agent.velocity = Vector3.zero;
                        Agent.SetDestination(transform.position);
                    }
                    break;
                }

            case AIState.isAttacking:
                {
                    transform.LookAt(PlayerController.instance.transform, Vector3.up);
                    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                    attackCounter -= Time.deltaTime;
                    if (attackCounter <= 0)
                    {
                        if (distanceToPlayer < attackRange)
                        {
                            anim.SetTrigger("Attack");
                            attackCounter = timeBetweenAttacks;
                        }
                        else
                        {
                            currentState = AIState.isIdle;
                            waitCounter = waitAtPoint;

                            Agent.isStopped = false;
                        }
                    }
                    break;
                }
        }

    }
}
