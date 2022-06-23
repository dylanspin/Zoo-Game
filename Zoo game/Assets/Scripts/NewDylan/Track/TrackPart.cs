using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPart : MonoBehaviour
{
    [SerializeField] private GameObject[] possibleEndParts;//possible end track parts
    [SerializeField] private Transform[] coinSpawns;//rubie spawn points
    [SerializeField] private GameObject coinPrefab;//rubie prefab
    [SerializeField] private Transform[] obstacles;//all obstacles on the track
    private TrackController controllerScript;//the controller of the complete track
    private CollectController collectScript;//the script that keeps track of collect rubbies
    private bool coinSpawned = false;//if coins are spawned
    private bool created = false;//if created new end piece is double check so there arent double track parts
    private bool triggerd = false;//if player went trough the trigger

    public void setStart(TrackController newController,CollectController newCollect)//sets start data for the track part
    {
        if(!coinSpawned)
        {
            coinSpawned = true;
            controllerScript = newController;
            collectScript = newCollect;
            spawnCoins();//spawns all the coins on the track
            if(coinSpawns.Length > 0)//deactivates the coin spawn position object if it was still active on accident
            {
                if(coinSpawns[0])
                {
                    coinSpawns[0].parent.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)//when player triggers the center trigger box
    {
        addEnd();
    }

    public void addEnd()//adds new part at the end of the track in the track controller script
    {
        if(!triggerd)
        {
            triggerd = true;
            controllerScript.createEnd();
        }
    }

    public void createNewPart()//is called on the newest track part of the complete track 
    {
        if(!created)
        {
            created = true;
            controllerScript.spawnNewPart(transform.position,possibleEndParts[Random.Range(0,possibleEndParts.Length)],true);
        }
    }

    private void spawnCoins()//spawns coins on the track on the coin positions
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

    public void removeObstacles(GameObject spawnEffect)//when the player collides with obstacle on this track part despawn all obstacles
    {
        for(int i=0; i<obstacles.Length; i++)
        {
            if(obstacles[i])
            {
                CollidedObject effect = Instantiate(spawnEffect,obstacles[i].position,Quaternion.Euler(0,0,0)).GetComponent<CollidedObject>();//spawns despawn effect
                effect.setObject(obstacles[i],null);
            }
        }
    }
}
