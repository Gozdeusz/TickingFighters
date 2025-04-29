using UnityEngine;

public class ThrowKnife : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject knifePrefab; // Prefab sztyletu
    [SerializeField] private Transform throwPoint; // Punkt rzutu
    [SerializeField] private float knifeSpeed = 10f; // Prędkość rzutu
    [SerializeField] private float knifeLifetime = 2f; // Czas życia sztyletu
    [SerializeField] private float rotationSpeed = 500f; // Prędkość obrotu wokół własnej osi

    [SerializeField] private WeaponCooldown weaponCooldown;
    

    void Start()
    {
      
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && weaponCooldown.IsReady) 
        {
            Throw();
            weaponCooldown.StartCooldown();
        }
    }

    void Throw()
    {
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 throwDirection = (mousePosition - throwPoint.position).normalized;

      
        GameObject knife = Instantiate(knifePrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = knife.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
           
            rb.linearVelocity = throwDirection * knifeSpeed;

           
            float angle = Mathf.Atan2(throwDirection.y, throwDirection.x) * Mathf.Rad2Deg;
            knife.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        
        KnifeRotation knifeRotation = knife.AddComponent<KnifeRotation>();
        knifeRotation.rotationSpeed = rotationSpeed;

        
        Destroy(knife, knifeLifetime);
    }
}

public class KnifeRotation : MonoBehaviour
{
    public float rotationSpeed = 360f; 

    void Update()
    {
        
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
