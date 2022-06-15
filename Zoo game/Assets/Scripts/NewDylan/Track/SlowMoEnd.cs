using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoEnd : MonoBehaviour
{   
    [Header("Settings")]
    [SerializeField] private float speed = 1;

    [Header("Private data")]
    private float currentTime = 1;
    private bool on = false;

    public void Update()
    {
        if(on)
        {
            currentTime = Mathf.Lerp(currentTime,0,speed * Time.unscaledDeltaTime);
            Time.timeScale = currentTime;
            Time.fixedDeltaTime = 0.02f;
        }
    }

    public void turnOn()
    {
        on = true;
    }
}
