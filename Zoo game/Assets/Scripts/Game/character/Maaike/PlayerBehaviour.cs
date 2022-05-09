using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float jumpVelocity = 5f;
   // public float gravity = -10.0f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    
    private Vector3 velocity;

    //private Rigidbody _rb;

    /*void Start()
    {
        _rb = GetComponentRigidbody > ();
    }*/

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f ) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime); 
        }
    }


   /* void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
       // _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        velocity.y = Mathf.Sqrt(jumpVelocity * -2 * gravity);//change this to correct jumpheight
    }*/
}

//velocity.y += gravity * Time.deltaTime;
//controller.Move(velocity * Time.deltaTime);

//velocity.y = Mathf.Sqrt(Height * -2 * gravity);//change this to your jumpheight 