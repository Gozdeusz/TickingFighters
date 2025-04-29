using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float radius = 0.2f;
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float shootAngleOffset = 10f;
    [SerializeField] private WeaponCooldown weaponCooldown;
    [SerializeField] private float arrowLifetime = 1f;
    [SerializeField] private float arrowSpeed = 10f;
   
    private float angle = 0f; 
    private bool isGoingLeft = false;
    void Update()
    {
        
         isGoingLeft = player.localScale.x < 0;

        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; 

       
        Vector3 direction = (mousePosition - player.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

       
        if (isGoingLeft)
        {
            targetAngle = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg;
        }

        targetAngle = Mathf.Clamp(targetAngle, -70f, 70f);

      
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * smoothSpeed);

        
        float radianAngle = angle * Mathf.Deg2Rad;

      
        Vector3 offset = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0) * radius;

       
        if (isGoingLeft)
        {
            offset.x = -offset.x;
        }

      
        transform.position = player.position + offset;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (isGoingLeft) {
            transform.rotation = Quaternion.Euler(0, 0, -angle);
        }
        
        

        
        if (Input.GetMouseButtonDown(0) && weaponCooldown.IsReady)
        {
            ShootArrows();
            weaponCooldown.StartCooldown();
        }

        
    }

    void ShootArrows()
    {
        float[] shootAngles = new float[] { -shootAngleOffset, 0f, shootAngleOffset };

        foreach (float offsetAngle in shootAngles)
        {
            float shootDirection = angle + offsetAngle;
            Debug.Log(shootDirection);
            if (isGoingLeft)
            {
                shootDirection = 180f + (-angle + offsetAngle);
                Debug.Log(shootDirection);
            }

            Vector3 shootDirectionVector = new Vector3(Mathf.Cos(shootDirection * Mathf.Deg2Rad), Mathf.Sin(shootDirection * Mathf.Deg2Rad), 0);

            
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.Euler(0, 0, shootDirection-45));

            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = shootDirectionVector * arrowSpeed; // Nadajemy prędkość
            }

            Destroy(arrow, arrowLifetime);
        }
    }
}
