using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BookController : MonoBehaviour
{   
    [Header("Animal UI")]
    [SerializeField] private bool loadUi = false;//if its in the main menu load ui other wise just load the data
    [SerializeField] private AnimalSlot[] uiSlots;//animal ui slots in the book
    [SerializeField] private AnimalSlot rightAnimalSlot;//the right animal picture 
    [SerializeField] private TMPro.TextMeshProUGUI[] animalInfo;//the text related to the selected animal

    [Header("Animal Data")]
    [SerializeField] private AnimalData[] animals;//the scriptable animal data objects

    [Header("Other UI Data")]
    [SerializeField] private TMPro.TextMeshProUGUI moneyAmount;//the text for the amount of money
    [SerializeField] private TMPro.TextMeshProUGUI highScore;//the highscore tab
    [SerializeField] private buyPopUp popUp;//the price tag pop up for buying the animals so the player cant buy the animal on accident as easily
    [SerializeField] private unlockedAnimal unlockScreen;//unlocked screen script for when the animal is bought or when the info tab is clicked
    [SerializeField] private SoundController soundScript;//the sound effect script for button click sounds etc
    [SerializeField] private GameObject[] buttons;//all buttons for displaying or hiding them when the animal is unlocked or locked

    [Header("Private Data")]
    private int[] highScores = new int[50];//the high scores save array
    private bool[] unlocked = new bool[50];//this array keeps track if the animal is unlocked or not
    private int money = 0;//amount of money thats saved
    private int lastSelected = 0;//last selected animal in the scrap book
    private int langues = 0;//0 = english 1 = dutch

    private void Start()
    {
        loadData();
        if(loadUi)
        {
            showRight(lastSelected);
        }
    }

    private void Update()
    {
        resetBook();
    }

    private void resetBook()//for demo resseting
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
        if(loadData != null)//if book data was found set to saved data
        {
            highScores = loadData.highScores;
            unlocked = loadData.unlocks;
            money = loadData.money;
        }
        else//if the book data wasent found load default amount
        {
            unlocked[0] = true;//unlocks the first animal on default
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
        setSlots();//sets the ui slots of the animals
        moneyAmount.text = money.ToString();//sets the money at the start of the scene
    }

    private void setSlots()//loads all the animal ui slots
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
        //refreshes the UI 
        setSlots();
        showRight(lastSelected);
    }

    public void closeAll()//for going back to the main screen it closes all other pop ups in the book section
    {
        popUp.closePopUp();
        unlockScreen.closeWindow();
    }

    public void showCode()//show code input pop up
    {
        popUp.showPopUp(animals[lastSelected],langues);
        soundScript.playSoundEffect(0);
    }

    public void showBuy()//show buy price tag pop up
    {
        popUp.showPopUp(animals[lastSelected],langues);
        soundScript.playSoundEffect(0);
    }

    public void buyAnimal()//when the buy or code button is pressed in the pop ups
    {
        bool canUnlock = false;
        if(animals[lastSelected].codeunlock)//checks if the animal is unlocked with a code
        {
            canUnlock = popUp.getCode() == animals[lastSelected].code;//check if the code was correct
        }
        else
        {
            canUnlock = money >= animals[lastSelected].price && !unlocked[lastSelected];//check if the player has enough money to buy the animal
        }

        if(canUnlock)//if the animal can be unlocked either with money or with the code
        {
            money -= animals[lastSelected].price;//removes the money from the players amount for buying the animal
            unlocked[lastSelected] = true;//sets the slot of the animal to unlocked for saving
            SaveScript.saveBook(this);//saves book data

            //UI
            moneyAmount.text = money.ToString();//sets the new money amount
            uiSlots[lastSelected].setData(lastSelected,animals[lastSelected],unlocked[lastSelected],true,this,langues);//loads the UI slot again
            showRight(lastSelected);//Shows unlocked animal on the right side of the book
            popUp.closePopUp();//closes the buy or code pop up
            unlockScreen.showUnlocked(animals[lastSelected],langues);//shows the unlocked screen with more information about that animal
            showButtons(0,3);//shows the buttons that should show when the animal is unlocked
        }
        else
        {
            popUp.cantBuy();//shakes the pop up to show the code is incorrect or the player doesnt have enough money
        }
    }   

    public void showUnlocked()//for when the player wants to open the info again
    {
        if(unlocked[lastSelected])
        {
            soundScript.playSoundEffect(0);
            unlockScreen.showUnlocked(animals[lastSelected],langues);
        }
    }

    public void hoverSet(int slotId,bool active)//sets the information on the right side of the book when the mouse is hovering over animal slot
    {
        int showId = slotId;
        if(!active)
        {
            showId = lastSelected;
        }
        
        showRight(showId);
    }

    private void showRight(int id)//sets all the information on the right side of the book about the animal
    {
        rightAnimalSlot.setData(id,animals[id],unlocked[id],true,this,langues);
        highScore.transform.parent.parent.gameObject.SetActive(unlocked[id]);//turns on highscore

        if(unlocked[id])//if the animal is unlocked show the actual animal data
        {
            animalInfo[0].text = animals[id].animalName[langues];
            animalInfo[1].text = animals[id].info1[langues];
            animalInfo[2].text = animals[id].info2[langues];
            animalInfo[3].text = animals[id].fact[langues];
            animalInfo[4].transform.parent.gameObject.SetActive(false);//turns of the price tag
            highScore.text = highScores[id] + "M";//sets the high score of the animal
        }
        else
        {
            string notKnown = "Unknown";
            if(langues == 1)//dutch
            {
                notKnown = "Onbekend";
            }

            for(int i=0; i<animalInfo.Length; i++)
            {
                animalInfo[i].text = notKnown;//sets all the information to unkown because the animal isnt unlocked
            }

            animalInfo[4].transform.parent.gameObject.SetActive(true);//turns on the price tag
            animalInfo[4].text = animals[id].price.ToString();//shows the price of the animal in the price tag
            if(animals[id].codeunlock)
            {
                animalInfo[4].text = "Code";//shows code instead of the price when the animal is unlocked with a code
            }
        }

        checkButtons(id);
    }

    public void selectAnimal(int id)//selecting animal with left click
    {
        lastSelected = id;//sets the last selected to the clicked animal ID
        showRight(lastSelected);
        checkButtons(lastSelected);
        soundScript.playSoundEffect(2);//plays button sound effect
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

    public void showButtons(int slot,int second)//shows the buttons to the related situation
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
    
    public AnimalData getCurrent()//gets the current selected animal data
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

    public bool[] getUnlocks()//returns what animals are unlcoked
    {
        return unlocked;
    }

    public int getMoney()//for saving the money amount
    {
        return money;
    }

    public int[] getHighScore()//for saving the highscore 
    {
        return highScores;
    }


    /////////////in game
    public bool checkHighScore(int distance)//checks if the new score is higher then the saved score
    {
        return distance > highScores[Values.activeId];
    }

    public void saveEndData(int amount,int distance)//saves the new data
    {
        money += amount;
        if(distance > highScores[Values.activeId])
        {
            highScores[Values.activeId] = distance;
        }
        SaveScript.saveBook(this);//saves book data
    }
}
