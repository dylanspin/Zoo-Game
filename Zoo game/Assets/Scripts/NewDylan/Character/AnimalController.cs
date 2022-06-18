using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    [Header("Testing")]
    [SerializeField] private AnimalData testAnimal;

    [Header("Set data")]
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject bodySpawnPos;

    [Header("Scripts")]
    [SerializeField] private Controller controllerScript;
    [SerializeField] private TrackController trackScript;
    [SerializeField] private camFollow followScript;
    [SerializeField] private animalCol colScript;
    [SerializeField] private AnimalMovement moveScript;
    [SerializeField] private Abilities abilitieScript;

    void Start()
    {
        if(Values.selectedAnimal != null)
        {
            setStartData(Values.selectedAnimal);
        }
        else
        {
            setStartData(testAnimal);
        }
    }

    public void setStartData(AnimalData newAnimal)
    {
      //  cameraObj.transform.parent = null;
        transform.parent = null;

        AnimalPrefab prefabScript = Instantiate(newAnimal.AnimalObject, bodySpawnPos.transform.position, Quaternion.Euler(0,0,0),transform).GetComponent<AnimalPrefab>(); 

        controllerScript.setAnimal(newAnimal,abilitieScript);
        abilitieScript.setStartData(prefabScript.animalBody,newAnimal,controllerScript.getBarScript());
        moveScript.setStartData(prefabScript.groundCheck,newAnimal,trackScript.getLanes(),prefabScript.movePs,prefabScript.anim);
        colScript.setStartData(newAnimal,prefabScript.collideEffect,controllerScript,prefabScript.animalBody);
        followScript.setStartData(newAnimal);
    }

}
