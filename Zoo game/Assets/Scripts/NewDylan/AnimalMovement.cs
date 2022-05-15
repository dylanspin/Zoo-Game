using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float groundDistance;
    [SerializeField] private float jumpRecharge = 0.5f;

    [Header("Set Data")]
    [SerializeField] private Rigidbody rbPlayer;//player ridgetbody
    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float jumpHeight = 10;

    [Header("Private Data")]
    private float currentSpeed = 5;
    private Vector3 movementVelocity; //movement velocity
    private bool isGrounded;
    private bool allowJump = true;

    private void Start()
    {
        currentSpeed = movementSpeed;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(groundCheck.position,-groundCheck.up,groundDistance,groundLayers);
        Debug.DrawRay(groundCheck.position, -groundCheck.up * groundDistance, Color.red);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        
        float x = Input.GetAxis("Horizontal");

        movementVelocity = new Vector3(x, 0f, 1);

        Vector3 move = transform.TransformDirection(movementVelocity) * currentSpeed;

        rbPlayer.velocity = new Vector3(move.x ,rbPlayer.velocity.y, move.z);
    }

    private void jump()
    {
        if(isGrounded && allowJump)
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
}
