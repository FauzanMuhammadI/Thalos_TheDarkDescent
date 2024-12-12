using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
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
    [SerializeField] private float dashCooldown = 5f;
    [SerializeField] private float dashSpeedMultiplier = 2f;
    [SerializeField] private float dashDuration = 0.5f;
    public GhostBoss ghost;
    private float dashTimer;
    private bool isDashing;
    private Vector3 dashDirection;

    [Header("Suspicious")]
    [SerializeField] private float suspiciousTime;
    private float timeSinceLastSawPlayer;

    [Header("Attack")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackTime = 2f;
    private float timeToAttack;

    public Vector3 lastMoveDirection;

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

        if (agent.velocity.magnitude > 0.1f)
        {
            lastMoveDirection = agent.velocity.normalized;
        }

        FlipSprite();

        switch (currentState)
        {
            case AIState.Idle:
                HandleIdleState(distanceToPlayer);
                break;

            case AIState.Patrolling:
                HandlePatrollingState(distanceToPlayer);
                break;

            case AIState.Chasing:
                HandleChasingState(distanceToPlayer);
                break;

            case AIState.Attacking:
                HandleAttackingState(distanceToPlayer);
                break;
        }

        transform.rotation = Quaternion.Euler(60f, 0f, 0f); 
    }

    private void FlipSprite()
    {
        if (lastMoveDirection.x != 0) 
        {
            Vector3 scale = spriteRenderer.transform.localScale;
            scale.x = Mathf.Sign(-lastMoveDirection.x) * Mathf.Abs(scale.x);
            spriteRenderer.transform.localScale = scale;
        }
    }

    private void HandleIdleState(float distanceToPlayer)
    {
        animator.SetBool("Walking", false);
        animator.SetBool("Hit", false);
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
    }

    private void HandlePatrollingState(float distanceToPlayer)
    {
        animator.SetBool("Walking", true);
        animator.SetBool("Hit", false);
        if (agent.remainingDistance <= 0.2f)
        {
            currentWaypoint = (currentWaypoint + 1) % wayPoints.Length;
            currentState = AIState.Idle;
            waitCounter = waitAtPoint;
        }

        if (distanceToPlayer <= chaseRange)
        {
            currentState = AIState.Chasing;
        }
    }

    private void HandleChasingState(float distanceToPlayer)
    {
        animator.SetBool("Walking", true);
        animator.SetBool("Hit", false);
        if (!isDashing)
        {
            agent.SetDestination(player.transform.position);

            if (distanceToPlayer > chaseRange)
            {
                HandlePlayerOutOfChaseRange();
            }

            if (distanceToPlayer <= attackRange)
            {
                currentState = AIState.Attacking;
                agent.isStopped = true;
            }

            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                StartDash();
            }
        }
        else
        {
            DashMovement();
        }
    }

    private void HandleAttackingState(float distanceToPlayer)
    {
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
    }

    private void HandlePlayerOutOfChaseRange()
    {
        agent.isStopped = true;
        timeSinceLastSawPlayer -= Time.deltaTime;

        if (timeSinceLastSawPlayer <= 0)
        {
            currentState = AIState.Idle;
            timeSinceLastSawPlayer = suspiciousTime;
            agent.isStopped = false;
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimer = dashCooldown;
        dashDirection = (player.transform.position - transform.position).normalized;
        if (ghost != null)
        {
            ghost.makeGhost = true;
        }
        StartCoroutine(EndDash());
    }

    private IEnumerator EndDash()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        agent.isStopped = false;
        if (ghost != null)
        {
            ghost.makeGhost = false;
        }
    }

    private void DashMovement()
    {
        agent.isStopped = true;
        transform.position += dashDirection * dashSpeedMultiplier * Time.deltaTime;
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
