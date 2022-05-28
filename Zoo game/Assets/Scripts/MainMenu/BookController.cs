using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour
{   
    [Header("Animal UI")]
    [SerializeField] private AnimalSlot[] uiSlots;//animal ui slots in the book
    [SerializeField] private Sprite[] animalSprites;//the correct images for the animals in the book

    [SerializeField] private AnimalSlot rightAnimalSlot;//the right animal picture 
    [SerializeField] private TMPro.TextMeshProUGUI[] animalInfo;//the text related to the selected animal

    [Header("Animal Data")]/////////////wil be made in to scriptable objects so it doesnt have to be like this here
    [SerializeField] private int[] prices = new int[50];
    [SerializeField] private string[] animalName = new string[50];
    [SerializeField] private string[] info1 = new string[50];
    [SerializeField] private string[] info2 = new string[50];
    [SerializeField] private string[] randomFact = new string[50];

    [Header("Other UI Data")]
    [SerializeField] private TMPro.TextMeshProUGUI moneyAmount;

    [Header("Private Data")]
    private bool[] unlocked = new bool[50];
    private int money = 0;
    private int lastSelected = 0;


    private void Start()
    {
        loadData();
    }

    private void loadData()
    {
        BookData loadData = SaveScript.loadBook();
        if(loadData != null)
        {
            //username = loadData.username; ///example
        
        }else{
            Debug.Log("No data found");
        }

        setData();
    }

    private void setData()
    {
        setSlots();
        moneyAmount.text = money.ToString();
    }

    private void setSlots()//later needs page offset
    {
        for(int i=0; i<uiSlots.Length; i++)
        {
            uiSlots[i].setData(i,animalSprites[i],unlocked[i],true,this);
        }
    }   

    public void buyAnimal(int slot)
    {
        if(money > prices[slot] && !unlocked[slot])
        {
            money -= prices[slot];
            unlocked[slot] = true;
            SaveScript.saveBook(this);//saves book data

            //do some ui Stuff indicating that animal is unlocked
        }
        else
        {
            //show message or something else indicating not enough money
        }
    }   

    public void hoverSet(int slotId,bool active)
    {
        int showId = slotId;
        if(!active)
        {
            showId = lastSelected;
        }
        
        uiSlots[showId].setData(showId,animalSprites[showId],unlocked[showId],true,this);

        Debug.Log(showId);
        /////needs to be later from scriptable animal
        if(unlocked[showId])
        {
            animalInfo[0].text = animalName[showId];
            animalInfo[1].text = info1[showId];
            animalInfo[2].text = info2[showId];
            animalInfo[3].text = randomFact[showId];
        }
        else
        {
            for(int i=0; i<animalInfo.Length; i++)
            {
                animalInfo[i].text = "Unknown";
            }
        }
    }

    public void selectAnimal(int slotId)
    {
        lastSelected = slotId;
        if(unlocked[slotId])
        {

        }
    }


}
