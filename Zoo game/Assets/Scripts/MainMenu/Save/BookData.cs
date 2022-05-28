using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BookData 
{   
    [Header("Ints")]
    public int money = 0;

    [Header("Bools")]
    public bool[] unlocks = new bool[50];//bool array of unlocked animals

    public BookData(BookController oData)
    { 
        /*
            set the public values example : 
            username = oData.username;
        */
    }
    
}
