using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Set data")]
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource[] soundEffects;
    
    [Header("Private data")]
    private bool soundOn;

    public void setMusic(bool active)
    {
        music.enabled = active;
    }

    public void setSound(bool active)
    {
        soundOn = active;
    }

    public void playSoundEffect(int effect)
    {
        if(soundOn)
        {
            soundEffects[effect].Play();
        }
    }

}
