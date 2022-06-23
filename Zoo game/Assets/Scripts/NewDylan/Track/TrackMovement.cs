using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMovement : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private UiController uiScript;//in game ui script

    [Header("Settings")]
    [SerializeField] private int distancePerSec = 10;//for the ditance score
    [SerializeField] private int speedMultiplier = 100;//multiplier of the speed of the track
    [SerializeField] private float gainSpeed = 0.01f;//the speed that gets added over time per second
    [SerializeField] private float maxDistanceSpeed = 5; //max speed of the track

    [Header("private data")]
    private float distanceMoved = 0;//current moved distance
    private float currentSpeed = 0;//current movement speed of the track
    private bool stopped = false;//if the track is pauzed for when the player collided with something

    void Update()
    {
        if(!Values.pauzed && !stopped)//if game is not pauzed or the track is stopped
        {
            transform.position += -transform.forward * ((1 + currentSpeed) * speedMultiplier) * Time.deltaTime;//moves the track forwards
            setSpeed();
            setDistance();
        }   
    }   

    public void setSpeed()//increases the speed of the track movement
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

    public void stopTrack(bool active)//stops the track if the player collides with object
    {
        stopped = active;
    }

    public void setDistance()//sets the distance score
    {
        distanceMoved += distancePerSec * (1 + currentSpeed) * Time.deltaTime;
        uiScript.setDistance(distanceMoved);//sets the ui for the score
    }

    public float getDistance()//gets the distance for the end screen and saving the score
    {
        return distanceMoved;
    }
}
