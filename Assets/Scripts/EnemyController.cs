using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    enum AIState
    {
        Idle, Patrolling, Chasing, Attacking
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

    [Header("Chasing")]
    [SerializeField] private float chaseRange;

    [Header("Suspicious")]
    [SerializeField] private float suspiciousTime;
    private float timeSinceLastSawPlayer;

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
        timeSinceLastSawPlayer = suspiciousTime;
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

        // Flip sprite based on player position
        FlipSprite();

        switch (currentState)
        {
            case AIState.Idle:
                if (isHit) break;
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = AIState.Patrolling;
                    agent.SetDestination(wayPoints[currentWaypoint].position);
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.Chasing;
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

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.Chasing;
                }
                break;

            case AIState.Chasing:
                if (isHit) break;
                animator.SetBool("Walking", true);
                agent.SetDestination(player.transform.position);
                if (distanceToPlayer > chaseRange)
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                    timeSinceLastSawPlayer -= Time.deltaTime;

                    if (timeSinceLastSawPlayer <= 0)
                    {
                        currentState = AIState.Idle;
                        timeSinceLastSawPlayer = suspiciousTime;
                        agent.isStopped = false;
                    }
                }

                if (distanceToPlayer <= attackRange)
                {
                    currentState = AIState.Attacking;
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;
                }

                break;

            case AIState.Attacking:
                transform.LookAt(player.transform.position, Vector3.up);
                timeToAttack -= Time.deltaTime;
                if (timeToAttack <= 0)
                {
                    animator.SetTrigger("Attack");
                    timeToAttack = attackTime;
                }

                if (distanceToPlayer > attackRange)
                {
                    currentState = AIState.Chasing;
                    agent.isStopped = false;
                }
                break;
        }

        // Rotasi tetap dikunci
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


    //private void OnDrawGizmos()
    //{
    //    // Gambar waypoint
    //    Gizmos.color = Color.green; // Ubah warna gizmo menjadi hijau
    //    for (int i = 0; i < wayPoints.Length; i++)
    //    {
    //        Gizmos.DrawSphere(wayPoints[i].position, 0.3f); // Gambar bola kecil di posisi waypoint
    //        if (i > 0)
    //        {
    //            Gizmos.DrawLine(wayPoints[i - 1].position, wayPoints[i].position); // Gambar garis antara waypoint
    //        }
    //    }

    //    // Gambar jarak pengejaran
    //    Gizmos.color = Color.red; // Ubah warna gizmo menjadi merah
    //    Gizmos.DrawWireSphere(transform.position, chaseRange); // Gambar lingkaran wireframe di sekitar musuh

    //    // Gambar jarak serangan
    //    Gizmos.color = Color.blue; // Ubah warna gizmo menjadi biru untuk area serangan
    //    Gizmos.DrawWireSphere(transform.position, attackRange); // Gambar lingkaran wireframe untuk area serangan
    //}
}
