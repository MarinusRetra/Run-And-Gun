using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform FirePoint;
    public WeaponScriptable[] weaponIn;
    public GameObject Projectile;
    public Bullet[] Bullet;
    private int currentBullet = 0;
    int CurrentWeapon = 0;

    private void Start()
    {
        //zet de AmmoCount naar MaxAmmoCount van het i en doet hetzelfde met AmmoInMagazine
        weaponIn[CurrentWeapon].blockShoot = false;
        for (int i = 0; i < weaponIn.Length; i++)
        {
            weaponIn[i].AmmoCount = weaponIn[i].MaxAmmoCount;
            weaponIn[i].AmmoInMagazine = weaponIn[i].MagazineCount;
            weaponIn[i].blockShoot = false;
        }
        //GetComponent<SpriteRenderer>().sprite = weaponIn[CurrentWeapon].WeaponSprite;
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
    /// Wisselt je wapen, je projectile en de sprite naar dat van het gezette wapen.
    /// </summary>
    /// <param name="weaponIn"></param>
    void SwapWeapon(int weaponIn)
    {
            CurrentWeapon = weaponIn;
            SetBullet(CurrentWeapon);
            GetComponent<SpriteRenderer>().sprite = this.weaponIn[CurrentWeapon].WeaponSprite;
    }

    /// <summary>
    /// Wisselt De kogel die geschoten wordt, niet het wapen
    /// </summary>
    /// <param name="bulletIn"></param>
    void SetBullet(int bulletIn)
    {
        currentBullet = bulletIn;
    }


    /// <summary>
    /// Deelt de ammo count door ammoIn en voegt het resultaat toe aan het huidige wapen 
    /// en dat resultaat nog 1 keer gedeeld door 2 toe aan de andere wapens
    /// </summary>
    /// <param name="ammoIn"></param>
    public void AddAmmo(int ammoIn)
    {
        var currentGun = weaponIn[CurrentWeapon];
        currentGun.AmmoCount += currentGun.MaxAmmoCount / ammoIn;
        foreach(var g in  weaponIn)
        {
            if (g != currentGun)
            {
                g.AmmoCount += g.MaxAmmoCount / ammoIn / 2;
            }
        }
    }

    /// <summary>
    /// Zet de blockShoot variable van het huidige wapen en reload vervolgens het wapen
    /// </summary>
    /// <returns></returns>
    public IEnumerator ReloadRoutine(int currentWeapon)
    {
        weaponIn[currentWeapon].blockShoot = true;
        yield return new WaitForSeconds(weaponIn[currentWeapon].ReloadTime);
        weaponIn[currentWeapon].blockShoot = false;
        weaponIn[currentWeapon].Reload();
        Debug.Log(weaponIn[currentWeapon]);
        //StopCoroutine(ReloadRoutine(currentWeapon));
    }
    /// <summary>
    /// Checkt of je kan schieten zo, ja Schiet, zo niet als alleen je magazijn leeg is reload, ammo count en magazijn is leeg, Return leeg wapen
    /// </summary>
    void CheckShoot()
    {
        // als je meer dan 0 ammo hebt schiet een [ProjectileCount] aantal projectiles
        if (Input.GetMouseButtonDown(0) && weaponIn[CurrentWeapon].AmmoInMagazine > 0)
        {
            weaponIn[CurrentWeapon].Shoot(Projectile, FirePoint, Bullet[currentBullet].LifeTime,Bullet[currentBullet].Damage, weaponIn[CurrentWeapon].ProjectileCount);
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
                StartCoroutine(ReloadRoutine(CurrentWeapon));
            }
            else
            {
                Debug.Log("InReload");
            }
        }
    }
}
