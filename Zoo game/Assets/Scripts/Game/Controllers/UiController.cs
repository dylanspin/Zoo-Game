using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{

    private void Start()
    {
        cursorLock(true);
    }
  
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    public void escape(bool active)
    {

    }

    private void cursorLock(bool active)//hides cursor
    {
        Screen.lockCursor = active; 
        // camScript.lockLook(!active);
    }
}
