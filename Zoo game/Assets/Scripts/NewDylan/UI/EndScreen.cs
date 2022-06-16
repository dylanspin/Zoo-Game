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
    [SerializeField] private string[] normalScore;
    [SerializeField] private string[] highScore;

    [Header("Private")]
    private int langues = 0;

    public void setLangues(int newLang)
    {
        langues = newLang;
    }

    public void showEndScreen(int rating,int current,float distance,bool newHigh)
    {
        this.gameObject.SetActive(true);
        topText.GetComponent<Animator>().SetBool("NewHigh",newHigh);//shows scaling animation when new high score
        if(newHigh)
        {
            topText.text = highScore[langues];
        }
        else
        {
            topText.text = normalScore[langues];
        }
        count.text = current.ToString();
        distanceText.text = distance.ToString("F0");
        if(rating >= bags.Length)
        {
            rating = bags.Length-1;
        }   
        bagImage.sprite = bags[rating];
    }
    
}
