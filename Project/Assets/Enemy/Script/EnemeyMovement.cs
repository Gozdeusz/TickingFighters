using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Animator animator;
    [SerializeField] private float stopDistance = 2f;
    [SerializeField] private float patrolRadius = 3f;
    [SerializeField] private float patrolChangeTime = 5f;

    private Rigidbody2D rb;
    private Vector2 patrolDirection;
    private float patrolTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        patrolTimer = patrolChangeTime;
        ChangePatrolDirection();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius) 
        {
            ChasePlayer(distanceToPlayer);
        }
        else 
        {
            Patrol();
        }
    }

    private void ChasePlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > stopDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.localScale = new Vector3(Mathf.Sign(-directionToPlayer.x), 1, 1);
            rb.linearVelocity = directionToPlayer * moveSpeed;
            animator.SetBool("isWalking", true);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);
        }
    }

    private void Patrol()
    {
        patrolTimer -= Time.deltaTime;

        if (patrolTimer <= 0)
        {
            ChangePatrolDirection();
            patrolTimer = patrolChangeTime;
        }

        rb.linearVelocity = patrolDirection * (moveSpeed * 0.5f);
        transform.localScale = new Vector3(Mathf.Sign(-patrolDirection.x), 1, 1);
        animator.SetBool("isWalking", true);
    }

    private void ChangePatrolDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        patrolDirection = new Vector2(randomX, randomY).normalized;
    }
}
