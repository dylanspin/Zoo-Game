using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private TMPro.TextMeshProUGUI count;

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
        if(active)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        cursorLock(!active);
        Values.pauzed = active;
    }

    private void cursorLock(bool active)//hides cursor
    {
        Screen.lockCursor = active; 
        // camScript.lockLook(!active);
    }

    public void setCollected(int amount)
    {
        count.text = amount + "X";
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
