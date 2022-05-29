using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStats : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float staminaGain = 10;
    [SerializeField] private float gainDelay = 0.5f;

    [Header("Privat values")]
    private float stamina = 100;
    private bool gaining = false;

    private void Start()
    {
        stamina = maxStamina;
    }

    void Update()
    {
        Gain();
    }

    private void Gain()
    {
        if(gaining)
        {
            if(stamina + staminaGain < maxStamina)
            {
                stamina += staminaGain;
            }
            else
            {
                stamina = maxStamina;
                //show something maybe
            }
        }
    }

    public void lose(float amount)
    {
        gaining = false;
        CancelInvoke("setGain");
        Invoke("setGain",gainDelay);
        if(stamina - amount > 0)
        {
            stamina -= amount;
        }
        else
        {
            stamina = 0;
            //show something and do something when stamina ran out
        }
    }

    private void setGain()
    {
        gaining = true;
    }

    public float getStam()
    {
        return stamina;
    }
}
