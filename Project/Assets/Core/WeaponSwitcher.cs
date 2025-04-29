using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Transform player; 
    public GameObject[] weapons; 
    private int currentWeaponIndex = 0;
    [SerializeField] private GameObject weaponMark;

    void Start()
    {
       
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[currentWeaponIndex].SetActive(true);
    }

    void Update()
    {
       //Testowa zmiana bronii
        /*if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            SwitchWeapon(1); 
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            SwitchWeapon(-1); 
        }

   
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1); 
        }*/
        
    }

    public void SwitchWeapon(int direction)
    {
       
        weapons[currentWeaponIndex].SetActive(false);

    
        currentWeaponIndex += direction;

        
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1;
        }
        else if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0;
        }


        weapons[currentWeaponIndex].SetActive(true);
        weaponMark.transform.rotation = Quaternion.Euler(0, 0, weaponMark.transform.rotation.eulerAngles.z + 90);


    }
}
