using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] CamLook camScript;


    private void Start()
    {
        
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
        if(active)
        {
            Screen.lockCursor = true; 
            camScript.lockLook(false);
        }else{
            Screen.lockCursor = false;
            camScript.lockLook(true);
        }
    }
}
