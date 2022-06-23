using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    [Header("set data")] 
    [SerializeField] private Animator anim;//button animator
    [SerializeField] private int optionId;//the id of the option

    [Header("Private data")] 
    private SettingsController settingScript;//the main controller of the settings
    private bool state;//current on or off state

    void OnEnable()//when enabled again set animator
    {
        anim.SetBool("On",state);
    }

    public void startData(SettingsController newScript,bool active)//when scene is loaded set saved data
    {
        settingScript = newScript;
        state = active;
        anim.SetBool("On",active);
    }

    public void setOption(bool active)//called via the buttons 
    {
        if(state == active)//if the same button thats on is clicked again it turns off
        {
            state = !state;
        }
        else
        {
            state = active;
        }
        settingScript.setSetting(optionId,state);//saves the setting
        anim.SetBool("On",state);//sets the anim of the buttons
    }   
}
