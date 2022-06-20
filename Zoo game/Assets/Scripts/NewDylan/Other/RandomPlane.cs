using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlane : MonoBehaviour
{
    [SerializeField] Animator anim;
  
    private void Start()
    {
        Invoke("activateAnim",Random.Range(30,300));
    }

    private void activateAnim()
    {
        anim.SetBool("Start",true);
    }
}
