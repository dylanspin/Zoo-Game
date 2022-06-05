using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalCol : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float collisionForce = 100;
    [SerializeField] private float unlockTime = 0.5f;

    [Header("Scripts")]
    [SerializeField] private AnimalMovement moveScript;
    [SerializeField] private Abilities abilityScript;
    [SerializeField] private CamShake shakeScript;//camera shake/camera 

    [Header("Private data")]
    private bool canbreak = false;

    public void setStartData(AnimalData newData)
    {
        canbreak = newData.canBreak;
    }   

    private void OnCollisionEnter(Collision other) 
    {
        collide(other);//checks collision
    }

    private void collide(Collision other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Ground"))//when hitting something else thats not the ground
        {
            if(true)//moveScript.getGroundPos().position.y < other.contacts[0].point.y //if collision is beneath ground check still needs fixing
            {
                if(!canbreak)
                {
                    Debug.Log("Can collide");
                    if(!moveScript.getLocked())
                    {
                        Debug.Log("not locked");
                        moveScript.addKnockBack(other,collisionForce,unlockTime);
                    }
                }
                else
                {
                    //needs to check what interaction needs to happen with object and if its breakable else also add knockback
                    breakObject(other);
                }
                StartCoroutine(shakeScript.Shake(0.25f,0.05f));
            }
            else
            {
                Debug.Log("Is beneath");
            }
        }
    }

    private void breakObject(Collision other)
    {
       
    }
}
