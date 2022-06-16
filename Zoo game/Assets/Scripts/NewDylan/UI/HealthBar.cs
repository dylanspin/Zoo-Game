using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Image fillImage;
    [SerializeField] private Slider slider;
    [SerializeField] private Color[] colorStages;

    public void setStart(int health)
    {
        slider.maxValue = health;
    }

    public void sethealthBar(int newHealth)
    {
        int setAmount = newHealth;
        if(newHealth < 0)
        {
            setAmount = 0;
        }
        slider.value = setAmount; 
        fillImage.color = colorStages[setAmount]; 
        anim.Play("loseHealth",0, 0.25f);
    }
}
