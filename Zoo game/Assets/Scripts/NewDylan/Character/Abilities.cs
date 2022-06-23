using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{

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
    private bool digging = false;//is digging
    private bool canDig = false;//can dig under objects
    private bool canCharge = false;//cam charge trough object
    private bool canRockJump = false;//can jump over rocks

    public void setStartData(AnimalPrefab prefabScript,AnimalData newData,AbilityBar newBarScript)//sets start data
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
        checkAbilities();//check passive ability
    }

    private void checkAbilities()//check passive ability
    {
        if(canRockJump)//checks for if a rock is infront if so increase jump height
        {
            Vector3 drawPos = new Vector3(transform.position.x,transform.position.y+5,transform.position.z);//positon of box cast
            bool rockFront = (Physics.BoxCastAll(drawPos, Vector3.one, transform.forward, Quaternion.Euler(0,0,0), 80, rockLayer).Length > 0);//cast a raycast box infront of the player to check if there is a rock
            moveScript.setDoubleJump(rockFront);//sets the jump height based on if there is a rock infront of the player
            barScript.showStatus(rockFront);//shows ability icon if there is a rock
        }
    }

    public void checkSpecial()
    {
        if(canDig)//if the animal can dig
        {
            dig(!digging);
        }
        if(canCharge)//if the animal can charge
        {
            charge(true);
        }
    }

    private void charge(bool active)//sets charging ability
    {
        if(active)
        {
            if(barScript.canActivate())//checks if the bar is full
            {
                if(specialEffect)
                {   
                    specialEffect.Play();//special ability effect
                }
                barScript.activate(true,false);//drains the bar
                colScript.setCharging(true);//activates the ignore collisions bool in the collision script
            }
        }
        else
        {
            if(specialEffect)
            {   
                specialEffect.Stop();//special ability effect
            }
            barScript.activate(false,false);//lets the bar fill up again
            colScript.setCharging(false);//disables the ignore collisions bool in the collision script
        }
    }

    private void dig(bool active)
    {
        if(active)
        {
            if(canDig && moveScript.getGrounded() && barScript.canActivate())
            {
                barScript.activate(true,false);//drains the bar
                animalObject.transform.parent.parent.gameObject.layer = LayerMask.NameToLayer(playerLayers[1]);//alows the player collision to ignore all other collisions except the ground
                digging = true;  
                animalObject.SetActive(false);//disables the player mesh object should be a animation digging
            }
        }
        else
        {
            barScript.activate(false,false);//allows the bar to fill up again
            animalObject.transform.parent.parent.gameObject.layer = LayerMask.NameToLayer(playerLayers[0]);//sets the layer back to the normal player layer
            digging = false;
            animalObject.SetActive(true);//activates the player mesh object should be a animation digging
        }
    }

    public void setBarZero()//drains bar to 0 then lets it fill again
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
            dig(false);//stops digging because the bar is drained
        }
        if(canCharge)
        {
            charge(false);//stops charging because the bar is drained
        }
    }

    public bool getDigging()//sends digging status
    {
        return digging;
    }
}
