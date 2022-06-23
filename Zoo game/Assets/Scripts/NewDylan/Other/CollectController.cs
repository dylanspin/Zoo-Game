using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectController : MonoBehaviour
{
    [Header("Set Data")]
    [SerializeField] private List<Transform> hiddenPoints = new List<Transform>();//hidden friend spawn points
    [SerializeField] private Transform[] coinSpawns;//rubby spawn points
    [SerializeField] private GameObject coinPrefab;//rubby prefab
    [SerializeField] private GameObject friendPrefab;//hidden friend prefab
    [SerializeField] private UiController uiScript;//in game ui script
    [SerializeField] private int coinAmount = 30;//max amount of coins to spawn isnt used anymore
    
    [Header("Private Data")]
    private int coinsCollected = 0;//total amount of coins collected
    private int friendsCollected = 0;//not used anymore

    private void Start() 
    {
        //for the open world it spawns coins and hidden friend at the start
        // spawnFriend();
        // spawnCoins();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.L))//for cheating coins in the demo
        {
            collectItem(true);
        }
    }

    private void spawnCoins()//spawns coins at the start of the match in the open world but isnt used anymore
    {
        for(int i=0; i<coinSpawns.Length; i++)
        {
            GameObject spawnedCoin = Instantiate(coinPrefab,coinSpawns[i].position,Quaternion.Euler(0,0,0),transform);
            spawnedCoin.GetComponent<Animator>().speed = Random.Range(0.7f,1.0f);
            spawnedCoin.GetComponent<Collect>().setStart(this);
        }
        uiScript.setCollected(0,coinAmount);
    }

    public void collectItem(bool isCoin)//add collectable to the counter
    {
        if(isCoin)
        {
            coinsCollected ++;
            uiScript.setCollected(coinsCollected,coinAmount);
        }
        else
        {
            friendsCollected ++;
        }
    }

    private void spawnFriend()//not used anymore was for spawning a hidden friend at a random point in the world
    {
        if(hiddenPoints.Count > 1)
        {
            Transform hiddenPoint = hiddenPoints[Random.Range(0,hiddenPoints.Count)];
            GameObject spawnedHidden = Instantiate(friendPrefab,hiddenPoint.position,Quaternion.Euler(0,0,0));
            spawnedHidden.GetComponent<Collect>().setStart(this);
        }
    }
    

    public int getMoney()//sends the current coin amount
    {
        return coinsCollected;
    }

    public int getmax()//sends the max amount of coins
    {
        return coinAmount;
    }
}
