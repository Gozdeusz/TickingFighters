using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().takeDamage(10, transform.position);

        }

    }
    void Update()
    {
        
    }
}
