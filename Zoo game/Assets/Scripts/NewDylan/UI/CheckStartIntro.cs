using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckStartIntro : MonoBehaviour
{
    private void Start()
    {
        Values.lastPage = 1;
    //    check();
    }

    private void check()
    {
        SettingData loadData = SaveScript.loadSettings();
        if(loadData != null)
        {
            Values.lastPage = 1;
            SceneManager.LoadScene(2);
        }
    }
}
