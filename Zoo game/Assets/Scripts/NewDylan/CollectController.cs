using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectController : MonoBehaviour
{
    [Header("Set Data")]
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> hiddenPoints = new List<Transform>();
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject friendPrefab;
    [SerializeField] private int coinAmount = 30;
    
    [Header("Private Data")]
    private int coinsCollected = 0;
    private int friendsCollected = 0;

    private void Start() 
    {
        spawnCoins();//spawns coin at random positions
        spawnFriend();
    }

    private void spawnCoins()
    {
        int spawnAmount = coinAmount;
        if(coinAmount > spawnPoints.Count)//checks if spawn amount is not greater then the spawnpoints 
        {
            spawnAmount = spawnPoints.Count;
        }

        for(int i=0; i<spawnAmount; i++)
        {
            Transform coinPoint = spawnPoints[Random.Range(0,spawnPoints.Count)];
            GameObject spawnedCoin = Instantiate(coinPrefab,coinPoint.position,Quaternion.Euler(0,0,0));
            spawnedCoin.GetComponent<Animator>().speed = Random.Range(0.7f,1.0f);
            spawnedCoin.GetComponent<Collect>().setStart(this);
            spawnPoints.Remove(coinPoint);//removes point from pool
        }
    }

    private void spawnFriend()
    {
        Transform hiddenPoint = hiddenPoints[Random.Range(0,hiddenPoints.Count)];
        GameObject spawnedHidden = Instantiate(friendPrefab,hiddenPoint.position,Quaternion.Euler(0,0,0));
        spawnedHidden.GetComponent<Collect>().setStart(this);
    }
    
    public void collectItem(bool isCoin)
    {
        if(isCoin)
        {
            coinsCollected ++;
        }
        else
        {
            friendsCollected ++;
        }
    }
}
