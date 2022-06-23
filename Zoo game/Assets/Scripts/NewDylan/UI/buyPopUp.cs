using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyPopUp : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI priceText;//the price text
    [SerializeField] private TMPro.TextMeshProUGUI animalText;//the  top information text
    [SerializeField] private Animator anim;//animator for the price tag used for the shake for example
    [SerializeField] private GameObject[] typeUnlock;//if code or price unlock
    [SerializeField] private TMPro.TMP_InputField inputIp;//the code input field

    public void showPopUp(AnimalData animal,int langues)//called from the book controller opens the pop up
    {
        gameObject.SetActive(true);//turns on this pop up what starts of with transition animation
        typeUnlock[0].SetActive(!animal.codeunlock);//shows the normal price unlock
        typeUnlock[1].SetActive(animal.codeunlock);//shows the code unlock
        animalText.text = "Unknown";//english
        if(langues == 1)//dutch
        {
            animalText.text = "Niet bekend";
        }
        priceText.text = animal.price.ToString();//sets the price 
    }

    public void closePopUp()//is closed when the button is pressed
    {
        gameObject.SetActive(false);
    }

    public void cantBuy()//when the animal cant be unlocked shake this pop up
    {
        anim.Play("shakeBuy",0, 0.25f);
    }

    public string getCode()//gets the code from the input is used in the book controller
    {
        string codeInput = inputIp.text;
        codeInput = codeInput.Replace(" ", string.Empty);

        return codeInput;
    }
}
