using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    [Header("Set Data")]
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject abilityStatus;

    [Header("Private Data")]
    private Abilities abilityScript;//the animal ability script
    private float regainSpeed = 2;//the speed the bar fills again per second
    private float maxTime = 2;//the max length of the timer
    private float currentTime = 0;//current time
    private bool isOn = false;//if is draining

    public void setStart(AnimalData animal,Abilities abilScript)//called from controller script
    {
        abilityScript = abilScript;//animal ability script
        maxTime = animal.abilityTime;//length of the timer
        regainSpeed = animal.regainSpeed;//the speed the bar fills again per second
        currentTime = maxTime;
        slider.maxValue = maxTime;//sets the max value in the slider component
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
            else//when the bar is drained
            {
                currentTime = 0;
                isOn = false;
                stopAbility();//if the bar is drained stop the ability
            }
        }
        else
        {
            if(currentTime + regainSpeed * Time.deltaTime < maxTime)
            {
                currentTime += regainSpeed * Time.deltaTime;
            }
            else//when the bar is filled
            {
                currentTime = maxTime;
                setBarActive(false);
                barFilled();//calls functions related to when the bar is filled
            }
        }
        slider.value = currentTime;
    }

    public void showStatus(bool active)//shows the ability icon in this case its used for the baboon when in front of a rock
    {
        abilityStatus.SetActive(active);
    }

    private void barFilled()
    {
        abilityScript.filled();//calls functions related to when the bar is filled
    }

    private void stopAbility()
    {
        abilityScript.stopAbil();//stops abilities when the bar is drained
    }

    public bool canActivate()//returns bool if the bar is filled
    {
        return currentTime == maxTime;
    }
}
