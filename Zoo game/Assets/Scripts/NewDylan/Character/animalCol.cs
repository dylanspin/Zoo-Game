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
    [SerializeField] private Controller controllerScript; 

    [Header("Private data")]
    private bool canbreak = false;
    private ParticleSystem collEffect;

    public void setStartData(AnimalData newData,ParticleSystem newPs)
    {
        canbreak = newData.canBreak;
        collEffect = newPs;
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
                controllerScript.collided(unlockTime);
                addKnockBack();
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
                        addKnockBack();
                    }
                    //needs to check what interaction needs to happen with object and if its breakable else also add knockback
                    breakObject(other);
                    StartCoroutine(shakeScript.Shake(0.25f,0.05f));
                }
            }
        }
    }

    private void addKnockBack()
    {
        if(!moveScript.getLocked())
        {
            inGameAudio.playSoundEffect(1);
            collEffect.Clear();
            collEffect.Play();
            moveScript.addKnockBack(collisionForce,unlockTime);
            StartCoroutine(shakeScript.Shake(0.25f,0.05f));
        }
    }

    private void breakObject(Collision other)
    {
       
    }
}
