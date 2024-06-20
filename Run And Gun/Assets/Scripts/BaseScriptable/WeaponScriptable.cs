using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon")]
public class WeaponScriptable : ScriptableObject
{
    [SerializeField] int ammoCount;
    public int ProjectileVelocity;
    public int ProjectileCount;
    public float ReloadTime;
    public string WeaponName;
    public Sprite WeaponSprite;
    public int MaxAmmoCount;
    public int AmmoCount 
    { get { return ammoCount; } // Zorgt dat je niet boven MaxAmmoCount kan gaan
      set { ammoCount = value <= MaxAmmoCount ? value : MaxAmmoCount; }  } 
    public int AmmoInMagazine;
    public int MagazineCount;
    public bool blockShoot = false;

    /// <summary>
    /// Instantiate een kogel op de Gegeven Firepoint en set de LifeTime van de kogel
    /// </summary>
    /// <param name="projectileIn"></param>
    /// <param name="FirePoint"></param>
    public void Shoot(GameObject projectileIn, Transform FirePoint, float LifeTimeIn, int damageIn, int weaponIn)
    {
        if (!blockShoot)
        {
            GameObject Projectile;
            if (weaponIn == 2)
            { 
                Projectile = Instantiate(projectileIn, new Vector2(FirePoint.position.x + Random.Range(0f, 0.2f), FirePoint.position.y + Random.Range(0f , 0.5f)), FirePoint.rotation);
            }
            else 
            {
               Projectile = Instantiate(projectileIn, FirePoint.position, FirePoint.rotation);
            }

            Rigidbody2D rb = Projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(FirePoint.forward * ProjectileVelocity, ForceMode2D.Impulse);
            Projectile.GetComponent<Projectile>().BulletLifeTime = LifeTimeIn;
            Projectile.GetComponent<Projectile>().Damage = damageIn;
            Random.Range(0, MagazineCount);
            AmmoInMagazine--;
        }
    }
    /// <summary>
    /// Zet de ammo om een reload te imiteren
    /// </summary>
    public void Reload()
    {
        if (AmmoCount < MagazineCount)
        {
            AmmoInMagazine = AmmoCount;
            AmmoCount = 0;
        }
        else 
        { 
            AmmoCount -= MagazineCount;
            AmmoInMagazine = MagazineCount;
        }
    }
    /// <summary>
    /// Override ToString zodat ik het wapen naam en ammo count terug geef
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{WeaponName} Ammo: {AmmoInMagazine} | {AmmoCount}";
    }
}
