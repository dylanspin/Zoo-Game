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
    // private Transform rotationObject;
    private float mouseAngle = 0;

    private void Start()
    {
        // rotationObject = new GameObject("rotObj").transform;
        // rotationObject.rotation = transform.rotation;
    }   

    ///old code commented sorry its a bit of mess now but maybe i want to reuse some of it later..

    private void Update()
    {
        if(!isLocked)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime * 500;

            mouseAngle += mouseX;
            mouseAngle = Mathf.Clamp(mouseAngle,-maxOffsetAngle,maxOffsetAngle);
            mouseAngle = Mathf.Lerp(mouseAngle,0,returnAngleSpeed);
            
            //can be shorter
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

            // rotationObject.Rotate(Vector3.up * mouseX);
            // rotationObject.localEulerAngles = new Vector3(0,Mathf.Clamp(rotationObject.localEulerAngles.y, transform.eulerAngles.y-45, transform.eulerAngles.y+45),0);
            // float currentRot = transform.eulerAngles.y;
            // rotationObject.eulerAngles = new Vector3(0,Mathf.Clamp(rotationObject.eulerAngles.y, currentRot-maxOffsetAngle, currentRot+maxOffsetAngle),0);
            // transform.rotation = Quaternion.Lerp(transform.rotation, rotationObject.rotation, rotationSpeed * Time.deltaTime);

        }
    }
   
    public void lockCam(bool active)
    {
        isLocked = active;
    }
}
