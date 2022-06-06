using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUiController : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    [SerializeField] private bool[] animSwitch;
    [SerializeField] private Animator anim;
    private int activePage = 0;
    
    private void Start()
    {
        Values.pauzed = false;
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
        if(!animSwitch[open])
        {
            pages[activePage].SetActive(false);
            pages[open].SetActive(true);
        }
        else
        {
            anim.SetInteger("Page",open);
        }
        activePage = open;
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
