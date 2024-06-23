using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float BulletLifeTime;
    [HideInInspector] public int Damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        try
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(Damage);
        }   catch { }
    }
    private void Start()
    {
        StartCoroutine(KillBullet());
    }

    IEnumerator KillBullet()
    { 
      yield return new WaitForSeconds(BulletLifeTime);
      Destroy(gameObject);
    }

    
}
