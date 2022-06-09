using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyPopUp : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI priceText;
    [SerializeField] private TMPro.TextMeshProUGUI animalText;

    public void showPopUp(AnimalData animal)
    {
        gameObject.SetActive(true);
        animalText.text = animal.animalName;
        priceText.text = animal.price.ToString();
    }

    public void closePopUp()
    {
        gameObject.SetActive(false);
    }

    public void cantBuy()
    {
        
    }
}
