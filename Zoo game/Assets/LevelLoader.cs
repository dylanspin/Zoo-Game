<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel ()
    {
        //SceneManager.LoadScene(4);
        StartCoroutine (LoadLevel(SceneManager.GetActiveScene(). buildIndex +1 ));
    }

    IEnumerator LoadLevel (int LevelIndex)
    {
        transition.SetTrigger ("Start");
        yield return new WaitForSeconds (transitionTime);
        SceneManager.LoadScene (LevelIndex);

    }
}
=======
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class LevelLoader : MonoBehaviour
{
   
   public GameObject loadingScreen;
   public Slider slider;
   public Text progressText;

   
   public void LoadLevel (int sceneIndex)
   {
       StartCoroutine(LoadAsynchronously(sceneIndex));
        
   }

   IEnumerator LoadAsynchronously (int sceneIndex)
   {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            
            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
   }
}
>>>>>>> Celeste
