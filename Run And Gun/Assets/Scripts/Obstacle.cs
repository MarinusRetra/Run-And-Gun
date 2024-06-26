using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int Damage = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(Damage);
        }
        catch { }
    }
}
