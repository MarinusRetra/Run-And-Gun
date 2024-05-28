using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon")]
public class WeaponScriptable : ScriptableObject
{
    public int ProjectileCount;
    public string WeaponName;
    public Sprite WeaponSprite;
    public int MaxAmmoCount;
    public int AmmoCount { get; private set; }
    public int AmmoInMagazine { get; private set; }
    public int MagazineCount;

    public WeaponScriptable(int projectileCountIn, string nameIn, Sprite spriteIn, int maxAmmoCountIn, int magazineCountIn)
    {
        ProjectileCount = projectileCountIn;
        WeaponName = nameIn;
        WeaponSprite = spriteIn;
        MaxAmmoCount = maxAmmoCountIn;
        MagazineCount = magazineCountIn;
        AmmoCount = MaxAmmoCount;
        AmmoInMagazine = MagazineCount;
    }

    public void Shoot(GameObject projectileIn, Transform FirePoint)
    {
        GameObject Projectile = Instantiate(projectileIn, FirePoint.position, FirePoint.rotation);
        Rigidbody2D rb = Projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePoint.forward * 30, ForceMode2D.Impulse);
        AmmoInMagazine--;
    }

    public void Reload()
    {
        AmmoCount -= MagazineCount;
        AmmoInMagazine = MagazineCount;
    }

    public override string ToString()
    {
        return $"{WeaponName} Ammo: {AmmoCount}";
    }

}
