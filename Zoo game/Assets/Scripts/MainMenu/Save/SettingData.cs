using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingData
{
    public bool soundOn = true;
    public bool musicOn = true;

    public SettingData(SettingsController oData)
    { 
        soundOn = oData.getSound();
        musicOn = oData.getMusic();
    }
}
