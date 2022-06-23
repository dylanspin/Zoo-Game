using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSlot : MonoBehaviour
{
    [Header("Set data")]
    [SerializeField] private Image animalImage;//the image of the animal
    [SerializeField] private GameObject[] notUnlocked;//types of indicators the animal is not unlocke either a ? or a wildlands logo
    [SerializeField] private TMPro.TextMeshProUGUI animalName;//text for the animals name

    [Header("Private data")]
    private int slotId;//current id of the slot
    private BookController controllerScript;//the main book controller

    public void setData(int setId,AnimalData animalInfo,bool unlocked,bool active,BookController newController,int langues)//gets called from book controller script
    {
        slotId = setId;
        animalImage.sprite = animalInfo.animalImage;//sets the image to the animals sprite
        gameObject.SetActive(active);//for when there is no animal for that slot
        animalImage.gameObject.SetActive(unlocked);
        notUnlocked[0].SetActive(!unlocked && !animalInfo.codeunlock);//shows ? if not unlocked
        notUnlocked[1].SetActive(!unlocked && animalInfo.codeunlock);//shows wildlands logo when not unlocked
        controllerScript = newController;
        if(unlocked)
        {
            animalName.text = animalInfo.animalName[langues]; //sets the animals name 
        }
        else
        {
            animalName.text = "?"; 
        }
    }

    public void hoverAnimal(bool active)//when hovering 
    {
        controllerScript.hoverSet(slotId,active);
    }

    public void setAnimal()//when clicked
    {
        controllerScript.selectAnimal(slotId);//selects animal
    }
}
