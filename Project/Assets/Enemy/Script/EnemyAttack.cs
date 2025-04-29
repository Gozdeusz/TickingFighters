using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private float lastAttackTime = 1f; 
    [SerializeField] private float attackCooldown = 1f;

    private Enemy enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                
                other.GetComponent<Player>().takeDamage(enemy.getDamage());

                
                lastAttackTime = Time.time;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            if (Time.time - lastAttackTime >= attackCooldown)
            {

                other.GetComponent<Player>().takeDamage(enemy.getDamage());


                lastAttackTime = Time.time;
            }
        }
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    void Update()
    {
        
    }
}
