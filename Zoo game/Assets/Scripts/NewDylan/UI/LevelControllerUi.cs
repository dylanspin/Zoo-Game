using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControllerUi : MonoBehaviour
{   
    [Header("Scripts")]
    [SerializeField] private BookController bookScript;//main book controller script
    
    [Header("Loading")]
    [SerializeField] private Animator anim;
    [SerializeField] private Slider slider; //fill up bar slider
    [SerializeField] private TMPro.TextMeshProUGUI loadingText;
    [SerializeField] private TMPro.TextMeshProUGUI infoText;//info text at the bottom of the loading screen

    [Header("Info")]
    [SerializeField] private string[] english;//english info in the loading screen
    [SerializeField] private string[] dutch;//dutch info in the loading screen

    [Header("Private data")]
    private AnimalData loadAnimal;//the selected animal
    private float progress = 0;//current loading progress
    private bool loading = false;//if fake loading 
    private int langues = 0;
    private void Update()
    {
        if(loading)//fills loading bar
        {
            progress = Mathf.Lerp(progress,100,0.0045f);
            slider.value = progress; 
            if(langues == 0)//enlish
            {
                loadingText.text = "Loading: " + progress.ToString("F2") + "%"; 
            }
            else//dutch
            {
                loadingText.text = "laden: " + progress.ToString("F2") + "%"; 
            }
        }
    }

    public void setLangues(int newLang)//sets the langues of the text
    {
        langues = newLang;
    }

    public void playAnimal()//called from button
    {
        AnimalData lastAnimal = bookScript.getCurrent();
        if(lastAnimal != null)//if animal is unlocked this is a double check 
        {
            Values.selectedAnimal = lastAnimal;
            checkLevel(Values.selectedAnimal);
        }
    }   

    private void checkLevel(AnimalData playAnimal)
    {
        loading = true;//sets fake loading
        loadAnimal = playAnimal;
        
        if(langues == 0)
        {
            infoText.text = english[Random.Range(0,english.Length)];
        }
        else
        {
            infoText.text = dutch[Random.Range(0,dutch.Length)];
        }

        anim.SetBool("Load",true);
        Invoke("startLoad",2f);//actually loads the level
    }

    private void startLoad()
    {
        StartCoroutine(LoadAsynchronously()); 
    }

    IEnumerator LoadAsynchronously () 
    { 
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadAnimal.playLevelIndex); 

        while (!operation.isDone) 
        { 
            //shows the actual loading but it loaded in less then a second and we wanted to make the transition from book to gameplay less instant
            // float progress2 = Mathf.Clamp01(operation.progress / .9f); 
            // slider.value = progress2; 
            // loadingText.text = "Loading: " + progress2 * 100f + "%"; 

            yield return null; 
        } 
    } 
}
