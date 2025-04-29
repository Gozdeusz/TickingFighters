using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private Animator animator;
    [SerializeField] private float dashSpeed = 500f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private TrailRenderer trailRenderer; 

    private Player player;  
    private Vector2 movement;
    private Vector2 lastDirection = Vector2.right;
    private bool isDashing = false;

    void Start()
    {
        trailRenderer.enabled = false; 
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (!isDashing)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            movement = new Vector2(moveX, moveY);

            if (movement.x != 0 || movement.y != 0)
            {
                lastDirection = movement.normalized;
            }

            rb.linearVelocity = movement * speed;

            if (animator != null)
            {
                animator.SetBool("isWalking", movement.magnitude > 0);
                if (movement.magnitude > 0) onDust();
                else particleSystem.Stop();
            }

            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private void Flip()
    {
        if (lastDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, 1);
        }
        else if (lastDirection.x > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, 1);
        }
    }

    private void onDust()
    {
        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }

    public IEnumerator Dash()
    {
        if (player.getEnergy() < 0)
        {
            //Powieksz pasek energii
        }
        else
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;

            player.subEnergy(10);
            isDashing = true;
            trailRenderer.enabled = true;
            rb.linearVelocity = Vector2.zero;
            Vector2 dashDirection = lastDirection;
            rb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);

            yield return new WaitForSeconds(dashDuration);

            isDashing = false;
            collider.enabled = true;
            //Wizualny efekt slizgu
            trailRenderer.enabled = false;
        }
    }

    public Vector2 getLastDirection()
    {
        return lastDirection;
    }
    public void startDash()
    {
        if (Input.GetMouseButtonDown(0) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }
}
