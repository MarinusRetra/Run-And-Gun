using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform FirePoint;
    public WeaponScriptable[] weaponIn;
    public GameObject Projectile;
    int CurrentWeapon = 0;

    private void Start()
    {
        //zet de AmmoCount naar MaxAmmoCount van het i en doet hetzelfde met AmmoInMagazine
        weaponIn[CurrentWeapon].blockShoot = false;
        for (int i = 0; i < weaponIn.Length; i++)
        {
            weaponIn[i].AmmoCount = weaponIn[i].MaxAmmoCount;
            weaponIn[i].AmmoInMagazine = weaponIn[i].MagazineCount;
        }
    }
    void Update()
    {
        CheckShoot();
        ReadSwapInput();
        CheckReload();
    }
    /// <summary>
    /// Wisselt naar het wapen op basis van de input
    /// </summary>
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
    /// <summary>
    /// Wisselt je wapen en je projectile naar dat van het gezette wapen.
    /// </summary>
    /// <param name="weaponIn"></param>
    void SwapWeapon(int weaponIn)
    { 
      CurrentWeapon = weaponIn;
      Projectile.GetComponent<Projectile>().SetBullet(CurrentWeapon);
    }

    /// <summary>
    /// Zet de blockShoot variable van het huidige wapen en reload vervolgens het wapen
    /// </summary>
    /// <returns></returns>
    public IEnumerator ReloadRoutine()
    {
        weaponIn[CurrentWeapon].blockShoot = true;
        yield return new WaitForSeconds(weaponIn[CurrentWeapon].ReloadTime);
        weaponIn[CurrentWeapon].blockShoot = false;
        weaponIn[CurrentWeapon].Reload();
        StopCoroutine(ReloadRoutine());
    }
    /// <summary>
    /// Checkt of je kan schieten zo, ja Schiet, zo niet als alleen je magazijn leeg is reload, ammo count en magazijn is leeg, Return leeg wapen
    /// </summary>
    void CheckShoot()
    {
        // als je meer dan 0 ammo hebt schiet een [ProjectileCount] aantal projectiles
        if (Input.GetMouseButtonDown(0) && weaponIn[CurrentWeapon].AmmoInMagazine > 0)
        {
            for (int i = 0; i < weaponIn[CurrentWeapon].ProjectileCount; i++)
            {
                weaponIn[CurrentWeapon].Shoot(Projectile, FirePoint);
                Debug.Log(weaponIn[CurrentWeapon]);
                // TO-DO verander de positie van elke kogel voor een bullet spread effect
            }
        }
        // als je magazijn leeg is en je totale ammo 0 is dan gaat dit af
        else if (Input.GetMouseButtonDown(0) && weaponIn[CurrentWeapon].AmmoCount < 1 && weaponIn[CurrentWeapon].AmmoInMagazine < 1)
        {
            Debug.Log("Empty Gun");
        }
    }

     /// <summary>
     /// Reload Wapen als je geen ammo hebt en blokkeerd de inputs van nog een reload terwijl de coroutine bezig is 
     /// </summary>
    void CheckReload()
    {
        if (Input.GetKeyDown(KeyCode.R) || weaponIn[CurrentWeapon].AmmoInMagazine < 1 && weaponIn[CurrentWeapon].AmmoCount > 0)
        {
            if (!weaponIn[CurrentWeapon].blockShoot)
            {
                StartCoroutine(ReloadRoutine());
            }
            else
            {
                Debug.Log("InReload");
            }
        }
    }
}
