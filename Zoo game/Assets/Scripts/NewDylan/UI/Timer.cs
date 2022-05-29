using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Controller controllerScript;

    [Header("text objects")]
    [SerializeField] private TMPro.TextMeshProUGUI count;
   
    [Header("Settings")]
    [SerializeField] private int timeSpeed = 1;
    [SerializeField] private float minutes = 0;
    [SerializeField] private float seconds = 0;

    [Header("Private data")]
    private bool isOn = true;

    public void Update()
    {
        countDown();
    }

    private void countDown()
    {
        if(isOn)
        {
            if(seconds > 0)
            {
                seconds -= timeSpeed * Time.deltaTime;
            }
            else
            {
                if(minutes > 0)
                {
                    minutes --;
                    seconds = 59;
                }
                else
                {
                    endTime();
                }
            }

            if(seconds > 9)
            {
                count.text = minutes.ToString() + ":" + seconds.ToString("F0");
            }
            else
            {
                count.text = minutes.ToString() + ":0" + seconds.ToString("F0");
            }
        }
    }

    private void endTime()
    {
        isOn = false;
        controllerScript.endMatch();
        Debug.Log("End timer");
    }
}
