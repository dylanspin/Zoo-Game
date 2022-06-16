using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    // [Header("Settings")]
    // private bool canDig = true;

    [Header("Scripts")]
    [SerializeField] private AnimalMovement moveScript;
    [SerializeField] private AbilityBar barScript;

    [Header("SetData")]
    [SerializeField] private GameObject animalObject;
    [SerializeField] private string[] playerLayers;

    [Header("private data")]
    private bool digging = false;
    private bool canDig = true;

    public void setStartData(GameObject newAnimalObject,AnimalData newData,AbilityBar newBarScript)
    {
        animalObject = newAnimalObject;
        canDig = newData.canDig;
        barScript = newBarScript;
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            dig(!digging);
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

    private void dig(bool active)
    {
        if(active)
        {
            if(canDig && moveScript.getGrounded() && barScript.canActivate())
            {
                barScript.activate(true);
                animalObject.transform.parent.parent.gameObject.layer = LayerMask.NameToLayer(playerLayers[1]);
                digging = true;  
                animalObject.SetActive(false);
            }
        }
        else
        {
            barScript.activate(false);
            animalObject.transform.parent.parent.gameObject.layer = LayerMask.NameToLayer(playerLayers[0]);
            digging = false;
            animalObject.SetActive(true);
        }
    }

    public bool getDigging()
    {
        return digging;
    }

    public void stopAbil()
    {
        if(canDig)
        {
            dig(false);
        }
    }

}
