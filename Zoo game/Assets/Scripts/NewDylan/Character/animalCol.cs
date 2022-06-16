using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalCol : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject objectEffect;

    [Header("Settings")]
    [SerializeField] private float collisionForce = 100;
    [SerializeField] private float unlockTime = 0.5f;

    [Header("Scripts")]
    [SerializeField] private AnimalMovement moveScript;
    [SerializeField] private Abilities abilityScript;
    [SerializeField] private CamShake shakeScript;//camera shake/camera  

    [Header("Private data")]
    private int health = 4;
    private bool canbreak = false;
    private ParticleSystem collEffect;
    private Controller controllerScript;

    public void setStartData(AnimalData newData,ParticleSystem newPs,Controller newController)
    {
        canbreak = newData.canBreak;
        health = newData.health;
        collEffect = newPs;
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
            if(!canbreak)
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
            else
            {
                if(!other.transform.root.GetComponent<Rigidbody>())
                {
                    Transform rootObj = other.transform.parent.parent.transform;
                    if(rootObj.tag == "canBreak")
                    {   
                        CollidedObject effect = Instantiate(objectEffect,rootObj.transform.position,Quaternion.Euler(0,0,0)).GetComponent<CollidedObject>();
                        effect.setObject(rootObj,other);
                    }
                    else
                    {
                        bool dead = loseHealth();
                        removeAllFromPart(dead,other);
                        addKnockBack(dead);
                    }
                    //needs to check what interaction needs to happen with object and if its breakable else also add knockback
                    StartCoroutine(shakeScript.Shake(0.25f,0.05f));
                }
            }
        }
    }

    private void removeAllFromPart(bool dead,Collision other)
    {
        if(!dead)
        {
            other.transform.GetComponentInParent<TrackPart>().removeObstacles(objectEffect);
        }
    }

    private void addKnockBack(bool dead)
    {
        if(!moveScript.getLocked())
        {
            inGameAudio.playSoundEffect(1);
            collEffect.Clear();
            collEffect.Play();
            moveScript.addKnockBack(collisionForce,unlockTime,dead);
            StartCoroutine(shakeScript.Shake(0.25f,0.05f));
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
