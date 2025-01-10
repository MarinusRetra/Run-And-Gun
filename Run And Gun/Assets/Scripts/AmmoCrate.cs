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
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            trigger.gameObject.GetComponentInChildren<Gun>().AddAmmo(Ammo+2);
            // Hier doe ik +2 om ammo te kunnen gebruiken hoe ik wil zonder dat ik de logic voor 
            // Ammo is het nummer wat op de crate staat wat laat zien hoeveel ammo er in de crate zit.
            Destroy(gameObject);
        }
    }
}
