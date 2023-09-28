using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public Vector3 gravity;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    public bool canDoubleJump = false;
    public float mouseSensitivy = 5.0f;
    private float jumpHeight = 1f;
    private float gravityValue = -9.81f;
    private CharacterController controller;
    private float walkSpeed = 5;
    private float runSpeed = 8; 
    private float speed = 3.0F;
    private int extraJump;
    public int extraJumpValue;
    public int pointsValue;
    public string points;
    Animator animator;
    public Text scoreText;

    
 
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        extraJump = extraJumpValue;
        animator = GetComponent<Animator>();
        pointsValue = 0;
        points = "" + pointsValue;

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
 
        Vector3 movement;
        // Apply the movement direction and speed
        if (Input.GetKey(KeyCode.LeftShift)){
            movement = moveDirection.normalized * runSpeed * Time.deltaTime;
            animator.SetFloat("Speed", 1.0f);
        }else{
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ){
                movement = moveDirection.normalized * walkSpeed * Time.deltaTime;
                animator.SetFloat("Speed", 0.5F);
            }else{
                movement = moveDirection.normalized * walkSpeed * Time.deltaTime;
                animator.SetFloat("Speed", 0f);
            }
        }
 
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
            animator.SetBool("isGrounded", true);
            animator.SetBool("doubleJump", false);
            extraJump = extraJumpValue;
            if (Input.GetButton("Jump")){
                
                gravity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                animator.SetBool("isGrounded", false);
            }else{
                gravity.y = -1.0f;
            }
        }
        else if (Input.GetButtonDown("Jump") && canDoubleJump){
                gravity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                //extraJump--;
                animator.SetBool("isGrounded", false);
                animator.SetBool("doubleJump", true);
                canDoubleJump = false;
                transform.position += new Vector3(0, 25, 0);
                
            }
        else
        {
            // Since there is no physics applied on character controller we have this applies to reapply gravity
            gravity.y += gravityValue * Time.deltaTime;
            //animator.SetBool("doubleJump", false);
            
        }
        // Apply gravity and move the character
        playerVelocity = gravity * Time.deltaTime + movement;
        
        controller.Move(playerVelocity);
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("You collided");
       if (other.tag == "DoubleJump"){
            canDoubleJump = true;
            Destroy(other.gameObject);
       }
       if (other.tag == "Point"){
            //Debug.Log("Got a Point");
            pointsValue += 10;
            points = "" + pointsValue;
            scoreText.text = points;
            Destroy(other.gameObject);
       }
    }

    
}
