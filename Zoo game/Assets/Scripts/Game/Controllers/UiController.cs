using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{   
    [Header("Set data")]
    [SerializeField] private GameObject escapeMenu;//escape menu object
    [SerializeField] private EndScreen endScript;//end screen script
    [SerializeField] private Controller controllerScript;//main game controller script
    [SerializeField] private GameObject gameInfo;//timer and counter
    [SerializeField] private TMPro.TextMeshProUGUI count;//the counter for rubbies collected
    [SerializeField] private TMPro.TextMeshProUGUI distanceText;//the counter for distance moved

    [Header("Private data")]
    private bool gameEnded = false;

    private void Start()
    {
        Values.pauzed = false;
        Time.timeScale = 1;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }
  
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            escape(!Values.pauzed);
        }
    }

    public void escape(bool active)//opens escape menu
    {
        if(!gameEnded)//if the endscreen isnt active
        {
            escapeMenu.SetActive(active);//activates the endscreen
            if(active)
            {
                Time.timeScale = 0;//pauzed the game speed
            }
            else
            {
                Time.timeScale = 1;//sets the game speed back to normal
            }
            Values.pauzed = active;
        }
    }

    public void setCollected(int amount,int maxAmount)
    {
        string addAmount = "000";
        if(amount > 100)//for displaying the numbers in a specivic order
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

    public void setDistance(float distance)//sets the distance moved
    {
        distanceText.text = "M"+distance.ToString("F0");
    }

    public void endTrack()//if the game is ended 
    {
        gameEnded = true;
    }

    public void showEndScreen(int rating,int current,float distance,bool newHigh)//show the end screen
    {
        Values.pauzed = true;
        Time.timeScale = 0;
        endScript.showEndScreen(rating,current,distance,newHigh);
        gameInfo.SetActive(false);//turns off timer and counter
    }

    public void returnMain()//goes back to main menu
    {
        controllerScript.saveData();//saves the score and collect rubbies
        SceneManager.LoadScene(2);//loads the main menu scene
    }

    public void restartLevel()//restarts the level
    {
        controllerScript.saveData();//saves the score and collect rubbies
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//reloads the current scene
    }

    public void quitGame()
    {
        Application.Quit();//quits the game
    }

}
