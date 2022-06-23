using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hover : MonoBehaviour
{
    [SerializeField] private Animator anim;//the animator of the button
    [SerializeField] private Image imageChange;//the image where to change the color of 
    [SerializeField] private string boolName = "Enter";//the bool in the animator
    [SerializeField] private bool changeColor = false;//if hover should change the color of the button
    [SerializeField] private Color[] hoverColors = new Color[2];//the hover colors

    public void trigger(bool active)//when mouse over or exit
    {
        anim.SetBool(boolName,active);
        if(changeColor)
        {
            imageChange.color = hoverColors[active ? 1 : 0];
        }
    }
}
