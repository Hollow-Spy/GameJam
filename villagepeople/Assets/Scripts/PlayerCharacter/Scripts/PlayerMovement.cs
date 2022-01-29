using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

     float speed = 12;

    Vector3 velocity;

    public float gravity = -9.81f;

    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] float jumpheight;
    [SerializeField] float walkspeed;
   [SerializeField] float sprintSpeed;
   public bool is_walking;
    public bool isGrounded;
    public bool is_running;

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(x > 0 || z > 0)
        {
            is_walking = true;
        }
        else
        {
            is_walking = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            is_running = true;
            speed = sprintSpeed;
        }
     if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            is_running = false;
            speed = walkspeed;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        /*if(Input.GetButtonDown("Jump") && isGrounded )
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2 * gravity);

        }
        */

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity);

    }
}
