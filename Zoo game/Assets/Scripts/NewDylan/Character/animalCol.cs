using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalCol : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject[] objectEffect;//the different despawn effects

    [Header("Settings")]
    [SerializeField] private float collisionForce = 100;//knock back force when collided
    [SerializeField] private float unlockTime = 0.5f;//time it takes for the animal to move again after collision

    [Header("Scripts")]
    [SerializeField] private AnimalMovement moveScript;
    [SerializeField] private Abilities abilityScript;
    [SerializeField] private CamShake shakeScript;//camera shake/camera  

    [Header("Private data")]
    private int health = 4;//current health
    private bool canCollide = true;
    private bool canbreak = false;
    private bool charging = false;
    private ParticleSystem collEffect;//stars effect
    private Controller controllerScript;//game controller script
    private GameObject animalObject;//the animal body 

    public void setStartData(AnimalData newData,AnimalPrefab prefabScript,Controller newController)//sets start data
    {
        canbreak = newData.canBreak;
        health = newData.health;
        collEffect = prefabScript.collideEffect;
        animalObject = prefabScript.animalBody;;
        controllerScript = newController;
    }   

    private void OnCollisionEnter(Collision other) //when collided with object
    {
        collide(other);//checks collision
    }

    private void collide(Collision other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Ground"))//when hitting something else thats not the ground
        {
            if(!other.transform.root.GetComponent<Rigidbody>())//if the object doesnt already have a rb (the hit effect on objects)
            {
                if(!canbreak && !charging)//if the animal cant break trough objects
                {
                    collided(other);//normal collision loses life and adds knock back
                }
                else
                {
                    Transform rootObj = other.transform.parent.parent.transform;
                    // if(rootObj.tag == "canBreak")//not used anymore used to be that you could only break small objects
                    // {   
                    //     CollidedObject effect = Instantiate(objectEffect,rootObj.transform.position,Quaternion.Euler(0,0,0)).GetComponent<CollidedObject>();
                    //     effect.setObject(rootObj,other);
                    // }
                    if(rootObj.tag == "bigBreak" || rootObj.tag == "canBreak")//if object can be moved
                    {
                        if(canCollide || charging)//if the animal hasent hit an object in a while or if its charging
                        {
                            if(!charging)
                            {
                                canCollide = false;
                                abilityScript.setBarZero();//if not charing set the bar to 0 and the next hit will add knockback and lose a life
                            }
                            CollidedObject effect = Instantiate(objectEffect[0],rootObj.transform.position,Quaternion.Euler(0,0,0)).GetComponent<CollidedObject>();//add despawn effect to the hit object
                            effect.setObject(rootObj,other);
                            StartCoroutine(shakeScript.Shake(0.25f,0.05f));//triggers the camera shake
                        }
                        else
                        {
                            collided(other);//normal collision loses life and adds knock back
                        }
                    }
                    else
                    {
                        collided(other);//normal collision loses life and adds knock back
                    }
                }
            }
        }
    }

    private void collided(Collision other)//checks collision
    {
        if(!moveScript.getLocked())
        {
            bool dead = loseHealth();//if the player has lost all their lifes
            controllerScript.collided(unlockTime,dead,health);//stops the track movement and does other stuff related to the collision
            removeAllFromPart(dead,other);//removes all obstacles from the track part
            addKnockBack(dead);//add knocks back to the player and triggers the effect
        }
    }

    private void removeAllFromPart(bool dead,Collision other)
    {
        if(!dead)
        {
            other.transform.GetComponentInParent<TrackPart>().removeObstacles(objectEffect[1]);//finds the track part script and removes all obstacles in that function
        }
    }

    private void addKnockBack(bool dead)
    {
        if(!moveScript.getLocked())
        {
            if(dead)
            {
                animalObject.transform.parent.parent.gameObject.layer = LayerMask.NameToLayer("KnockBackLayer");//sets the layer that ignores obstacles so the player wont be stuck in the collided object
            }
            inGameAudio.playSoundEffect(1);//plays hit sound effect
            collEffect.Clear();//restarts the collision effect
            collEffect.Play();//plays the collision effect
            moveScript.addKnockBack(collisionForce,unlockTime,dead);//ads knockback in the movement script
            StartCoroutine(shakeScript.Shake(0.25f,0.05f));//shakes the camera 
        }
    }

    public void setCharging(bool active)//activates charging via the abilities script
    {
        charging = active;
    }
    
    public void resetCol()//actives the one save hit again via the abilities script
    {
        if(canbreak)
        {
            canCollide = true;
        }
    }

    private bool loseHealth()//loses health on collision
    {
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
