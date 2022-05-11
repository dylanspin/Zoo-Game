using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLook : MonoBehaviour
{
    /*
        How it is going to work i think
        rotation point on the animal parent object
        that rotates the camera around from that point. When moving forward it rotates the animal towards the angle the camera is looking?
        Or it could instantly rotate when looking but could look bad. 
    */

    private bool locked = false;

    public void lockLook(bool active)
    {
        locked = active;
    }
}
