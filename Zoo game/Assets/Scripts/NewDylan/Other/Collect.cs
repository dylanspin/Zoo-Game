using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    [Header("Set data")]
    [SerializeField] private ParticleSystem collectEffect;//particle effect for the collectable 
    [SerializeField] private Animator anim;//collect animator
    [SerializeField] private bool isCoin = true;//if its a coin collectable

    [Header("Private data")]
    private bool collected = false;
    private CollectController controlScript;//main collect controller what keeps track of collected items
    
    public void setStart(CollectController newScript)//sets the start data
    {
        controlScript = newScript;
    }

    private void OnTriggerEnter(Collider other)//when something triggers the trigger around the collectable
    {
        collectThis();
    }

    private void collectThis()
    {
        if(!collected)  
        {
            collected = true;//so it cant be collected again
            anim.SetBool("Collect",true);//activates schrinking animation
            inGameAudio.playSoundEffect(0);//play collect sound effect
            if(collectEffect)
            {
                collectEffect.Play();//plays collect particle effect
            }
            controlScript.collectItem(isCoin);//ads the coin to the collected amount
        }
    }
}
