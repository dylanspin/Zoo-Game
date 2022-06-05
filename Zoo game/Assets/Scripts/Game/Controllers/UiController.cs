using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{   
    [Header("Set data")]
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private EndScreen endScript;
    [SerializeField] private GameObject gameInfo;//timer and counter
    [SerializeField] private TMPro.TextMeshProUGUI count;

    [Header("Private data")]
    private bool gameEnded = false;

    private void Start()
    {
        cursorLock(true);
        Values.pauzed = false;
        Time.timeScale = 1;
    }
  
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            escape(!Values.pauzed);
        }
    }

    public void escape(bool active)
    {
        escapeMenu.SetActive(active);
        if(!gameEnded)
        {
            if(active)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            cursorLock(!active);
        }
        Values.pauzed = active;
    }

    private void cursorLock(bool active)//hides cursor
    {
        Screen.lockCursor = active; 
        // camScript.lockLook(!active);
    }

    public void setCollected(int amount)
    {
        string addAmount = "000";
        if(amount > 100)
        {
            addAmount = "";
        }
        else if(amount > 10)
        {
            addAmount = "0";
        }
        else if(amount > 1)
        {
            addAmount = "00";
        }

        count.text = addAmount + amount;
    }

    public void showEndScreen(int rating,int current,int max)
    {
        Values.pauzed = true;
        Time.timeScale = 0;
        cursorLock(false);
        endScript.showEndScreen(rating,current,max);
        gameInfo.SetActive(false);//turns off timer and counter
    }

    public void returnMain()
    {
        SceneManager.LoadScene("Dylan UI Testing");
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
