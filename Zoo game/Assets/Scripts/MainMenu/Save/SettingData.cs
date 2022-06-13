using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingData
{
    public bool soundOn = true;
    public bool musicOn = true;
    public int langues = 0;//0 = english 1 = dutch

    public SettingData(SettingsController oData)
    { 
        soundOn = oData.getSound();
        musicOn = oData.getMusic();
        langues = oData.getLang();
    }
}
