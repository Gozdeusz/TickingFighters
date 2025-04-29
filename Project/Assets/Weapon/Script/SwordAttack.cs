using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private float swordDamage;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private WeaponCooldown weaponCooldown;

    private bool isAttack = false;

    private void Start()
    {
        swordDamage = 1; 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttack)
        {

            if (other.tag == "Enemy")
            {
                other.GetComponent<Enemy>().takeDamage(swordDamage, transform.position);
            }
        }
       
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && weaponCooldown.IsReady)
        {
            weaponAnimator.SetTrigger("Attack");
            weaponCooldown.StartCooldown();
            isAttack = true;

            StartCoroutine(ResetAttack(0.5f)); 
        }
    }

    IEnumerator ResetAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttack = false;
    }

}
