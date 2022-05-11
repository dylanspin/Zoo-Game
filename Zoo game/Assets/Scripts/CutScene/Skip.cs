using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Skip : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI fastAndNormalText;

    public void FastAndNormal ()
    {
        if (Time.timeScale ==1f)
        {
            Time.timeScale =2f;
            fastAndNormalText.text = "Normal";
        }

        else
        {
            Time.timeScale = 1f;
            fastAndNormalText.text = "Fast";
        }
    }
    
}
