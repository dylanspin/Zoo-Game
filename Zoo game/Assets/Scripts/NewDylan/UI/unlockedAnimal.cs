using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unlockedAnimal : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI nameAnimal;
    [SerializeField] private TMPro.TextMeshProUGUI infoText;
    [SerializeField] private Image animalImg;
    private AnimalData loadedAnimal;
    private int languas = 0;

    public void setLang(int newLang)
    {
        languas = newLang;
    }

    public void showUnlocked(AnimalData newAnimal,int langues)
    {
        gameObject.SetActive(true);
        loadedAnimal = newAnimal;
        nameAnimal.text = loadedAnimal.animalName[languas];
        loadPage(0);
    }
    
    public void loadPage(int newPage)
    {
        if(languas == 0)
        {
            infoText.text = loadedAnimal.unlockPagesEng[newPage];
        }
        else
        {
            infoText.text = loadedAnimal.unlockPagesDutch[newPage];
        }
        animalImg.sprite = loadedAnimal.pageImages[newPage];
    }

    public void closeWindow()
    {
        this.gameObject.SetActive(false);
    }

    public void openLink()
    {
        Application.OpenURL("https://www.wildlands.nl/");
    }
}
