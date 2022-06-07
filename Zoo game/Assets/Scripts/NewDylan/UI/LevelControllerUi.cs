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
    private float progress = 0;
    private bool loading = false;


    private void Update()
    {
        if(loading)
        {
            progress = Mathf.Lerp(progress,100,0.003f);
            slider.value = progress; 
            loadingText.text = "Loading: " + progress.ToString("F2") + "%"; 
        }
    }

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
        loading = true;
        loadAnimal = playAnimal;

        anim.SetBool("Load",true);
        Invoke("startLoad",3f);//1.5f
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
            // float progress2 = Mathf.Clamp01(operation.progress / .9f); 
            
            // slider.value = progress2; 
            // loadingText.text = "Loading: " + progress2 * 100f + "%"; 

            yield return null; 
        } 
    } 
}
