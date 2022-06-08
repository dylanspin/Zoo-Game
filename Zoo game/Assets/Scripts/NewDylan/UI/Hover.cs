using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Hover : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Image imageChange;
    [SerializeField] private string boolName = "Enter";
    [SerializeField] private bool changeColor = false;
    [SerializeField] private Color[] hoverColors = new Color[2];

    public void trigger(bool active)
    {
        anim.SetBool(boolName,active);
        if(changeColor)
        {
            imageChange.color = hoverColors[active ? 1 : 0];
        }
    }

}
