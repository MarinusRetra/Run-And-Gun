using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Bullet[] Bullet;
    private int currentBullet = 0;

    public void SetBullet(int bulletIn)
    {
        currentBullet = bulletIn;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet hit");
        // collision.TakeDamage(Bullet[currentBullet].Damage);
    }

}
