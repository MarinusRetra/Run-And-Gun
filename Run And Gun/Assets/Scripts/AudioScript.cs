using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{ 
    public static float Volume = 100;
    public Slider VolumeSlider;
    public TextMeshProUGUI volumeText;
    static Transform Player;

    private void Start()
    {
       Player = GameObject.Find("Main Camera").transform;
    }

    public static void PlaySoundEffect(AudioClip audioClipIn)
    {
        AudioSource.PlayClipAtPoint(audioClipIn, Player.position, Volume/100);
    }

    public void SetVolume()
    { 
       Volume = VolumeSlider.value;
       volumeText.text = Volume.ToString();
    }
}
