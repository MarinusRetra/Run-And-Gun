using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Target; //De target is bijna altijd de speler

    void Update()
    {
        if (Target.transform.position.y > -6)
        {
            transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 2 + Input.GetAxis("Vertical") * 2 , -10);
        }
    }
    
}
