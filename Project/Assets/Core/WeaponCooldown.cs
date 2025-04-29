using UnityEngine;
using System.Collections;

public class WeaponCooldown : MonoBehaviour
{
    [SerializeField] private float cooldownTime = 1f; 
    private bool isOnCooldown = false;

    public bool IsReady => !isOnCooldown; 

    public void StartCooldown()
    {
        if (!isOnCooldown)
        {
            StartCoroutine(CooldownRoutine());
        }
    }

    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }
}
