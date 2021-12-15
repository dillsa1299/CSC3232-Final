using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script controls movement of the players model, as well as its graphics and collision physics

    Code in this document adapted from the following source:
    https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=Brackeys
*/

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 12f;

    public float gravity = -1f;

    public Transform groundCheck;
    float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 200f;

    Vector3 velocity;
    bool isGrounded;
    float originalMoveSpeed;

    public bool jumpBoostPowerup = false;
    public bool doubleJump = false;
    int doubleJumpCount = 0;

    void Start()
    {
        originalMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Creates small sphere at feet of player and checks for collision

        if (isGrounded && velocity.y < 0) //Stops velocity from gravity from constantly increasing
        {
            velocity.y = -4f;
            doubleJumpCount = 0;
        }

        if (Input.GetKey("left shift")) //Player sprints whilst holding shift
        {
            moveSpeed = originalMoveSpeed * 2f;
        }
        else
        {
            moveSpeed = originalMoveSpeed;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded) //Jumps if player is on ground
        {
            if(jumpBoostPowerup)
            {
                velocity.y = Mathf.Sqrt(jumpHeight*1.5f * -2f * gravity);
            }
            else
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        if(Input.GetButtonDown("Jump") && !isGrounded && doubleJump) //Double Jump
        {
            if(doubleJumpCount == 0)
            {
                velocity.y = Mathf.Sqrt(jumpHeight*1.5f * -2f * gravity);
                doubleJumpCount +=1;
            }
        }

        velocity.y += gravity; //Always applies gravity
        controller.Move(velocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jump Boost"))
        {
            jumpBoostPowerup = true;
        }
        if (other.gameObject.CompareTag("Double Jump"))
        {
            doubleJump = true;
        }
    }
}
