using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    // Start is called before the first frame update
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
