using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Target;
    void Update()
    {
         transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y , -10);
    }
}
