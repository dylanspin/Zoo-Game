using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMovement : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private UiController uiScript;

    [Header("Settings")]
    [SerializeField] private int distancePerSec = 10;//for the ditance score
    [SerializeField] private int speedMultiplier = 100;
    [SerializeField] private float gainSpeed = 0.01f;
    [SerializeField] private float maxDistanceSpeed = 5; 

    [Header("private data")]
    private float distanceMoved = 0;
    private float currentSpeed = 0;

    void Update()
    {
        if(!Values.pauzed)
        {
            transform.position += -transform.forward * ((1 + currentSpeed) * speedMultiplier) * Time.deltaTime;
            setSpeed();
            setDistance();
        }   
    }   

    public void setSpeed()
    {
        float newAmount = currentSpeed + gainSpeed * Time.deltaTime;
        if(newAmount < maxDistanceSpeed)
        {
            currentSpeed = newAmount;
        }
        else
        {
            currentSpeed = maxDistanceSpeed;
        }
    }

    public void setDistance()
    {
        distanceMoved += distancePerSec * (1 + currentSpeed) * Time.deltaTime;
        uiScript.setDistance(distanceMoved);
    }

    public float getDistance()
    {
        return distanceMoved;
    }
}
