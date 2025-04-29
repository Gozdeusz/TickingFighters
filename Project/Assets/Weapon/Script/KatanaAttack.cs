using System.Collections;
using UnityEngine;

public class KatanaAttack : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private int damage = 20;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Movement movement;

    private bool isDashing;
    private Vector2 lastDirection;

    private void Update()
    {
       
            movement.startDash();
        if (Input.GetMouseButtonDown(0)) {
            PerformDashAttack();
        }
        
    }

    public void PerformDashAttack()
    {
        animator.SetTrigger("isAttack");
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().takeDamage(damage, transform.position);
        }
    }
}
