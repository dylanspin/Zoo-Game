using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAnimal : MonoBehaviour
{
    [Header("cam settings")]
    [SerializeField] private float sensitivity = 1.5f;
    [SerializeField] private float rotationSpeed = 1.5f;
    [SerializeField] private float maxOffsetAngle = 45f;

    [Header("privat data")]
    private bool isLocked = false;
    private Transform rotationObject;

    private void Start()
    {
        rotationObject = new GameObject("rotObj").transform;
    }

    private void Update()
    {
        if(!isLocked)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime * 500;
            rotationObject.Rotate(Vector3.up * mouseX);
            // float currentRot = transform.eulerAngles.y;
            // rotationObject.eulerAngles = new Vector3(0,Mathf.Clamp(rotationObject.eulerAngles.y, currentRot-maxOffsetAngle, currentRot+maxOffsetAngle),0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotationObject.rotation, rotationSpeed * Time.deltaTime);
        }
    }
   
    public void lockCam(bool active)
    {
        isLocked = active;
    }
}
