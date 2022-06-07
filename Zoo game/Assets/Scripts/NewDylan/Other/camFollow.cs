using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour
{
    [Header("settings")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    public void setStartData(AnimalData newAnimal)
    {
        offset = newAnimal.camOffset;
    }

    private void FixedUpdate()
    {
        positionLerping();
        rotationLerping();
    }

    private void positionLerping()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }

    private void rotationLerping()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction,Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
