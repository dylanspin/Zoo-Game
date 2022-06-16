using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidedObject : MonoBehaviour
{
    [SerializeField] private Transform holder;
    [SerializeField] private Animator anim;
    [SerializeField] private float animTrigger = 1.0f;

    public void setObject(Transform newChild,Collision other)
    {
        if(other != null)
        {
            if(other.gameObject.GetComponent<MeshCollider>())
            {
                other.gameObject.GetComponent<MeshCollider>().convex = true;
            }
            else if(other.gameObject.GetComponentInChildren<MeshCollider>())
            {
                other.gameObject.GetComponentInChildren<MeshCollider>().convex = true;
            }
        }
        else if(newChild.GetComponentInChildren<MeshCollider>())
        {
            newChild.GetComponentInChildren<MeshCollider>().convex = true;
        }
        else
        {
            newChild.gameObject.SetActive(false);
        }
        newChild.parent = holder;
        Invoke("activateEnd",animTrigger);
    }

    private void activateEnd()
    {
        anim.SetBool("activate",true);
    }

    public void endAnim()
    {
        Destroy(this.gameObject);
    }
}
