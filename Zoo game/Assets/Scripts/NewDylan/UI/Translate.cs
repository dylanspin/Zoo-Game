using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private BookController bookScript;
    [SerializeField] private LevelControllerUi levelScript;
    [SerializeField] private unlockedAnimal unlockScript;

    [Header("Set Data")]
    [SerializeField] private TMPro.TextMeshProUGUI[] translateText;
    [SerializeField] private string[] dutch;
    [SerializeField] private string[] english;

    public void setLangues(int newLangues)
    {
        if(bookScript)
        {
            bookScript.setLangues(newLangues);
            levelScript.setLangues(newLangues);
            unlockScript.setLang(newLangues);
        }

        // setLangues
        for(int i=0; i<translateText.Length; i++)
        {
            if(newLangues == 0)
            {
                translateText[i].text = english[i];
            }
            else
            {
                translateText[i].text = dutch[i];
            }
        }
    }

}
