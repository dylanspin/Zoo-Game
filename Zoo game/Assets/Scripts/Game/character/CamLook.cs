using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLook : MonoBehaviour
{
    private bool locked = false;

    public void lockLook(bool active)
    {
        locked = active;
    }
}
