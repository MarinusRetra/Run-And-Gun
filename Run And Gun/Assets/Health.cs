using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    int hp;
    int maxHealth;
    public int HP 
    { 
        get { return hp; }
        set {
         hp = value < 0 ? 0 : value; //hp kan niet lager dan 0
         hp = value > maxHealth ? maxHealth : value; //hp kan niet hoger dan maxHealth
        }
    }
    private void Awake()
    {
        maxHealth = hp;
    }

    public void TakeDamage(int damageIn)
    { 
        HP -= damageIn;
        if (HP == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        CalculateAmmoDrop(UnityEngine.Random.Range(0,5));
    }

    private void CalculateAmmoDrop(int kans)
    {
        if (kans == 1)
        {
            Debug.Log("AmmoDrop");
        }
        throw new NotImplementedException();
    }
}
