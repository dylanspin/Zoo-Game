using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float groundDistance;

    [Header("Set Data")]
    [SerializeField] private Rigidbody rbPlayer;//player ridgetbody
    [SerializeField] private LookAnimal camScript;//player ridgetbody
    [SerializeField] private AnimalStats statsScript;//player ridgetbody
    [SerializeField] private CamShake shakeScript;//camera shake/camera 

    [Header("Abilities settings")]
    [SerializeField] private bool canbreak = false;
    [SerializeField] private bool canDig = false;
    [SerializeField] private string[] playerLayers;
    [SerializeField] private GameObject animalObject;//is temp later needs like a dive animation going in to the ground

    [Header("Movements settings")]
    [SerializeField] private float normalSpeed = 10;
    [SerializeField] private float boostSpeed = 15;
    [SerializeField] private float minBoost = 15;
    [SerializeField] private float boostStaminaDrain = 15;
    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float jumpRecharge = 0.5f;

    [Header("Collision settings")]
    [SerializeField] private float collisionForce = 100;
    [SerializeField] private float unlockTime = 0.5f;

    [Header("Private Data")]
    private float currentSpeed = 5;
    private Vector3 movementVelocity; //movement velocity
    private Vector3 impact = Vector3.zero;//extra velocity for adding forces on to the player
    private bool isGrounded;
    private bool allowJump = true;
    private bool lockMovement = false;
    private bool digging = false;
    private bool boosted = false;



    private void Start()
    {
        currentSpeed = normalSpeed;
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

        if(Input.GetKeyDown(KeyCode.R))//needs to be mobile input later
        {
            dig(!digging);
        }

        fakeForce();//sets impact for 
        
        float x = Input.GetAxis("Horizontal");
        if(lockMovement)
        {
            movementVelocity = new Vector3(0, 0f, 0);
        }
        else
        {
            movementVelocity = new Vector3(x, 0f, 1);
        }
        Vector3 move = transform.TransformDirection(movementVelocity) * currentSpeed;

        rbPlayer.velocity = new Vector3(move.x ,rbPlayer.velocity.y, move.z) + (impact);
    }

    //collision functions
    private void OnCollisionEnter(Collision other) 
    {
        collide(other);//checks collision
    }

    private void collide(Collision other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Ground"))//when hitting something else thats not the ground
        {
            if(!canbreak)
            {
                if(!lockMovement)
                {
                    addKnockBack(other);
                }
            }
            else
            {
                //needs to check what interaction needs to happen with object and if its breakable else also add knockback
                breakObject(other);
            }
            StartCoroutine(shakeScript.Shake(0.25f,0.05f));
        }
    }

    private void addKnockBack(Collision other)
    {
        lockMovement = true;//locks movement for concussion effect
        camScript.lockCam(true);//locks the camera from moving the player rotation (needs to change to maaikes camera follow)
        Vector3 dir = other.contacts[0].point - transform.position;
        dir = -dir.normalized;
        AddImpact(dir,collisionForce);//adds force in the opesite direction of the collision
        Invoke("unlockMovement",unlockTime);
    }

    private void breakObject(Collision other)
    {
       
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
        if(isGrounded && allowJump && !digging)
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
                currentSpeed = boostSpeed;
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
        if(Input.GetKeyDown(KeyCode.LeftShift))//needs to be mobile input later
        {
            boost(true);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))//needs to be mobile input later
        {
            boost(false);
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

    //digging ability
    
    /*
        Still needs delay 
        Animation
        ground effect
        Digging above ground trail effect
        When going up check if there is something in the way if so cant go up
        If cant go up show that with a shake or a other indicator
    */
    private void dig(bool active)
    {
        if(canDig && isGrounded)
        {
            if(active)
            {
                gameObject.layer = LayerMask.NameToLayer(playerLayers[1]);
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer(playerLayers[0]);
            }
            animalObject.SetActive(!active);
            digging = active;   
        }
    }
}
