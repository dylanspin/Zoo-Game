using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGameAudio : MonoBehaviour
{
    [SerializeField] private AudioSource[] audios;
    private static AudioSource[] staticAudios;

    private void Start()
    {
        staticAudios = audios;
    }

    public static void playSoundEffect(int soundId)
    {
        staticAudios[soundId].Play();
    }


}
