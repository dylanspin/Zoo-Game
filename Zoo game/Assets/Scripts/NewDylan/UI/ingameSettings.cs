using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingameSettings : MonoBehaviour
{
    [SerializeField] private AudioSource[] audios;
    [SerializeField] private Translate translateScript;
    [SerializeField] private EndScreen endScript;

    public void setSettings(bool soundOn,bool musicOn,int langues)
    {
        //still needs to set the audio
        translateScript.setLangues(langues);
        endScript.setLangues(langues);
    }
}
