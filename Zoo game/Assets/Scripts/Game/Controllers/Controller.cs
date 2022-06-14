using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private UiController uiScript;
    [SerializeField] private CollectController collectScript;
    [SerializeField] private BookController bookScript;
    [SerializeField] private TrackMovement trackScript;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            endMatch();
        }
    }

    public void collided(float knockbackTime)
    {
        trackScript.stopTrack(true);
        uiScript.endTrack();
        Invoke("endMatch",knockbackTime);
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

}



