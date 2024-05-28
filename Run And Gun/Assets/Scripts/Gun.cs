using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform FirePoint;
    public WeaponScriptable[] weaponIn;
    public GameObject Projectile;
    int CurrentWeapon = 0;
    void Update()
    {
        CheckShoot();
        ReadSwapInput();
    }

    void ReadSwapInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapWeapon(2);
        }
    }

    void SwapWeapon(int weaponIn)
    { 
      CurrentWeapon = weaponIn;
      Projectile.GetComponent<Projectile>().SetBullet(CurrentWeapon);
    }

    void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0) && weaponIn[CurrentWeapon].AmmoInMagazine > 0)
        {
            for (int i = 0; i < weaponIn[CurrentWeapon].ProjectileCount; i++)
            { 
                weaponIn[CurrentWeapon].Shoot(Projectile, FirePoint);
                Debug.Log(weaponIn[CurrentWeapon]);
                // verander de positie van elke kogel voor een bullet spread effect
            }
        }
        else if (Input.GetMouseButtonDown(0) && weaponIn[CurrentWeapon].AmmoInMagazine == 0)
        {
            weaponIn[CurrentWeapon].Reload();
        }
        else if (Input.GetMouseButtonDown(0) && weaponIn[CurrentWeapon].AmmoCount == 0)
        {
            Debug.Log("Empty Gun");
        }
    }
}
