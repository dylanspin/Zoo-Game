using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAnimal : MonoBehaviour
{
    [Header("cam settings")]
    [SerializeField] private float sensitivity = 1.5f;
    [SerializeField] private float rotationSpeed = 1.5f;
    [SerializeField] private float maxOffsetAngle = 45f;
    [SerializeField] private float returnAngleSpeed = 0.5f;
    [SerializeField] private float turnSpeed = 0.5f;

    [Header("privat data")]
    private bool isLocked = false;
    private float mouseAngle = 0;

    private void Update()
    {
        if(!Values.pauzed)
        {
            if(!isLocked)
            {
                float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime * 500;

                mouseAngle += mouseX;
                mouseAngle = Mathf.Clamp(mouseAngle,-maxOffsetAngle,maxOffsetAngle);
                mouseAngle = Mathf.Lerp(mouseAngle,0,returnAngleSpeed);
                
                if(mouseAngle > 1)
                {
                    float procentage = ((mouseAngle - 1) * 100) / (maxOffsetAngle - 1) /100;
                    transform.Rotate(Vector3.up * turnSpeed * procentage);
                }
                else if(mouseAngle < -1)
                {
                    float procentage = ((mouseAngle - -1) * 100) / (-maxOffsetAngle - -1)/100;
                    transform.Rotate(Vector3.up * -turnSpeed  * procentage);
                }
            }
        }
    }
   
    public void lockCam(bool active)
    {
        isLocked = active;
    }
}
