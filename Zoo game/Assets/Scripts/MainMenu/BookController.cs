using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour
{   
    [Header("Scripts")]
    [SerializeField] private UiController uiScript;

    [Header("Set Data")]
    [SerializeField] private int[] prices = new int[50];

    [Header("Private Data")]
    private bool[] unlocked = new bool[50];


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
    }

    public void unlockAnimal(int slot)
    {
        unlocked[slot] = true;
        //do some ui Stuff
    }   
}
