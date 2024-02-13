using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Vector3 fallVelocity;
    public bool isFalling;

    [SerializeField]
    private float gravMultiplier;

    [SerializeField]
    private CharacterController charController;

    [SerializeField]
    private float walkSpeed = 3f;

    [SerializeField]
    private float runSpeed = 5f;

    float moveX, moveY;
    
    // Start is called before the first frame update
    void Start()
    {
        fallVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        HandleGravity();
    }
    private void HandleGravity()
    {
        charController.Move(fallVelocity * Time.deltaTime);
        if(charController.isGrounded)
        {
            isFalling = false;
        }
        else if(!Physics.Raycast(transform.position, -transform.up, 2f))
        {
            isFalling = true;
        }

        if(isFalling)
        {
            fallVelocity += Physics.gravity * Time.deltaTime * gravMultiplier;
        }
        else
        {
            fallVelocity = Vector3.zero;
        }
    }

    private void Jump()
    {
        if(!isFalling)
        {
            isFalling = true;
            fallVelocity = new Vector3(0f,10f,0f);
            charController.transform.Translate(0f,1f,0f);
        }
    }

    private void HandlePlayerMovement()
    {
        //if(GameManager.instance.timeStopped) return;
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        Vector3 moveDir = Camera.main.transform.forward * moveY + Camera.main.transform.right * moveX;

        moveDir.y = 0f;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveDir *= runSpeed;
        }
        else
        {
            moveDir *= walkSpeed;
        }

        charController.Move(moveDir * Time.deltaTime);
    }
}
