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
    [SerializeField] private float rollSpeed = 10;
    [SerializeField] private float jumpRecharge = 0.5f;
    [SerializeField] private float sideSpeed = 10;
    [SerializeField] private float airTimeMultiplier = 8;

    [Header("Private Data")]
    private float jumpHeight = 10;
    private float baseJumpHeight = 0;
    private float gravity = 10;
    private bool isGrounded;
    private bool allowJump = true;
    private bool lockMovement = false;
    private bool boosted = false;
    private int currentLane = 1;
    private float clicks = 0;
    private float airTime = 1;
    private Transform[] allTrackLines = new Transform[5];
    private ParticleSystem groundPs;
    private Animator anim;

    [Header("Swipe Data")]
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
   

    public void setStartData(AnimalPrefab prefavScript,AnimalData newData,Transform[] newTrackLanes)
    {
        groundCheck = prefavScript.groundCheck.transform;
        for(int i=0; i<newTrackLanes.Length; i++)
        {
            allTrackLines[i] = newTrackLanes[i];
        }

        groundPs = prefavScript.movePs;
        anim = prefavScript.anim;

        jumpHeight = newData.jumpHeight;
        baseJumpHeight = jumpHeight;
        allowJump = newData.canJump;

        rbPlayer.mass = newData.animalMass;
        rbPlayer.mass = newData.gravity;
    }

    private void Update()
    {
        getSwipe();
        getKeys();
        if(!Values.pauzed && !lockMovement)
        {
            clicks = Mathf.Lerp(clicks,0,1.0f * Time.deltaTime);
            
            isGrounded = Physics.Raycast(groundCheck.position,-groundCheck.up,groundDistance,groundLayers);
            
            getAirTime();
            checkGroundPs();
            
            Vector3 currentPos = transform.position;
            transform.position = Vector3.Lerp(transform.position, allTrackLines[currentLane].position, sideSpeed * Time.deltaTime);
            currentPos.x = transform.position.x;
            transform.position = currentPos;

            rbPlayer.AddForce(-transform.up * gravity * airTime);
        }
    }
 
    private void getSwipe()
    {
        if(Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
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
            if(Mathf.Abs(Vector2.Distance(secondPressPos,firstPressPos)) < 20)
            {
                checkClicks();
            }
        }
    }

    private void getKeys()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if(currentLane > 0)
            {
                currentLane --;
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if(currentLane < 2)
            {
                currentLane ++;
            }
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            jump();
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            roll();
        }
    }

    private void checkClicks()//checks for double click
    {
        if(!Values.pauzed)
        {
            if(clicks > 0.5f)
            {
                abilityScript.checkSpecial();
                clicks = 0;
            }
            else
            {
                clicks ++;
            }
        }
    }
    
    private void getAirTime()
    {
        if(isGrounded)
        {
            airTime = 1;
        }
        else
        {
            airTime += airTimeMultiplier * Time.deltaTime;
        }
    }


    //collision functions
    public void addKnockBack(float collisionForce,float timeLocked,bool dead)
    {
        lockMovement = true;//locks movement for concussion effect
        groundPs.Stop();
        camScript.lockCam(true);//locks the camera from moving the player rotation (needs to change to maaikes camera follow)
        rbPlayer.AddForce(-transform.forward * collisionForce, ForceMode.Impulse);
        if(!dead)
        {
            Invoke("unlockMovement",timeLocked);
        }
    }

    private void unlockMovement()
    {
        lockMovement = false;
        camScript.lockCam(false);
    }

    //jump functions
    private void jump()
    {
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

    public void setDoubleJump(bool active)
    {
        if(active)
        {
            jumpHeight = baseJumpHeight * 2;
        }
        else
        {
            jumpHeight = baseJumpHeight;
        }
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
