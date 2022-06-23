using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unlockedAnimal : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI nameAnimal;//the animal name 
    [SerializeField] private TMPro.TextMeshProUGUI infoText;//the info section of the page
    [SerializeField] private Image animalImg;//the image of the page
    private AnimalData loadedAnimal;//the current display animal
    private int languas = 0;//the selected langues

    public void setLang(int newLang)//sets the langues
    {
        languas = newLang;
    }

    public void showUnlocked(AnimalData newAnimal,int langues)//called from the book controller or when the info button is clicked
    {
        gameObject.SetActive(true);//turns on the pop up
        loadedAnimal = newAnimal;//sets the loaded animal data
        nameAnimal.text = loadedAnimal.animalName[languas];//sets the name of the animal
        loadPage(0);//loads the first page
    }
    
    public void loadPage(int newPage)//when one of the 3 page buttons is pressed
    {
        if(languas == 0)//enlish
        {
            infoText.text = loadedAnimal.unlockPagesEng[newPage];
        }
        else//dutch
        {
            infoText.text = loadedAnimal.unlockPagesDutch[newPage];
        }
        animalImg.sprite = loadedAnimal.pageImages[newPage];//sets the new page picture
    }

    public void closeWindow()//closes the pop up
    {
        this.gameObject.SetActive(false);
    }

    public void openLink()
    {
        Application.OpenURL("https://www.wildlands.nl/");//opens the wildlands website when the button is pressed
    }
}
