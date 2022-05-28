using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUiController : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    private int activePage = 0;
    
    private void Start()
    {
        //setting ui stuff maybe
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
    }

    public void openPage(int open)//later we can add animations
    {
        pages[activePage].SetActive(false);
        pages[open].SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
