using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unlockedAnimal : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI nameAnimal;
    [SerializeField] private Image animalImg;

    public void showUnlocked(AnimalData newAnimal)
    {
        nameAnimal.text = newAnimal.animalName;
        animalImg.sprite = newAnimal.animalImage;
        gameObject.SetActive(true);
    }

    public void endAnim()
    {
        this.gameObject.SetActive(false);
    }
}
