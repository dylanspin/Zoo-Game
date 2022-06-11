using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [Header("Set Info")]
    [SerializeField] private TMPro.TextMeshProUGUI topText;
    [SerializeField] private TMPro.TextMeshProUGUI count;
    [SerializeField] private TMPro.TextMeshProUGUI distanceText;
    [SerializeField] private Image bagImage;
   
    [Header("Settings")]
    [SerializeField] private Sprite[] bags;
    [SerializeField] private string[] ratingText;

    public void showEndScreen(int rating,int current,float distance)
    {
        this.gameObject.SetActive(true);
        bagImage.sprite = bags[rating];
        topText.text = ratingText[rating];
        // count.text = current+"/"+max;
        count.text = current.ToString();
        distanceText.text = distance.ToString("F0");
    }
    
}
