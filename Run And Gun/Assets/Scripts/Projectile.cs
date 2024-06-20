using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float BulletLifeTime;
    [HideInInspector] public int Damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet hit");
        Destroy(gameObject);
        collision.gameObject.GetComponent<Health>().TakeDamage(Damage);
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
