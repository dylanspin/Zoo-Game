using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyPopUp : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI priceText;
    [SerializeField] private TMPro.TextMeshProUGUI animalText;
    [SerializeField] private GameObject[] typeUnlock;
    [SerializeField] private TMPro.TMP_InputField inputIp;

    public void showPopUp(AnimalData animal,int langues)
    {
        gameObject.SetActive(true);
        typeUnlock[0].SetActive(!animal.codeunlock);
        typeUnlock[1].SetActive(animal.codeunlock);
        animalText.text = animal.animalName[langues];
        priceText.text = animal.price.ToString();
    }

    public void closePopUp()
    {
        gameObject.SetActive(false);
    }

    public void cantBuy()
    {
        
    }

    public string getCode()
    {
        string codeInput = inputIp.text;
        codeInput = codeInput.Replace(" ", string.Empty);

        return codeInput;
    }
}
