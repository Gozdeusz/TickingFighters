using UnityEngine;

public class Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isKnockedBack = false;
    [SerializeField] private float knockbackTime = 0.2f;
    [SerializeField] private float knockbackForce = 5f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 sourcePosition, float force)
    {
        if (rb == null) return;

        Vector2 knockbackDirection = (transform.position - (Vector3)sourcePosition).normalized;
        rb.linearVelocity = knockbackDirection * force; 
        isKnockedBack = true;
        Invoke(nameof(ResetKnockback), knockbackTime);
    }

    private void ResetKnockback()
    {
        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
    }
}
