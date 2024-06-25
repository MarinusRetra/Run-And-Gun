using UnityEngine;

public class HealtCrate : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().HP++;
            Destroy(gameObject);
        }
    }
}
