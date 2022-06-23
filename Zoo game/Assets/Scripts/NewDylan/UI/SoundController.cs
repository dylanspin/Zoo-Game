using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Set data")]
    [SerializeField] private AudioSource music;//the background music 
    [SerializeField] private AudioSource[] soundEffects;//ui sound effects
    
    [Header("Private data")]
    private bool soundOn;//the current state of the sound setting

    public void setMusic(bool active)//sets the music setting
    {
        music.enabled = active;
    }

    public void setSound(bool active)//sets the setting 
    {
        soundOn = active;
    }

    public void playSoundEffect(int effect)
    {
        if(soundOn)//if the sound setting is on
        {
            soundEffects[effect].Play();
        }
    }
}
