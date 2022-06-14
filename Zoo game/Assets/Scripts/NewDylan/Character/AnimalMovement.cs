using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalMovement : MonoBehaviour
{
    [Header("Ground Check")]
    private Transform groundCheck;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float groundDistance;

    [Header("Set Data")]
    [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private LookAnimal camScript;
    [SerializeField] private AnimalStats statsScript;
    [SerializeField] private Abilities abilityScript;

    [Header("Movements settings")]
    private float normalSpeed = 10;
    private float runSpeed = 15;
    private float minBoost = 15;
    private float boostStaminaDrain = 15;
    private float jumpHeight = 10;
    [SerializeField] private float rollSpeed = 10;
    [SerializeField] private float jumpRecharge = 0.5f;
    [SerializeField] private float sideSpeed = 10;
    [SerializeField] private float gravity = 10;

    [Header("Private Data")]
    private float currentSpeed = 5;
    private Vector3 impact = Vector3.zero;//extra velocity for adding forces on to the player
    private bool isGrounded;
    private bool allowJump = true;
    private bool canRun = true;
    private bool lockMovement = false;
    private bool boosted = false;
    private int currentLane = 1;
    private float clicks = 0;
    private Transform[] allTrackLines = new Transform[5];
    private ParticleSystem groundPs;

    [Header("Swipe Data")]
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    public void setStartData(GameObject newCheck,AnimalData newData,Transform[] newTrackLanes,ParticleSystem newPs)
    {
        groundCheck = newCheck.transform;
        for(int i=0; i<newTrackLanes.Length; i++)
        {
            allTrackLines[i] = newTrackLanes[i];
        }

        groundPs = newPs;
        canRun = newData.canRun;

        jumpHeight = newData.jumpHeight;
        allowJump = newData.canJump;

        currentSpeed = newData.speed;
        normalSpeed = newData.speed;
        runSpeed = newData.runSpeed;

        rbPlayer.mass = newData.animalMass;
    }

    private void Update()
    {
        getSwipe();
        getKeys();
        if(!Values.pauzed && !lockMovement)
        {
            clicks = Mathf.Lerp(clicks,0,1.0f * Time.deltaTime);
            
            isGrounded = Physics.Raycast(groundCheck.position,-groundCheck.up,groundDistance,groundLayers);
           
            checkGroundPs();
            checkBoost();
            
            Vector3 currentPos = transform.position;
            transform.position = Vector3.Lerp(transform.position, allTrackLines[currentLane].position, sideSpeed * Time.deltaTime);
            currentPos.x = transform.position.x;
            transform.position = currentPos;

            rbPlayer.AddForce(-transform.up * gravity);
        }
    }
 
    private void getSwipe()
    {
        if(Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            checkClicks();
        }

        if(Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();
    
            if (!(secondPressPos == firstPressPos))
            {
                if (Mathf.Abs(currentSwipe.x) > Mathf.Abs(currentSwipe.y))
                {
                    if (currentSwipe.x < 0)//left
                    {
                        if(currentLane > 0)
                        {
                            currentLane --;
                        }
                    }
                    else//right
                    {
                        if(currentLane < 2)
                        {
                            currentLane ++;
                        }
                    }
                }
                else
                {
                    if (currentSwipe.y < 0)//down
                    {
                        roll();
                    }
                    else//up
                    {
                        jump();
                    }
                }
                clicks = 0;
            }
        }
    }

    private void getKeys()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentLane > 0)
            {
                currentLane --;
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentLane < 2)
            {
                currentLane ++;
            }
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump();
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            roll();
        }
    }

    private void checkClicks()//checks for double click
    {
        if(clicks > 0.5f)
        {
            //do special
            clicks = 0;
        }
        else
        {
            clicks ++;
        }
    }
    

    //collision functions
    public void addKnockBack(float collisionForce,float timeLocked)
    {
        lockMovement = true;//locks movement for concussion effect
        groundPs.Stop();
        camScript.lockCam(true);//locks the camera from moving the player rotation (needs to change to maaikes camera follow)
        rbPlayer.AddForce(-transform.forward * collisionForce, ForceMode.Impulse);
        // Invoke("unlockMovement",timeLocked);
    }

    private void unlockMovement()
    {
        lockMovement = false;
        camScript.lockCam(false);
    }

    //jump functions
    private void jump()
    {
        Debug.Log("Try jump");
        if(isGrounded && allowJump && !abilityScript.getDigging())
        {
            isGrounded = false;
            allowJump = false;
            Invoke("rechargeJump",jumpRecharge);
            rbPlayer.AddForce(transform.up * jumpHeight * 2, ForceMode.Impulse);
        }
    }

    private void roll()
    {
        if(!isGrounded && !abilityScript.getDigging())
        {
            rbPlayer.AddForce(-transform.up * rollSpeed, ForceMode.Impulse);
        }
    }

    private void rechargeJump()
    {
        allowJump = true;
    }

    //boost functions
    private void boost(bool active)
    {
        if(active)
        {
            if(statsScript.getStam() > minBoost)
            {
                currentSpeed = runSpeed;
            }
        }
        else
        {
            currentSpeed = normalSpeed;
        }

        boosted = active;
    }

    private void checkGroundPs()
    {
        if(isGrounded)
        {
            groundPs.Play();
        }       
        else
        {
            groundPs.Stop();
        }
    }

    private void checkBoost()
    {   
        if(canRun)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift) && !abilityScript.getDigging())//needs to be mobile input later
            {
                boost(true);
            }
            if(Input.GetKeyUp(KeyCode.LeftShift) || abilityScript.getDigging())//needs to be mobile input later
            {
                boost(false);
            }
        }

        if(boosted)
        {
            statsScript.lose(boostStaminaDrain * Time.deltaTime);
            if(statsScript.getStam() < boostStaminaDrain)
            {
                boost(false);
            }
        }
    }

    public bool getGrounded()
    {
        return isGrounded;
    }

    public bool getLocked()
    {
        return lockMovement;
    }

    public Transform getGroundPos()
    {
        return groundCheck;
    }
}
