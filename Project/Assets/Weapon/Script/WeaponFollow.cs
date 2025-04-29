using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    public Transform player; //Punkt obrotu
    public float radius = 1.5f; // Promieñ ruchu miecza
    public float smoothSpeed = 10f; // Szybkoœæ pod¹¿ania

    private float angle = 0f; // K¹t miecza

    void Update()
    {
        //Kierunek graacza
        bool isGoingLeft = player.localScale.x < 0;
        
        //Pozycja myszki
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        //wektor kierunku do myszy
        Vector3 direction = (mousePosition - player.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (isGoingLeft)
        {
           targetAngle = Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg;
        }
        
       
        //Kat 140
        targetAngle = Mathf.Clamp(targetAngle, -70f, 70f);

        // Plynna zmiana kata
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * smoothSpeed);

     
        

     
        float radianAngle = angle * Mathf.Deg2Rad;
        if (isGoingLeft)
        {
            targetAngle = Mathf.Clamp(targetAngle, 70f, -70f);
            
            
        }
        Vector3 offset = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0) * radius;

       
        if (isGoingLeft)
        {
            offset.x = -offset.x; 
        }

        
        transform.position = player.position + offset;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
