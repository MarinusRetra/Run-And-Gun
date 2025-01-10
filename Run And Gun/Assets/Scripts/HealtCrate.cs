using UnityEngine;

public class HealtCrate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            trigger.gameObject.GetComponent<Health>().HP++;
            Destroy(gameObject);
        }
    }
}
