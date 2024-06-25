using UnityEngine;

public class AITargeting : MonoBehaviour
{
    public GameObject targetObject;
    void Update()
    {
        
        Vector3 direction = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + 1, targetObject.transform.position.z) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
    }
}
