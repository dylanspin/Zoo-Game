using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour
{   
    [Header("Animal UI")]
    [SerializeField] private bool loadUi = false;
    [SerializeField] private AnimalSlot[] uiSlots;//animal ui slots in the book
    [SerializeField] private AnimalSlot rightAnimalSlot;//the right animal picture 
    [SerializeField] private TMPro.TextMeshProUGUI[] animalInfo;//the text related to the selected animal

    [Header("Animal Data")]/////////////wil be made in to scriptable objects so it doesnt have to be like this here
    [SerializeField] private AnimalData[] animals;

    [Header("Other UI Data")]
    [SerializeField] private TMPro.TextMeshProUGUI moneyAmount;

    [Header("Private Data")]
    private bool[] unlocked = new bool[50];
    private int money = 0;
    private int lastSelected = 0;

    private void Start()
    {
        loadData();
        if(loadUi)
        {
            showRight(lastSelected);
        }
        // unlocked[0] = true;
    }

    private void loadData()
    {
        BookData loadData = SaveScript.loadBook();
        if(loadData != null)
        {
            unlocked = loadData.unlocks;
            money = loadData.money;
        }
        else
        {
            Debug.Log("No save data found");
        }
        
        if(loadUi)
        {
            setData();
        }
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
            bool exist = i < animals.Length;
            if(exist)
            {
                uiSlots[i].setData(i,animals[i].animalImage,unlocked[i],true,this);
            }
            uiSlots[i].gameObject.SetActive(exist);
        }
    }   

    public void buyAnimal()
    {
        if(money >= animals[lastSelected].price && !unlocked[lastSelected])
        {
            money -= animals[lastSelected].price;
            unlocked[lastSelected] = true;
            SaveScript.saveBook(this);//saves book data

            uiSlots[lastSelected].setData(lastSelected,animals[lastSelected].animalImage,unlocked[lastSelected],true,this);
            showRight(lastSelected);
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
        
        showRight(showId);
    }

    private void showRight(int id)
    {
        rightAnimalSlot.setData(id,animals[id].animalImage,unlocked[id],true,this);

        if(unlocked[id])
        {
            animalInfo[0].text = animals[id].name;
            animalInfo[1].text = animals[id].info1;
            animalInfo[2].text = animals[id].info2;
            animalInfo[3].text = animals[id].fact;
        }
        else
        {
            for(int i=0; i<animalInfo.Length; i++)
            {
                animalInfo[i].text = "Unknown";
            }
        }
    }

    public void selectAnimal(int slotId)//selecting animal with left click
    {
        lastSelected = slotId;
        showRight(lastSelected);
    }

    public AnimalData getCurrent()
    {
        if(unlocked[lastSelected])
        {
            return animals[lastSelected];
        }   
        else//when not unlocked
        {
            return null;
        }
    }

    public bool[] getUnlocks()
    {
        return unlocked;
    }

    public int getMoney()
    {
        return money;
    }

    /////////////in game

    public void saveMoney(int amount)
    {
        money += amount;
        SaveScript.saveBook(this);//saves book data

        Debug.Log("Saved : " + money);
    }
}
