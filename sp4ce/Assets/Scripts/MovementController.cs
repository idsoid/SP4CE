using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private float maxStamina=100;
    private float stamina;

    bool canRun;


    float moveX, moveY;
    
    // Start is called before the first frame update
    void Start()
    {
        fallVelocity = Vector3.zero;
        canRun = true;
        stamina = maxStamina;
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

        UIManager.instance.SetStaminaLength(stamina / maxStamina);
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

    [SerializeField]
    float staminaAlpha = 0f;
    private void HandlePlayerMovement()
    {
        //if(GameManager.instance.timeStopped) return;
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        Vector3 moveDir = Camera.main.transform.forward * moveY + Camera.main.transform.right * moveX;

        moveDir.y = 0f;

        if(Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            if(stamina > 0f)
            {
                
                moveDir *= runSpeed;
                if(moveDir != Vector3.zero)
                {
                    stamina-=Time.deltaTime*20f;
                    staminaAlpha += Time.deltaTime * 5f;
                    if(staminaAlpha > 1f) staminaAlpha = 1f;
                    UIManager.instance.SetStaminaAlpha(staminaAlpha);
                }
                else
                {
                    stamina += Time.deltaTime * 5f;
                }
            }
            else
            {
                canRun = false;
            }
        }
        else
        {
            
            stamina += Time.deltaTime * 15f;
            if(stamina > maxStamina)
            {
                stamina = maxStamina;
                canRun = true;
            }
            moveDir *= walkSpeed;

            if(canRun)
            {
                staminaAlpha -= Time.deltaTime * 5f;
                if(staminaAlpha < 0f) staminaAlpha = 0f;
                UIManager.instance.SetStaminaAlpha(staminaAlpha);
            }
            else
            {
                staminaAlpha += Time.deltaTime;
                if(staminaAlpha > 0.3f)
                {
                    UIManager.instance.SetStaminaAlpha(1f);
                    if(staminaAlpha > 0.6f)
                    {
                        staminaAlpha = 0f;
                    }
                }
                else
                {
                    UIManager.instance.SetStaminaAlpha(0f);
                }
                
            }
        }

        charController.Move(moveDir * Time.deltaTime);

        
    }
}
