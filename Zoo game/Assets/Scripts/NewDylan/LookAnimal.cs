using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAnimal : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1.5f;

    private void Start()
    {
        
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime * 500;
        // transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
