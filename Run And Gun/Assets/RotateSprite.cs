using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public GameObject targetObject;
    private void Update()
    {
        var target = targetObject.transform;
        Vector3 direction = target.position - transform.position;

        //De angle in graden
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        angle += 180;

        //Zet de rotatie
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
