using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    WeaponScriptable Gun;
    private float lifeTime;
    private int damage;

    private void Awake()
    {
        Gun = GetComponent<Gun>().weaponIn;
        SetBullet();
    }

    private void OnCollisionEnter(Collision collision)
    {
      //  collision.TakeDamage(damage);
    }

    public void SetBullet()
    { 
        damage = Gun.Damage;
        lifeTime = Gun.ProjectileLifeTime;
        Gun.bullet = this;
    }

}
