using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [Header("Set Data")]
    [SerializeField] private Setting[] settings;//all the individual settings
    [SerializeField] private SoundController soundScript;//this script controls the sounds in the main menu
    [SerializeField] private languesController languesScript;//this script controlls the langues in the main menu
    [SerializeField] private ingameSettings gameScript;//this script controlls the settings in the track scene

    [Header("Private Data")]
    private bool soundOn = true;
    private bool musicOn = true;
    private bool tips = true;//if it should show the intructions at the start of the match
    private int langues = 1;//0 = english 1 = dutch

    private void Start()
    {
        loadData();
    }

    private void loadData()
    {
        SettingData loadData = SaveScript.loadSettings();
        if(loadData != null)//if there was data found from the save file
        {
            soundOn = loadData.soundOn;
            musicOn = loadData.musicOn;
            langues = loadData.langues;
            tips = loadData.tips;
        }
        else//if there is not nothing found set the values to these default settings
        {
            soundOn = true;
            musicOn = true;
            tips = true;
            langues = 1;
        }

        if(gameScript)///if in game set the settings
        {
            gameScript.setSettings(soundOn,musicOn,langues,tips);
        }
        else//if in main meny display setting data in UI
        {
            loadSettings();
        }
    }
    
    private void loadSettings()
    {
        if(settings.Length > 0)
        {
            settings[0].startData(this,soundOn);//sound
            settings[1].startData(this,musicOn);//music
            settings[2].startData(this,tips);//tips in game
        }
        languesScript.loadLang(langues);//sets the set langues 
        setSettings();
    }
    
    private void setSettings()
    {
        if(soundScript)//sets the audio options
        {
            soundScript.setMusic(musicOn);
            soundScript.setSound(soundOn);
        }
    }

    public void saveLang(int newLang)//for when new langues is selected save that
    {
        langues = newLang;
        SaveScript.saveSettings(this);//saves setting data
    }

    public void setSetting(int setting,bool state)
    {
        if(setting == 0)
        {
            soundOn = state;
            soundScript.setSound(soundOn);
        }
        else if(setting == 1)
        {
            musicOn = state;
            soundScript.setMusic(musicOn);
        }
        else if(setting == 2)
        {
            tips = state;
        }
        soundScript.playSoundEffect(1);//play button sound effect
        SaveScript.saveSettings(this);//saves setting data
    }

    ///get functions for getting data needed for saving it 
    public bool getTips()
    {
        return tips;   
    }

    public bool getMusic()
    {
        return musicOn;
    }

    public bool getSound()
    {
        return soundOn;
    }

    public int getLang()
    {
        return langues;
    }
}
