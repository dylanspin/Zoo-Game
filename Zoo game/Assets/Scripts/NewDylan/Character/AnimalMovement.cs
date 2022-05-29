using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private float normalSpeed = 10;
    [SerializeField] private float runSpeed = 15;
    [SerializeField] private float minBoost = 15;
    [SerializeField] private float boostStaminaDrain = 15;
    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float jumpRecharge = 0.5f;

    [Header("Private Data")]
    private float currentSpeed = 5;
    private Vector3 movementVelocity; //movement velocity
    private Vector3 impact = Vector3.zero;//extra velocity for adding forces on to the player
    private bool isGrounded;
    private bool allowJump = true;
    private bool canRun = true;
    private bool lockMovement = false;
    private bool boosted = false;

    public void setStartData(GameObject newCheck,AnimalData newData)
    {
        groundCheck = newCheck.transform;

        canRun = newData.canRun;
        allowJump = newData.canJump;

        currentSpeed = newData.speed;
        normalSpeed = newData.speed;
        runSpeed = newData.runSpeed;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(groundCheck.position,-groundCheck.up,groundDistance,groundLayers);
        // Debug.DrawRay(groundCheck.position, -groundCheck.up * groundDistance, Color.red);//draws line to check the ground distance check

        if(Input.GetKeyDown(KeyCode.Space))//needs to be mobile input later
        {
            jump();
        }   

        checkBoost();

        fakeForce();//sets impact for 
        
        float x = Input.GetAxis("Horizontal");
        if(lockMovement)
        {
            movementVelocity = new Vector3(0, 0f, 0);
        }
        else
        {
            movementVelocity = new Vector3(x * 0.75f, 0f, 1);
        }
        Vector3 move = transform.TransformDirection(movementVelocity) * currentSpeed;

        rbPlayer.velocity = new Vector3(move.x ,rbPlayer.velocity.y, move.z) + (impact);
    }

    //collision functions

    public void addKnockBack(Collision other,float collisionForce,float timeLocked)
    {
        Debug.Log("added");
        lockMovement = true;//locks movement for concussion effect
        camScript.lockCam(true);//locks the camera from moving the player rotation (needs to change to maaikes camera follow)
       
        Vector3 dir = other.contacts[0].point - transform.position;
        dir = -dir.normalized;
        AddImpact(dir,collisionForce);//adds force in the opesite direction of the collision
        
        Invoke("unlockMovement",timeLocked);
    }

    private void unlockMovement()
    {
        lockMovement = false;
        camScript.lockCam(false);
    }

    ///force functions
    private void fakeForce()
    {
        if (impact.magnitude > 0.2F) 
        {
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }
    }
    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        dir.y = 0;
        impact += dir.normalized * force / rbPlayer.mass;
    }

    //jump functions
    private void jump()
    {
        if(isGrounded && allowJump && !abilityScript.getDigging())
        {
            isGrounded = false;
            allowJump = false;
            Invoke("rechargeJump",jumpRecharge);
            rbPlayer.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
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
