using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControllerUi : MonoBehaviour
{   
    [Header("Scripts")]
    [SerializeField] private BookController bookScript;
    
    [Header("Loading")]
    [SerializeField] private Animator anim;
    [SerializeField] private Slider slider; 
    [SerializeField] private TMPro.TextMeshProUGUI loadingText;

    [Header("Private data")]
    private AnimalData loadAnimal;
    private int levelIndex = 1;

    public void playAnimal()
    {
        AnimalData lastAnimal = bookScript.getCurrent();
        if(lastAnimal != null)
        {
            Values.selectedAnimal = lastAnimal;
            checkLevel(Values.selectedAnimal);
        }
        else
        {
            //show error of not unlocked animal
        }
    }   

    private void checkLevel(AnimalData playAnimal)
    {
        loadAnimal = playAnimal;

        anim.SetBool("Load",true);
        Invoke("startLoad",1.5f);//so it actually shows the loading screen instead of instantly loading 
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
            float progress = Mathf.Clamp01(operation.progress / .9f); 
            
            slider.value = progress; 
            loadingText.text = "Loading: " + progress * 100f + "%"; 

            yield return null; 
        } 
    } 
}
