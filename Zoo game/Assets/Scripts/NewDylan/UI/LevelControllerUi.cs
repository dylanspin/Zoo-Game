using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControllerUi : MonoBehaviour
{
    [SerializeField] private BookController bookScript;

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
        //needs checks on animalId
        // playAnimal.unlockId
        SceneManager.LoadScene(3);//loads level //needs to be loading screen scene
    }
}
