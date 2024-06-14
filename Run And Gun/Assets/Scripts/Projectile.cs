using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float BulletLifeTime;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet hit");
        Destroy(gameObject);
        //collision.GetComponent<Health>().TakeDamage(Bullet[currentBullet].Damage);
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
