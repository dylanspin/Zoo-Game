using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectController : MonoBehaviour
{
    [Header("Set Data")]
    [SerializeField] private List<Transform> hiddenPoints = new List<Transform>();
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject friendPrefab;
    [SerializeField] private UiController uiScript;
    [SerializeField] private int coinAmount = 30;
    
    [Header("Private Data")]
    private int coinsCollected = 0;
    private int friendsCollected = 0;

    private void Start() 
    {
        spawnFriend();
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
            uiScript.setCollected(coinsCollected);
        }
        else
        {
            friendsCollected ++;
        }
    }

    public int getMoney()
    {
        return coinsCollected;
    }
}
