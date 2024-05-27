using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public WeaponScriptable weaponIn;
    [SerializeField] Projectile bullet;

    void Start()
    {
        bullet = weaponIn.bullet;
    }

    void Update()
    {
        TryShoot();
    }

    public void TryShoot()
    {
        if (Input.GetMouseButtonDown(0) && weaponIn.AmmoInMagazine > 0)
        {
            weaponIn.Shoot();
        }
        else if (Input.GetMouseButtonDown(0) && weaponIn.AmmoInMagazine == 0)
        { 
            weaponIn.Reload();
        }
    }
}
