using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalCol : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject[] objectEffect;

    [Header("Settings")]
    [SerializeField] private float collisionForce = 100;
    [SerializeField] private float unlockTime = 0.5f;

    [Header("Scripts")]
    [SerializeField] private AnimalMovement moveScript;
    [SerializeField] private Abilities abilityScript;
    [SerializeField] private CamShake shakeScript;//camera shake/camera  

    [Header("Private data")]
    private int health = 4;
    private bool canCollide = true;
    private bool canbreak = false;
    private bool charging = false;
    private ParticleSystem collEffect;
    private Controller controllerScript;
    private GameObject animalObject;

    public void setStartData(AnimalData newData,AnimalPrefab prefabScript,Controller newController)
    {
        canbreak = newData.canBreak;
        health = newData.health;
        collEffect = prefabScript.collideEffect;
        animalObject = prefabScript.animalBody;;
        controllerScript = newController;
    }   

    private void OnCollisionEnter(Collision other) 
    {
        collide(other);//checks collision
    }

    private void collide(Collision other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Ground"))//when hitting something else thats not the ground
        {
            if(!other.transform.root.GetComponent<Rigidbody>())
            {
                if(!canbreak && !charging)
                {
                    collided(other);
                }
                else
                {
                    Transform rootObj = other.transform.parent.parent.transform;
                    // if(rootObj.tag == "canBreak")
                    // {   
                    //     CollidedObject effect = Instantiate(objectEffect,rootObj.transform.position,Quaternion.Euler(0,0,0)).GetComponent<CollidedObject>();
                    //     effect.setObject(rootObj,other);
                    // }
                    if(rootObj.tag == "bigBreak" || rootObj.tag == "canBreak")
                    {
                        if(canCollide || charging)
                        {
                            if(!charging)
                            {
                                canCollide = false;
                                abilityScript.setBarZero();
                            }
                            CollidedObject effect = Instantiate(objectEffect[0],rootObj.transform.position,Quaternion.Euler(0,0,0)).GetComponent<CollidedObject>();
                            effect.setObject(rootObj,other);
                        }
                        else
                        {
                            collided(other);
                        }
                    }
                    else
                    {
                        collided(other);
                    }
                    StartCoroutine(shakeScript.Shake(0.25f,0.05f));
                }
            }
        }
    }

    private void collided(Collision other)
    {
        if(!moveScript.getLocked())
        {
            bool dead = loseHealth();
            controllerScript.collided(unlockTime,dead);
            controllerScript.setHealth(health);
            removeAllFromPart(dead,other);
            addKnockBack(dead);
        }
    }

    private void removeAllFromPart(bool dead,Collision other)
    {
        if(!dead)
        {
            other.transform.GetComponentInParent<TrackPart>().removeObstacles(objectEffect[1]);
        }
    }

    private void addKnockBack(bool dead)
    {
        if(!moveScript.getLocked())
        {
            if(dead)
            {
                animalObject.transform.parent.parent.gameObject.layer = LayerMask.NameToLayer("KnockBackLayer");
            }
            inGameAudio.playSoundEffect(1);
            collEffect.Clear();
            collEffect.Play();
            moveScript.addKnockBack(collisionForce,unlockTime,dead);
            StartCoroutine(shakeScript.Shake(0.25f,0.05f));
        }
    }

    public void setCharging(bool active)
    {
        charging = active;
    }
    
    public void resetCol()
    {
        if(canbreak)
        {
            canCollide = true;
        }
    }

    private bool loseHealth()
    {
        Debug.Log("Lose Health");
        if(health > 1)
        {
            health --;
            return false;
        }
        else
        {
            health = 0;
            return true;
        }
    }
}
