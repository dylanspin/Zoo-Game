using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAnimal : MonoBehaviour
{
    [Header("cam settings")]
    [SerializeField] private float sensitivity = 1.5f;

    [Header("privat data")]
    private bool isLocked = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(!isLocked)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime * 500;
            // transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }

    public void lockCam(bool active)
    {
        isLocked = active;
    }
}
