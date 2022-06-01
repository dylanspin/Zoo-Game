using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    [Header("Set data")]
    [SerializeField] private ParticleSystem collectEffect;
    [SerializeField] private Animator anim;
    [SerializeField] private bool isCoin = true;

    [Header("Private data")]
    private bool collected = false;
    private CollectController controlScript;
    
    public void setStart(CollectController newScript)
    {
        controlScript = newScript;
    }

    private void OnTriggerEnter(Collider other)
    {
        collectThis();
    }

    private void collectThis()
    {
        if(!collected)  
        {
            collected = true;
            anim.SetBool("Collect",true);
            // gameObject.SetActive(false);
            if(collectEffect)
            {
                collectEffect.Play();
            }
            controlScript.collectItem(isCoin);
        }
    }
}
