using UnityEngine;

[CreateAssetMenu (fileName = "Projectile", menuName ="Projectile")]
public class Bullet : ScriptableObject
{
    public int Damage;
    public float LifeTime;
}
