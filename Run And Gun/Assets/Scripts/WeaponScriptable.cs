using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon")]
public class WeaponScriptable : ScriptableObject
{
    [HideInInspector] public Projectile bullet;
    public int ProjectileCount;
    public int Damage;
    public string WeaponName;
    public Sprite WeaponSprite;
    public float ProjectileLifeTime;
    public int MaxAmmoCount;
    public int AmmoCount { get; private set; }
    public int AmmoInMagazine { get; private set; }  
    public int MagazineCount;

    public WeaponScriptable(int projectileCountIn, int damageIn, string nameIn, Sprite spriteIn, float lifetimeIn, int maxAmmoCountIn, int magazineCountIn  )
    { 
        ProjectileCount = projectileCountIn;
        Damage = damageIn;
        WeaponName = nameIn;
        WeaponSprite = spriteIn;
        ProjectileLifeTime = lifetimeIn;
        MaxAmmoCount = maxAmmoCountIn;
        MagazineCount = magazineCountIn;
        AmmoCount = MaxAmmoCount;
        AmmoInMagazine = MagazineCount;
    }

    public void Shoot()
    {
        Instantiate(bullet);
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.forward);
        if (AmmoInMagazine > 0)
        { 
            AmmoCount--;
        }
    }
    public void Reload()
    {
        AmmoInMagazine = MagazineCount;
        AmmoCount -= MagazineCount;
    }

    public override string ToString()
    {
        return WeaponName;
    }
}

