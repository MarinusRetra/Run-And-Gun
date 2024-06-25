using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] int hp;
    public int maxHealth;
    bool iFrames = false;
    public AudioClip DeathSound;
    public int HP 
    { 
        get { return hp; }
        set {
         hp = value < 0 ? 0 : value; //hp kan niet lager dan 0
         hp = value > maxHealth ? maxHealth : value; //hp kan niet hoger dan maxHealth
        }
    }

    ParticleSystem deathParticles;
    SpriteRenderer spriteRenderer;
    Collider2D Collider;
    Gun weapon;
    SpriteRenderer weaponSpriteRenderer;

    public GameObject AmmoCratePrefab;
    public GameObject HealtCratePrefab;


    private void Awake()
    {
       
       hp = maxHealth;
    }

    private void Start()
    {
        Collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        deathParticles = GetComponent<ParticleSystem>();
        weapon = gameObject.GetComponentInChildren<Gun>();
        weaponSpriteRenderer = weapon.GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(int damageIn)
    {
        if (!iFrames)
        {
            HP -= damageIn;
            if (HP <= 0)
            {
                Die();
            }
            if (gameObject.CompareTag("Player"))
            { 
                StartCoroutine(PlayerDamaged());
            }
        }
    }

    private void Die()
    {
        weapon.StopAllCoroutines();
        AudioScript.PlaySoundEffect(DeathSound);

        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        weaponSpriteRenderer.enabled = false;
        weapon.enabled = false;
        Collider.enabled = false;
        spriteRenderer.enabled = false;

        deathParticles.Play();
        CalculateAmmoDrop(Random.Range(0,5));
    }

    private void CalculateAmmoDrop(int kans)
    {
        if (kans == 1)
        {
            StartCoroutine(WaitForParticlesThenDestroy(true));
        }
        if (kans == 2)
        {
            Instantiate(HealtCratePrefab, transform.position, transform.rotation);
        }
        else if (kans < 2)
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


    IEnumerator PlayerDamaged()
    {
        iFrames = true;
        for (int i = 0; i < 4; i++)
        {
           spriteRenderer.enabled = false;
           yield return new WaitForSeconds(0.1f);
           spriteRenderer.enabled = true;
           yield return new WaitForSeconds(0.1f);
        }
        iFrames = false;
    }
}
