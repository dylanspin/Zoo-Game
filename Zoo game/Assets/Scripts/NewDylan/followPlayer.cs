using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 0.1f;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        transform.position += offset;
    }

    void Update()
    {
        if(target)
        {
            Vector3 followPos = Vector3.Lerp(transform.position, target.position, followSpeed);
            transform.position = offset + followPos;
        }
    }
}
