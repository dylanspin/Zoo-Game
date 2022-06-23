using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [Header("Set Info")]
    [SerializeField] private TMPro.TextMeshProUGUI topText;//the top text of the pop up
    [SerializeField] private TMPro.TextMeshProUGUI count;//text for displaying the amount of rubbies collected
    [SerializeField] private TMPro.TextMeshProUGUI distanceText;//text for displaying the distance moved
    [SerializeField] private Image bagImage;//image for the bag sprite
   
    [Header("Settings")]
    [SerializeField] private Sprite[] bags;//different sprites for the rating of collected rubbies
    [SerializeField] private string[] normalScore;//translate text
    [SerializeField] private string[] highScore;//translate text

    [Header("Private")]
    private int langues = 0;

    public void setLangues(int newLang)//sets the langues
    {
        langues = newLang;
    }

    public void showEndScreen(int rating,int current,float distance,bool newHigh)
    {
        this.gameObject.SetActive(true);//activates this pop up
        topText.GetComponent<Animator>().SetBool("NewHigh",newHigh);//shows scaling animation when new high score
        if(newHigh)//if the player got a new high score
        {
            topText.text = highScore[langues];
        }
        else
        {
            topText.text = normalScore[langues];
        }
        count.text = current.ToString();//shows the rubbies collected
        distanceText.text = distance.ToString("F0")+"M";//shows the distance moved
        if(rating >= bags.Length)//double checks the rating
        {
            rating = bags.Length-1;
        }   
        bagImage.sprite = bags[rating];//shows the rating of collected rubbies 
    }
    
}
