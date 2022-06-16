using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [Header("Set Data")]
    [SerializeField] private Setting[] settings;
    [SerializeField] private SoundController soundScript;
    [SerializeField] private languesController languesScript;
    [SerializeField] private ingameSettings gameScript;

    [Header("Private Data")]
    private bool soundOn = true;
    private bool musicOn = true;
    private bool tips = true;
    private int langues = 1;//0 = english 1 = dutch

    private void Start()
    {
        loadData();
    }

    private void loadData()
    {
        SettingData loadData = SaveScript.loadSettings();
        if(loadData != null)
        {
            soundOn = loadData.soundOn;
            musicOn = loadData.musicOn;
            langues = loadData.langues;
            tips = loadData.tips;
        }
        else
        {
            soundOn = true;
            musicOn = true;
            tips = true;
            langues = 1;
        }

        if(gameScript)
        {
            gameScript.setSettings(soundOn,musicOn,langues,tips);
        }
        else
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
        languesScript.loadLang(langues);
        setSettings();
    }
    
    private void setSettings()
    {
        if(soundScript)
        {
            soundScript.setMusic(musicOn);
            soundScript.setSound(soundOn);
        }
    }

    public void saveLang(int newLang)
    {
        langues = newLang;
        SaveScript.saveSettings(this);//saves book data
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
        soundScript.playSoundEffect(1);
        SaveScript.saveSettings(this);//saves book data
    }

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
