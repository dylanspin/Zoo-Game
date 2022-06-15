using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    [SerializeField] private TMPro.TextMeshProUGUI highScore;
    [SerializeField] private buyPopUp popUp;
    [SerializeField] private unlockedAnimal unlockScreen;
    [SerializeField] private GameObject[] buttons;

    [Header("Private Data")]
    private int[] highScores = new int[50];
    private bool[] unlocked = new bool[50];
    private int money = 0;
    private int lastSelected = 0;
    private int langues = 0;//0 = english 1 = dutch

    private void Start()
    {
        loadData();
        if(loadUi)
        {
            showRight(lastSelected);
        }
        // unlocked[0] = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Numlock))
        {
            if(loadUi)
            {
                for(int i=0; i<unlocked.Length; i++)
                {
                    unlocked[i] = false;
                    highScores[i] = 0;
                }
                unlocked[0] = true;
                money = 0;
                SaveScript.saveBook(this);//saves book data
                setData();
                showRight(lastSelected);
            }
        }
    }

    private void loadData()
    {
        BookData loadData = SaveScript.loadBook();
        if(loadData != null)
        {
            highScores = loadData.highScores;
            unlocked = loadData.unlocks;
            money = loadData.money;
        }
        else
        {
            unlocked[0] = true;
            money = 0;
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
                uiSlots[i].setData(i,animals[i],unlocked[i],true,this,langues);
            }
            uiSlots[i].gameObject.SetActive(exist);
        }
    }   

    public void setLangues(int newLang)
    {
        langues = newLang;
        setSlots();
        showRight(lastSelected);
        //refresh ui
    }


    public void showCode()
    {
        popUp.showPopUp(animals[lastSelected],langues);
    }

    public void showBuy()
    {
        popUp.showPopUp(animals[lastSelected],langues);
    }

    public void buyAnimal()
    {
        bool canUnlock = false;
        if(animals[lastSelected].codeunlock)
        {
            canUnlock = popUp.getCode() == animals[lastSelected].code;
        }
        else
        {
            canUnlock = money >= animals[lastSelected].price && !unlocked[lastSelected];
        }

        if(canUnlock)
        {
            money -= animals[lastSelected].price;
            unlocked[lastSelected] = true;
            SaveScript.saveBook(this);//saves book data

            //ui
            uiSlots[lastSelected].setData(lastSelected,animals[lastSelected],unlocked[lastSelected],true,this,langues);
            showRight(lastSelected);
            popUp.closePopUp();
            unlockScreen.showUnlocked(animals[lastSelected],langues);
            showButtons(0,3);
        }
        else
        {
            popUp.cantBuy();
            //show message or something else indicating not enough money
        }
    }   

    public void showUnlocked()//for when the player wants to open the info again
    {
        if(unlocked[lastSelected])
        {
            unlockScreen.showUnlocked(animals[lastSelected],langues);
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
        rightAnimalSlot.setData(id,animals[id],unlocked[id],true,this,langues);
        highScore.transform.parent.parent.gameObject.SetActive(unlocked[id]);//turns on highscore

        if(unlocked[id])
        {
            animalInfo[0].text = animals[id].animalName[langues];
            animalInfo[1].text = animals[id].info1[langues];
            animalInfo[2].text = animals[id].info2[langues];
            animalInfo[3].text = animals[id].fact[langues];
            animalInfo[4].transform.parent.gameObject.SetActive(false);
            highScore.text = highScores[id] + "M";
        }
        else
        {
            string notKnown = "Unknown";
            if(langues == 1)
            {
                notKnown = "Onbekend";
            }
            for(int i=0; i<animalInfo.Length; i++)
            {
                
                animalInfo[i].text = notKnown;
            }
            animalInfo[4].transform.parent.gameObject.SetActive(true);
            animalInfo[4].text = animals[id].price.ToString();
            if(animals[id].codeunlock)
            {
                animalInfo[4].text = "Code";
            }
        }

        checkButtons(id);
    }

    public void selectAnimal(int id)//selecting animal with left click
    {
        lastSelected = id;
        showRight(lastSelected);
        checkButtons(lastSelected);
    }

    private void checkButtons(int id)
    {
        if(unlocked[id])
        {
            showButtons(0,3);//play button
        }
        else
        {
            if(animals[id].codeunlock)
            {
                showButtons(2,-1);//code button
            }
            else
            {
                showButtons(1,-1);//buy button
            }
        }
    }

    public void showButtons(int slot,int second)
    {
        for(int i=0; i<buttons.Length; i++)
        {
            if(i != slot && i != second)
            {
                buttons[i].SetActive(false);
            }
            else
            {
                buttons[i].SetActive(true);
            }
        }
    }
    
    public AnimalData getCurrent()
    {
        if(unlocked[lastSelected])
        {
            Values.activeId = lastSelected;//for saving the highScore
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

    public int[] getHighScore()
    {
        return highScores;
    }

    /////////////in game

    public bool checkHighScore(int distance)
    {
        return distance > highScores[Values.activeId];
    }

    public void saveEndData(int amount,int distance)
    {
        money += amount;
        if(distance > highScores[Values.activeId])
        {
            highScores[Values.activeId] = distance;
        }
        SaveScript.saveBook(this);//saves book data
    }
}
