using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    [SerializeField] private int nextLevel = 0;
    public float startTime = 2.5f;
    public float transitionTime = 1f;

    private void Start()
    {
        if(startTime != 0)
        {
            Invoke("LoadNextLevel",startTime);
        }
    }

    public void LoadNextLevel ()
    {
        StartCoroutine (LoadLevel(nextLevel));
    }

    IEnumerator LoadLevel (int LevelIndex)
    {
        transition.SetBool("Start",true);
        yield return new WaitForSeconds (transitionTime);
        SceneManager.LoadScene(LevelIndex);
    }
}

