using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [Header("Set Data")]
    [SerializeField] private Setting[] settings;

    [Header("Private Data")]
    private bool soundOn = true;
    private bool musicOn = true;

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
        }
        else
        {
            soundOn = true;
            musicOn = true;
        }

        loadSettings();
    }
    
    private void loadSettings()
    {
        settings[0].startData(this,soundOn);//sound
        settings[1].startData(this,musicOn);//music
    }

    public void setSetting(int setting,bool state)
    {
        if(setting == 0)
        {
            soundOn = state;
        }
        else if(setting == 1)
        {
            musicOn = state;
        }

        SaveScript.saveSettings(this);//saves book data
    }

    public bool getMusic()
    {
        return musicOn;
    }

    public bool getSound()
    {
        return soundOn;
    }
}
