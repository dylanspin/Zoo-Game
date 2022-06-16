using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPart : MonoBehaviour
{
    [SerializeField] private GameObject[] possibleEndParts;
    [SerializeField] private Transform[] coinSpawns;
    [SerializeField] private GameObject coinPrefab;
    private TrackController controllerScript;
    private CollectController collectScript;
    private bool coinSpawned = false;
    private bool created = false;
    private bool triggerd = false;

    public void setStart(TrackController newController,CollectController newCollect)
    {
        if(!coinSpawned)
        {
            coinSpawned = true;
            controllerScript = newController;
            collectScript = newCollect;
            spawnCoins();//bugged
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        addEnd();
    }

    public void createNewPart()
    {
        if(!created)
        {
            created = true;
            controllerScript.spawnNewPart(transform.position,possibleEndParts[Random.Range(0,possibleEndParts.Length)],true);
        }
    }

    public void addEnd()
    {
        if(!triggerd)
        {
            triggerd = true;
            controllerScript.createEnd();
        }
    }

    private void spawnCoins()
    {
        for(int i=0; i<coinSpawns.Length; i++)
        {
            if(coinSpawns[i])
            {
                GameObject spawnedCoin = Instantiate(coinPrefab,coinSpawns[i].position,Quaternion.Euler(0,0,0),transform);
                spawnedCoin.GetComponent<Animator>().speed = Random.Range(0.7f,1.0f);
                spawnedCoin.GetComponent<Collect>().setStart(collectScript);
            }
        }
    }
}
