using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{ 
    public static float Volume = 100;
    static Transform Player;

    private void Start()
    {
       Player = GameObject.Find("Player").transform;
    }

    public static void PlaySoundEffect(AudioClip audioClipIn)
    {
        AudioSource.PlayClipAtPoint(audioClipIn, Player.position, Volume);
    }
}
