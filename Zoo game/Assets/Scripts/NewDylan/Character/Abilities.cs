using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    // [Header("Settings")]
    // private bool canDig = true;

    [Header("Scripts")]
    [SerializeField] private AnimalMovement moveScript;

    [Header("SetData")]
    [SerializeField] private GameObject animalObject;
    [SerializeField] private string[] playerLayers;

    [Header("private data")]
    private bool digging = false;
    private bool canDig = true;

    public void setStartData(GameObject newAnimalObject,AnimalData newData)
    {
        animalObject = newAnimalObject;
        canDig = newData.canDig;
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
        if(canDig && moveScript.getGrounded())
        {
            if(active)
            {
                animalObject.transform.parent.gameObject.layer = LayerMask.NameToLayer(playerLayers[1]);
            }
            else
            {
                animalObject.transform.parent.gameObject.layer = LayerMask.NameToLayer(playerLayers[0]);
            }

            animalObject.SetActive(!active);//temp needs to be animation that digs down??
            digging = active;   
        }
    }


    public bool getDigging()
    {
        return digging;
    }

}
