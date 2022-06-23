using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkSpace : MonoBehaviour
{
    //checks if it should show the space bar in the instructions
    public void checkShowSpace()
    {
        gameObject.SetActive(Values.selectedAnimal.showSpace);
    }
}
