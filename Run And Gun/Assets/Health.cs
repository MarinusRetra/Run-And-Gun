using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    int hp;
    public int maxHealth;
    public int HP 
    { 
        get { return hp; }
        set {
         hp = value < 0 ? 0 : value; //hp kan niet lager dan 0
         //hp = value > maxHealth ? maxHealth : value; //hp kan niet hoger dan maxHealth
        }
    }

    ParticleSystem deathParticles;
    SpriteRenderer spriteRenderer;
    Collider2D Collider;
    public GameObject AmmoCratePrefab;


    private void Awake()
    {
       hp = maxHealth;
    }

    private void Start()
    {
        Collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        deathParticles = GetComponent<ParticleSystem>();
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
        Collider.enabled = false;
        spriteRenderer.enabled = false;
        deathParticles.Play();
        CalculateAmmoDrop(UnityEngine.Random.Range(0,5));
    }

    private void CalculateAmmoDrop(int kans)
    {
        if (kans == 1)
        {
            StartCoroutine(WaitForParticlesThenDestroy(true));
        }
        else 
        { 
            StartCoroutine(WaitForParticlesThenDestroy(false));
        }
    }

    /// <summary>
    /// Wacht totdat de particles klaar zijn met spelen en daarna wordt destroy(gameObject) aangeroepen
    /// </summary>
    /// <param name="DropAmmo">Halverwege de particles wordt ammo gespawned als DropAmmo true is</param>
    /// <returns></returns>
    IEnumerator WaitForParticlesThenDestroy(bool DropAmmo)
    {
        
        if (DropAmmo && !gameObject.CompareTag("Player")) 
        { 
            Instantiate(AmmoCratePrefab,transform.position,transform.rotation);
        }
        yield return new WaitForSeconds(deathParticles.main.duration);
        Destroy(gameObject);
    }
}
