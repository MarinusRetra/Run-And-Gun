using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    public Sprite[] possibleSprites;

    SpriteRenderer spriteRenderer;
    int Ammo;

    private void Start()
    {
        Ammo = Random.Range(0,4);
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = possibleSprites[Ammo];
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInChildren<Gun>().AddAmmo(Ammo+2);
            // Hier doe ik +2 om ammo te kunnen gebruiken hoe ik wil zonder dat ik de logic voor 
            // de crate sprite hoef te veranderen.
            Destroy(gameObject);
        }
    }
}
