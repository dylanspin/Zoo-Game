using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUiController : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    [SerializeField] private Animator anim;
    [SerializeField] private BookController bookScript;
    [SerializeField] private SoundController soundScript;
    
    private void Start()
    {
        Values.pauzed = false;
        Time.timeScale = 1;//sets the game speed to normal again
        anim.SetInteger("Page",Values.lastPage);//sets the start page 
        //limits the fps count 
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Values.lastPage != 0)
            {
                openPage(0);//goes back to the main page
            }
        }
    }

    public void openPage(int open)//later we can add animations
    {
        soundScript.playSoundEffect(0);
        anim.SetInteger("Page",open);//sets the animator page
        Values.lastPage = open;
        bookScript.closeAll();
    }

    public void quitGame()//quits the game when the quit button is pressed
    {
        Application.Quit();
    }

}
