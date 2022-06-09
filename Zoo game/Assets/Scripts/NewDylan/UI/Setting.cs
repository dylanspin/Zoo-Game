using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    [Header("set data")] 
    [SerializeField] private Animator anim;
    [SerializeField] private int optionId;

    [Header("Private data")] 
    private SettingsController settingScript;
    private bool state;

    void OnEnable()
    {
        anim.SetBool("On",state);
    }

    public void startData(SettingsController newScript,bool active)
    {
        settingScript = newScript;
        state = active;
        anim.SetBool("On",active);
    }

    public void setOption(bool active)
    {
        if(state == active)
        {
            state = !state;
        }
        else
        {
            state = active;
        }
        settingScript.setSetting(optionId,state);
        anim.SetBool("On",state);
    }   
}
