using UnityEngine;

//TODO Haal ammo uit het scriptable object

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon")]
public class WeaponScriptable : ScriptableObject
{
    [SerializeField] int ammoCount;
    public enum WeaponType  { None, Pulse, Shotgun, Sniper}
    public WeaponType thisWeapon;
    public float SpreadAngle;
    public AudioClip GunSound;
    public int ProjectileVelocity;
    public int ProjectileCount;
    public float ProjectileLifeTime;
    public int Damage;
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
