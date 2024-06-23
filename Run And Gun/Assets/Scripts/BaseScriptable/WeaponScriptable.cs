using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon")]
public class WeaponScriptable : ScriptableObject
{
    [SerializeField] int ammoCount;
    public enum WeaponType  { None, DB, Shotgun, Sniper}
    public WeaponType thisWeapon;
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
    public void Shoot(GameObject projectileIn, Transform FirePoint, float LifeTimeIn, int damageIn, int projectileCountIn)
    {
        if (!blockShoot)
        {
           //pulse Pos wordt gebruikt om van zichzelf af te trekken om een alternerend shietpatroon te maken
           float pulsePos = -0.2f; 
           for (int i = 0; i < projectileCountIn; i++)
           {
              switch (thisWeapon)
              {
              case WeaponType.Shotgun:
                  projectileIn = Instantiate(projectileIn, new Vector2(FirePoint.position.x + Random.Range(-0.3f, 0.3f), FirePoint.position.y + Random.Range(-0.5f, 0.8f)), FirePoint.rotation);
              break;
              
              case WeaponType.DB:
                   projectileIn = Instantiate(projectileIn, new Vector2(FirePoint.position.x, FirePoint.position.y - pulsePos), FirePoint.rotation);
                   pulsePos = -pulsePos;
                  break;

              case WeaponType.Sniper:
                 projectileIn = Instantiate(projectileIn, FirePoint.position, FirePoint.rotation);
              break;
              
              default:
                  projectileIn = Instantiate(projectileIn, FirePoint.position, FirePoint.rotation);
              break;
              }
          
              AmmoInMagazine--;
              Rigidbody2D rb = projectileIn.GetComponent<Rigidbody2D>();
              rb.AddForce(projectileIn.transform.forward * ProjectileVelocity, ForceMode2D.Impulse);
              projectileIn.GetComponent<Projectile>().BulletLifeTime = LifeTimeIn;
              projectileIn.GetComponent<Projectile>().Damage = damageIn;
              Random.Range(0, MagazineCount);
           }
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
