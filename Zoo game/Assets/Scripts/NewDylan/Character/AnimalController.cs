using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public  AnimalData testingAnimal;

    [Header("Set data")]
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject bodySpawnPos;

    [Header("Scripts")]
    [SerializeField] private CamShake shakeScript;
    [SerializeField] private animalCol colScript;
    [SerializeField] private AnimalMovement moveScript;
    [SerializeField] private Abilities abilitieScript;

    // [Header("Private Script")]
    // private game

    void Start()
    {
        setStartData(testingAnimal);
    }

    public void setStartData(AnimalData newAnimal)
    {
        cameraObj.transform.parent = null;
        transform.parent = null;

        AnimalPrefab prefabScript = Instantiate(newAnimal.AnimalObject, bodySpawnPos.transform.position, Quaternion.Euler(0,0,0),transform).GetComponent<AnimalPrefab>(); 

        abilitieScript.setStartData(prefabScript.animalBody,newAnimal);
        moveScript.setStartData(prefabScript.groundCheck,newAnimal);
        colScript.setStartData(newAnimal);
    }

}
