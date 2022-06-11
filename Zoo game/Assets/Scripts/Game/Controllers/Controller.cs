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

    public void endMatch()
    {
        int currentAmount = collectScript.getMoney();
        int maxAmount = collectScript.getmax();
        if(currentAmount > maxAmount)
        {
            currentAmount = maxAmount;
        }
        int part = (int)Mathf.Floor((float)currentAmount / (maxAmount / 2));

        bookScript.saveMoney(currentAmount);
        uiScript.showEndScreen(part,currentAmount,trackScript.getDistance());//needs distance
    }
}
