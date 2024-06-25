using System.Collections;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitForParticle());
    }

    IEnumerator WaitForParticle()
    {
       yield return new WaitForSeconds(gameObject.GetComponent<ParticleSystem>().main.duration+0.5f);
       Destroy(gameObject);
    }

}
