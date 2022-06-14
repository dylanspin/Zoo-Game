using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class languesController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Translate translateScript;
    [SerializeField] private SettingsController settingScript;

    [Header("Set Data")]
    [SerializeField] private Animator anim;
    private int activeLang = 0;

    public void setLang(int newLang)//by buttons
    {
        loadLang(newLang);
        settingScript.saveLang(newLang);
    }

    public void loadLang(int newLang)
    {
        activeLang = newLang;
        anim.SetBool("On",activeLang == 0);
        translateScript.setLangues(activeLang);
    }
}
