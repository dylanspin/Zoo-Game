using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    // [Header("Settings")]
    // private bool canDig = true;

    [Header("Scripts")]
    [SerializeField] private AnimalMovement moveScript;
    [SerializeField] private animalCol colScript;
    [SerializeField] private AbilityBar barScript;

    [Header("SetData")]
    [SerializeField] private GameObject animalObject;
    [SerializeField] private string[] playerLayers;
    [SerializeField] LayerMask rockLayer;

    [Header("private data")]
    private ParticleSystem specialEffect;
    private bool digging = false;
    private bool canDig = false;
    private bool canCharge = false;
    private bool canRockJump = false;

    public void setStartData(AnimalPrefab prefabScript,AnimalData newData,AbilityBar newBarScript)
    {
        animalObject = prefabScript.animalBody;
        canDig = newData.canDig;
        canRockJump = newData.rockJump;
        canCharge = newData.canCharge;
        barScript = newBarScript;
        if(prefabScript.specialPs)
        {
            specialEffect = prefabScript.specialPs;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            checkSpecial();
        }
        checkAbilities();
    }

    private void checkAbilities()
    {
        if(canRockJump)
        {
            Vector3 drawPos = new Vector3(transform.position.x,transform.position.y+5,transform.position.z);
            bool rockFront = (Physics.BoxCastAll(drawPos, Vector3.one, transform.forward, Quaternion.Euler(0,0,0), 80, rockLayer).Length > 0);
            moveScript.setDoubleJump(rockFront);
            barScript.showStatus(rockFront);
        }
    }

    public void checkSpecial()
    {
        if(canDig)
        {
            dig(!digging);
        }
        if(canCharge)
        {
            charge(true);
            // setCharging
        }
    }

    /*
        digging ability

        Still needs delay 
        Animation
        ground effect
        Digging above ground trail effect
        When going up check if there is something in the way if so cant go up
        If cant go up show that with a shake or a other indicator
    */

    private void charge(bool active)
    {
        if(active)
        {
            if(barScript.canActivate())
            {
                specialEffect.Play();
                barScript.activate(true,false);
                colScript.setCharging(true);
            }
        }
        else
        {
            specialEffect.Stop();
            barScript.activate(false,false);
            colScript.setCharging(false);
        }
    }

    private void dig(bool active)
    {
        if(active)
        {
            if(canDig && moveScript.getGrounded() && barScript.canActivate())
            {
                barScript.activate(true,false);
                animalObject.transform.parent.parent.gameObject.layer = LayerMask.NameToLayer(playerLayers[1]);
                digging = true;  
                animalObject.SetActive(false);
            }
        }
        else
        {
            barScript.activate(false,false);
            animalObject.transform.parent.parent.gameObject.layer = LayerMask.NameToLayer(playerLayers[0]);
            digging = false;
            animalObject.SetActive(true);
        }
    }

    public void setBarZero()
    {
        barScript.activate(true,true);
    }

    public void filled()//when bar is filled up
    {
        colScript.resetCol();
    }

    public void stopAbil()//when bar is drained fully 
    {
        if(canDig)
        {
            dig(false);
        }
        if(canCharge)
        {
            charge(false);
        }
    }

    public bool getDigging()
    {
        return digging;
    }
}
