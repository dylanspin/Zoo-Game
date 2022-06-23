using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingameSettings : MonoBehaviour
{
    [SerializeField] private inGameAudio audioScript;//this controlls the audio in the game 
    [SerializeField] private Translate translateScript;//this translates text
    [SerializeField] private EndScreen endScript;//end screen script
    [SerializeField] private GameObject instructions;//instructions object
    
    public void setSettings(bool soundOn,bool musicOn,int langues,bool tips)
    {
        translateScript.setLangues(langues);//translates all other text in the game 
        endScript.setLangues(langues);//sets the langues for the end screen 
        audioScript.setStart(soundOn,musicOn);//sets the audio options
        instructions.SetActive(tips);//turns on or off the instructions at the start
    }
}
