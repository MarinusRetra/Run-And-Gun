using System.Collections;
using System.Linq;
using UnityEngine;
using static WeaponScriptable; // dit is om de enum WeaponType te kunnen gebruiken

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
            weaponIn[i].blockShoot = false;
        }

        if (gameObject.CompareTag("AI"))
        {
            StartCoroutine(AIShoot());
        }

    }

    IEnumerator AIShoot()
    {
        yield return new WaitForSeconds(weaponIn[CurrentWeapon].ReloadTime-Random.Range(0,0.5f));
        Shoot();
        StartCoroutine(AIShoot());
    }

    void Update()
    {
        if (gameObject.CompareTag("Player"))
        { 
            CheckShoot();
            ReadSwapInput();
            CheckReload();
        }
    }
    /// <summary>
    /// Wisselt naar het wapen op basis van de input
    /// </summary>
    void ReadSwapInput()
    {
        if (gameObject.CompareTag("Player"))// swapt alleen de speler zijn wapen
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
    }
    /// <summary>
    /// Wisselt je wapen, je projectile en de sprite naar dat van het gezette wapen.
    /// </summary>
    /// <param name="weaponIn"></param>
    void SwapWeapon(int weaponIn)
    {
        CurrentWeapon = weaponIn;
        GetComponent<SpriteRenderer>().sprite = this.weaponIn[CurrentWeapon].WeaponSprite;
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
        weaponIn[currentWeapon].Reload();
        weaponIn[currentWeapon].blockShoot = false;
        Debug.Log(weaponIn[currentWeapon]);
    }
    /// <summary>
    /// Checkt of je kan schieten zo, ja Schiet, zo niet als alleen je magazijn leeg is reload, als ammo count en magazijn 0 zijn print "Empty Gun"
    /// </summary>
    void CheckShoot()
    {
        // als je meer dan 0 ammo hebt schiet een [ProjectileCount] aantal projectiles
        if (Input.GetMouseButtonDown(0) && weaponIn[CurrentWeapon].AmmoInMagazine > 0)
        {
            Shoot();
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



    /// <summary>
    /// Instantiate een kogel op de Gegeven Firepoint en set de LifeTime van de kogel
    /// </summary>
    /// <param name="Projectile"></param>
    /// <param name="FirePoint"></param>
    void Shoot()
    {
        if (!weaponIn[CurrentWeapon].blockShoot)
        {
            //pulse Pos wordt gebruikt om van zichzelf af te trekken om een alternerend shietpatroon te maken
            float pulsePos = -0.2f;
            for (int i = 0; i < weaponIn[CurrentWeapon].ProjectileCount; i++)
            {
                GameObject Projectile; 
                // hij maakt elke keer een nieuwe projectile aan als er geschoten wordt en verwijderd daarna dit
                // GameObject in plaats van de originele projectile
                switch (weaponIn[CurrentWeapon].thisWeapon)
                {
                    case WeaponType.Shotgun:
                        Projectile = Instantiate(this.Projectile, new Vector2(FirePoint.position.x + Random.Range(-0.3f, 0.3f), FirePoint.position.y + Random.Range(-0.5f, 0.8f)), FirePoint.rotation);
                        break;

                    case WeaponType.DB:
                        Projectile = Instantiate(this.Projectile, new Vector2(FirePoint.position.x, FirePoint.position.y - pulsePos), FirePoint.rotation);
                        pulsePos = -pulsePos;
                        break;

                    case WeaponType.Sniper:
                        Projectile = Instantiate(this.Projectile, FirePoint.position, FirePoint.rotation);
                        break;

                    default:
                        Projectile = Instantiate(this.Projectile, FirePoint.position, FirePoint.rotation);
                        break;
                }

                weaponIn[CurrentWeapon].AmmoInMagazine--;
                Rigidbody2D rb = Projectile.GetComponent<Rigidbody2D>();
                rb.AddForce(Projectile.transform.forward * weaponIn[CurrentWeapon].ProjectileVelocity, ForceMode2D.Impulse);
                Projectile.GetComponent<Projectile>().BulletLifeTime = weaponIn[CurrentWeapon].ProjectileLifeTime;
                Projectile.GetComponent<Projectile>().Damage = weaponIn[CurrentWeapon].Damage;
            }
        }
    }
}
