using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon")]
public class WeaponScriptable : ScriptableObject
{
    public int ProjectileVelocity;
    public int ProjectileCount;
    public float ReloadTime;
    public string WeaponName;
    public Sprite WeaponSprite;
    public int MaxAmmoCount;
    public int AmmoCount;
    public int AmmoInMagazine;
    public int MagazineCount;
    public bool blockShoot = false;
    
    //public WeaponScriptable(int projectileCountIn, string nameIn, Sprite spriteIn, int maxAmmoCountIn, int magazineCountIn)
    //{
    //    ProjectileCount = projectileCountIn;
    //    WeaponName = nameIn;
    //    WeaponSprite = spriteIn;
    //    MaxAmmoCount = maxAmmoCountIn;
    //    MagazineCount = magazineCountIn;
    //    AmmoCount = MaxAmmoCount;
    //    AmmoInMagazine = MagazineCount;
    //}

    /// <summary>
    /// Instantiate een kogel op de Gegeven Firepoint
    /// </summary>
    /// <param name="projectileIn"></param>
    /// <param name="FirePoint"></param>
    public void Shoot(GameObject projectileIn, Transform FirePoint)
    {
        if (!blockShoot)
        { 
            GameObject Projectile = Instantiate(projectileIn, FirePoint.position, FirePoint.rotation);
            Rigidbody2D rb = Projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(FirePoint.forward * ProjectileVelocity, ForceMode2D.Impulse);
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
