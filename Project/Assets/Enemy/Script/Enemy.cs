using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private float health = 100;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private Animator animator;
    
    private bool isDead = false;
    
    public enum enemyType {Worlob,Rihlob,Umbrlob,Splob }
    [SerializeField] private enemyType enemy;

    void Start()
    {
        if (animator == null)
        {
           
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void takeDamage(float damage, Vector2 attackerPosition)
    {
        health -= damage;
        Debug.Log("Otrzymano obra¿enia: " + damage);
        animator.SetTrigger("takeDamage");
        damageParticles();
       
        Knockback knockback = GetComponent<Knockback>();
        if (knockback != null)
        {
            knockback.ApplyKnockback(attackerPosition, 5f);
        }

        if (health <= 0)
        {
            Die();
        }
    }


    public float getDamage()
    {
        return damage;
    }

    private void Die()
    {
        isDead = true;
        
        
        
     
        Destroy(gameObject, 1f);
    }

    private float GetAnimationLength(string animationName)
    {
        if (animator == null) return 1f; 

        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 1f; 
    }

    private void damageParticles()
    {
        particleSystem.Play();
    }

    public void setDamage(float newDamage)
    {
        damage = newDamage;
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void setHealth(float newHealth) 
    { 
        health = newHealth;
    }

    public enemyType getEnemyType()
    {
        return enemy;
    }

    public float getHealth()
    {
        return health;
    }

}
