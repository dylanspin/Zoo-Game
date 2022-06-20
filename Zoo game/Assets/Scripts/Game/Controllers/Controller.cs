using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private UiController uiScript;
    [SerializeField] private HealthBar healthScript;
    [SerializeField] private AbilityBar abilityScript;
    [SerializeField] private CollectController collectScript;
    [SerializeField] private BookController bookScript;
    [SerializeField] private TrackMovement trackScript;
    [SerializeField] private SlowMoEnd endEffectScript;

    public void setAnimal(AnimalData newData,Abilities abilScript)//start called from the animal
    {
        healthScript.setStart(newData.health);
        abilityScript.setStart(newData,abilScript);
    }

    public void collided(float knockbackTime,bool dead)
    {
        trackScript.stopTrack(true);
        if(dead)
        {
            uiScript.endTrack();
            // endEffectScript.turnOn();
            // StartCoroutine (endGame(knockbackTime));
            Invoke("endMatch",knockbackTime);
        }
        else
        {
            Invoke("unlock",knockbackTime);
        }
    }

    private void unlock()
    {
        trackScript.stopTrack(false);
    }

    public void setHealth(int health)
    {
        healthScript.sethealthBar(health);
    }

    public void endMatch()
    {
        int currentAmount = collectScript.getMoney();
        int maxAmount = 500;
        int part = (int)Mathf.Floor((float)currentAmount / (maxAmount / 2));
        int distance = (int)trackScript.getDistance(); 
        bool newhigh = bookScript.checkHighScore(distance);

        bookScript.saveEndData(currentAmount,distance);
        uiScript.showEndScreen(part,currentAmount,distance,newhigh);
    }

    public void saveData()
    {
        int currentAmount = collectScript.getMoney();
        int distance = (int)trackScript.getDistance(); 
        bookScript.saveEndData(currentAmount,distance);
    }

    public AbilityBar getBarScript()
    {
        return  abilityScript;
    }
}



