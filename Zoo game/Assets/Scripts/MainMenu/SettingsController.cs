using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    private void Start()
    {
        loadData();
    }

    private void loadData()
    {
        SettingData loadData = SaveScript.loadSettings();
        if(loadData != null)
        {
            //username = loadData.username; ///example
        
        }else{
            Debug.Log("No data found");
        }
    }
}
