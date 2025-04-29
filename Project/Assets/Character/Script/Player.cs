using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStatBar playerStatBar;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject statbar;
    
   
    void Start()
    {
        if (health == 0 || maxHealth == 0 || energy == 0 || maxEnergy == 0)
        {
            health = 100;
            maxHealth = 100;
            maxEnergy = 100;
            energy = 100;
            playerStatBar.setMaxHealth(maxHealth);
            playerStatBar.setMaxEnergy(energy);
        }
    }

        public float takeDamage(float damage)
    {
        health -= damage;
        playerStatBar.setHealth(health);
        Debug.Log("Zostales zaatakowany");
        animator.SetTrigger("takeDamage");
        return health - damage;

    }

  

    private void Update()
    {
        OnDestroy();
    }

    private void OnDestroy()
    {
        if (health <= 0)
        {
            statbar.SetActive(false);
            deathScreen.SetActive(true);
            
            Object.Destroy(this);
            Destroy(this.gameObject, 1f);
        }
    }


    public void subEnergy(float energyPoint)
    {
        energy -= energyPoint;
        playerStatBar.setEnergy(energy);
    }
    public float getEnergy()
    {
        return energy;
    }
    public float getMaxEnergy()
    {
        return maxEnergy;
    }

    public void addEnergy(float energyPoint)
    {
        energy += energyPoint;
        playerStatBar.setEnergy(energy);
    }

}
