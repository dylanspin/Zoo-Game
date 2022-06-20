using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkSpace : MonoBehaviour
{
    public void checkShowSpace()
    {
        gameObject.SetActive(Values.selectedAnimal.showSpace);
    }
}
