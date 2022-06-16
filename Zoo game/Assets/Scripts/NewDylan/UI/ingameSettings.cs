using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingameSettings : MonoBehaviour
{
    [SerializeField] private inGameAudio audioScript;
    [SerializeField] private Translate translateScript;
    [SerializeField] private EndScreen endScript;
    [SerializeField] private GameObject instructions;
    
    public void setSettings(bool soundOn,bool musicOn,int langues,bool tips)
    {
        //still needs to set the audio
        translateScript.setLangues(langues);
        endScript.setLangues(langues);
        audioScript.setStart(soundOn,musicOn);
        instructions.SetActive(tips);
    }
}
