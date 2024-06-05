using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float BulletLifeTime;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet hit");
        //collision.TakeDamage(Bullet[currentBullet].Damage);
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
