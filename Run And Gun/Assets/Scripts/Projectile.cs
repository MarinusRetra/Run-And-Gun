using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float BulletLifeTime;
    [HideInInspector] public int Damage;
    public GameObject Particles;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(Damage);
        }   catch { }
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(KillBullet(BulletLifeTime));
    }

    IEnumerator KillBullet(float _lifeTimeIn)
    { 
      yield return new WaitForSeconds(_lifeTimeIn);
      Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(Particles , new Vector2(gameObject.transform.position.x ,gameObject.transform.position.y), gameObject.transform.rotation);
    }


}
