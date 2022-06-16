using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    [Header("Set Data")]
    [SerializeField] private Slider slider;

    [Header("Private Data")]
    
    private Abilities abilityScript;
    private float regainSpeed = 2;
    private float maxTime = 2;
    private float currentTime = 0;
    private bool isOn = false;

    public void setStart(AnimalData animal,Abilities abilScript)//called from controller script
    {
        abilityScript = abilScript;
        maxTime = animal.abilityTime;
        regainSpeed = animal.regainSpeed;
        currentTime = maxTime;
        slider.maxValue = maxTime;
    }

    void Update()
    {
        setBar();
    }

    public void activate(bool active,bool setZero)//called when ability is activated
    {
        if(active)
        {
           setBarActive(true);
        }
        if(setZero)
        {
            currentTime = 0;
        }
        isOn = active;
    }

    private void setBarActive(bool active)//turns on or of the bar
    {
        slider.transform.parent.gameObject.SetActive(active);
    }

    private void setBar()
    {
        if(isOn)
        {
            if(currentTime - 1 * Time.deltaTime > 0)
            {
                currentTime -= 1 * Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                isOn = false;
                stopAbility();
            }
        }
        else
        {
            if(currentTime + regainSpeed * Time.deltaTime < maxTime)
            {
                currentTime += regainSpeed * Time.deltaTime;
            }
            else
            {
                currentTime = maxTime;
                setBarActive(false);
                barFilled();
            }
        }
        slider.value = currentTime;
    }

    private void barFilled()
    {
        abilityScript.filled();
    }

    private void stopAbility()
    {
        abilityScript.stopAbil();
    }

    public bool canActivate()
    {
        return currentTime == maxTime;
    }
}
