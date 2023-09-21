using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 gravity;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    public float mouseSensitivy = 5.0f;
    private float jumpHeight = 1f;
    private float gravityValue = -9.81f;
    private CharacterController controller;
    private float walkSpeed = 5;
    private float runSpeed = 8; 
    private float speed = 3.0F;
    private int extraJump;
    public int extraJumpValue;

    
 
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        extraJump = extraJumpValue;
    }

    public void Update()
    {
       UpdateRotation();
       ProcessMovement();
       Moving();
    }
    void UpdateRotation()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X")* mouseSensitivy, 0, Space.Self);
 
    }

    void Moving(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 verticalMovement = transform.forward * verticalInput;
        Vector3 horizontalMovement = transform.right * horizontalInput;

        transform.position = transform.position + (verticalMovement + horizontalMovement)* speed * Time.deltaTime;
        transform.Rotate(new Vector3(0, mouseX, 0));
    }

    void ProcessMovement()
    { 
        // Moving the character forward according to the speed
        //float speed = GetMovementSpeed();
 
        // Get the camera's forward and right vectors
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
 
        // Make sure to flatten the vectors so that they don't contain any vertical component
        cameraForward.y = 0;
        cameraRight.y = 0;
 
        // Normalize the vectors to ensure consistent speed in all directions
        cameraForward.Normalize();
        cameraRight.Normalize();
 
        // Calculate the movement direction based on input and camera orientation
        Vector3 moveDirection = (cameraForward * Input.GetAxis("Vertical")) + (cameraRight * Input.GetAxis("Horizontal"));
 
        // Apply the movement direction and speed
        Vector3 movement = moveDirection.normalized * speed * Time.deltaTime;
 
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            // if (Input.GetButtonDown("Jump"))
            // {
            //     gravity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            // }
            // else
            // {
            //     // Dont apply gravity if grounded and not jumping
            //     gravity.y = -1.0f;
            // }
            extraJump = extraJumpValue;
            if (Input.GetButtonDown("Jump")){
            gravity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }else{
                gravity.y = -1.0f;
            }
        }
        else if (Input.GetButtonDown("Jump") && extraJump > 0){
                gravity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                extraJump--;
            }
        else
        {
            // Since there is no physics applied on character controller we have this applies to reapply gravity
            gravity.y += gravityValue * Time.deltaTime;
        }
        // Apply gravity and move the character
        playerVelocity = gravity * Time.deltaTime + movement;
        controller.Move(playerVelocity);
    }

    
}