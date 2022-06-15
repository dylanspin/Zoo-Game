using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGameAudio : MonoBehaviour
{
    [SerializeField] private AudioSource[] audios;
    [SerializeField] private AudioSource music;
    private static AudioSource[] staticAudios;
    private static bool sound = false;

    public void setStart(bool soundOn, bool musicOn)
    {
        if(musicOn)
        {
            music.Play();
        }
        sound = soundOn ;
        staticAudios = audios;
    }

    public static void playSoundEffect(int soundId)
    {
        if(sound)
        {
            staticAudios[soundId].Play();
        }
    }


}
