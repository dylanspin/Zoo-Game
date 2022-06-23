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
    public int[] highScores = new int[50];//int array of animals highscores

    public BookData(BookController oData)
    { 
        unlocks = oData.getUnlocks();
        money = oData.getMoney();
        highScores = oData.getHighScore();
    }
    
}
