using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private UiController uiScript;
    [SerializeField] private CollectController collectScript;
    [SerializeField] private BookController bookScript;

    public void endMatch()
    {
        bookScript.saveMoney(collectScript.getMoney());
    }
}
