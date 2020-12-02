using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    bool isRunning = false;
    bool isCrouching = false;



    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        /*bool isRunning = Input.GetKey(KeyCode.LeftShift);*/
        
        if (canMove && !isCrouching)
        {
            Run();
        }


        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        //Press Left Ctrl to crouch
        if (canMove)
        {
            Crouch(characterController.isGrounded);
        }
    }

    void Crouch(bool isGrounded)
    {
        RaycastHit hit;
        Ray rayUP = new Ray(transform.position, Vector3.up);
        Physics.Raycast(rayUP, out hit);
        float timer = 0;
        float timeToWait = 0.002f;
        bool checkingTime = true;
        bool timerDone = false;

        if (Input.GetButtonDown("Crouch") && isGrounded)
        {
            gameObject.GetComponent<CharacterController>().height = 1;
            playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            isRunning = false;
            isCrouching = true;
        }
        else if (!Input.GetButton("Crouch"))
        {
            if (hit.distance == 0 || hit.distance >= 2)
            {



                if (checkingTime)
                {
                    timer += Time.deltaTime;
                    if (timer >= timeToWait)
                    {
                        timerDone = true;
                    }
                }

                if (timerDone)
                {
                    //return from crouching
                    Stand();
                    isCrouching = false;
                }

            }
        }
        else if (Input.GetButtonDown("Crouch")) isRunning = false;

    }

    void Stand()
    {
        gameObject.GetComponent<CharacterController>().height = 2;
        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }

    void Run()
    {
        if (Input.GetButtonDown("Run"))
        {
            isRunning = true;
        }
        else if (Input.GetButtonUp("Run"))
        {
            isRunning = false;
        }
    }
}