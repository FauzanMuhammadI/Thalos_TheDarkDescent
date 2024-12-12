using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ErebosController : MonoBehaviour
{
    enum AIState
    {
        Idle, Patrolling, Attacking
    }

    [Header("Patrol")]
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float waitAtPoint = 2f;
    private int currentWaypoint = 0;
    private float waitCounter;

    [Header("Components")]
    private NavMeshAgent agent;
    public Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("AI States")]
    [SerializeField] private AIState currentState;

    [Header("Attack")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackTime = 2f;
    private float timeToAttack;

    private bool isHit = false;

    private GameObject player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        waitCounter = waitAtPoint;
        timeToAttack = attackTime;
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not found or has been destroyed.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        FlipSprite();

        switch (currentState)
        {
            case AIState.Idle:
                if (isHit) break;
                animator.SetBool("Walking", true);
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = AIState.Patrolling;
                    agent.SetDestination(wayPoints[currentWaypoint].position);
                }

                if (distanceToPlayer <= attackRange)
                {
                    currentState = AIState.Attacking;
                }

                break;

            case AIState.Patrolling:
                if (isHit) break;
                animator.SetBool("Walking", true);
                if (agent.remainingDistance <= 0.2f)
                {
                    currentWaypoint++;
                    if (currentWaypoint >= wayPoints.Length)
                    {
                        currentWaypoint = 0;
                    }
                    currentState = AIState.Idle;
                    waitCounter = waitAtPoint;
                }

                if (distanceToPlayer <= attackRange)
                {
                    currentState = AIState.Attacking;
                }
                break;

            case AIState.Attacking:
                animator.SetBool("Walking", true);

                // Tetapkan tujuan patroli jika jaraknya jauh
                if (agent.remainingDistance <= 0.2f)
                {
                    currentWaypoint++;
                    if (currentWaypoint >= wayPoints.Length)
                    {
                        currentWaypoint = 0;
                    }
                    agent.SetDestination(wayPoints[currentWaypoint].position);
                }

                // Lakukan serangan saat pemain dalam jarak serang
                if (distanceToPlayer <= attackRange)
                {
                    transform.LookAt(player.transform.position, Vector3.up);
                    timeToAttack -= Time.deltaTime;

                    if (timeToAttack <= 0)
                    {
                        animator.SetTrigger("Attack");
                        timeToAttack = attackTime;
                    }
                }

                // Jika pemain keluar dari jarak serang, kembali ke patroli
                if (distanceToPlayer > attackRange)
                {
                    currentState = AIState.Patrolling;
                }

                break;
        }

        transform.rotation = Quaternion.Euler(60f, 0f, 0f);
    }


    public void TriggerHitAnimation()
    {
        if (isHit) return; 

        isHit = true;
        animator.SetBool("Hit", true);

        StartCoroutine(HandleHitAnimation());
    }

    private IEnumerator HandleHitAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.SetBool("Hit", false);
        isHit = false; 
    }


    private void FlipSprite()
    {
        if (player == null) return;

        Vector3 scale = spriteRenderer.transform.localScale;

        if (player.transform.position.x > transform.position.x)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }

        spriteRenderer.transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < wayPoints.Length; i++)
        {
            Gizmos.DrawSphere(wayPoints[i].position, 0.3f);
            if (i > 0)
            {
                Gizmos.DrawLine(wayPoints[i - 1].position, wayPoints[i].position);
            }
        }

        // Gambar jarak serangan
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
