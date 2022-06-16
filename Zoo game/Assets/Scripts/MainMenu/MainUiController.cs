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
    private int activePage = 0;
    
    private void Start()
    {
        Values.pauzed = false;
        anim.SetInteger("Page",Values.lastPage);
        Time.timeScale = 1;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(activePage != 0)
            {
                openPage(0);
            }
        }
    }

    public void openPage(int open)//later we can add animations
    {
        soundScript.playSoundEffect(0);
        anim.SetInteger("Page",open);
        activePage = open;
        Values.lastPage = open;
        bookScript.closeAll();
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
