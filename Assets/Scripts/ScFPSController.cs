using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class ScFPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public Camera armCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public ParticleSystem runEffects;
    public float initialFov = 60f;
    public float runFov = 65f;
    public Animator rightHandAnimator;

    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationX;
    private bool _isCrouching;
    private Vector3 _forward = Vector3.zero;
    private Vector3 _right = Vector3.zero;





    [HideInInspector]
    public bool canMove = true;
    [HideInInspector]
    public Vector3 characterVelocity;
    [HideInInspector]
    public bool isRunning;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        
         

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        _forward = transform.TransformDirection(Vector3.forward);
        _right = transform.TransformDirection(Vector3.right);
        

        //Prevents running while crouching
        if (Input.GetButtonDown("Crouch"))
        {
            isRunning = false;
            runEffects.Stop();
        }



        rightHandAnimator.SetBool("isRunning", isRunning);


        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = _moveDirection.y;
        _moveDirection = (_forward * curSpeedX) + (_right * curSpeedY);

        // Press Left Shift to run
        if (canMove && !_isCrouching)
        {
            Run();
        }

        if (Input.GetButton("Jump") && canMove && _characterController.isGrounded)
        {
            Jump();
        }
        else
        {
            _moveDirection.y = movementDirectionY;
        }


        characterVelocity.x = isRunning ? runningSpeed : walkingSpeed;

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        _characterController.Move(_moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            _rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -lookXLimit, lookXLimit);
            if (TryGetComponent<WallRunnig>(out WallRunnig got))
            {
                playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, got.zRotation);
            }
            else
            {
                playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, transform.rotation.z);
            }
            
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        //Press Left Ctrl to crouch
        if (canMove)
        {
            Crouch(_characterController.isGrounded);
        }
    }

    void Crouch(bool isGrounded)
    {
        Ray rayUp = new Ray(transform.position, Vector3.up);
        Physics.Raycast(rayUp, out var hit);
        float timer = 0;
        const float timeToWait = 0.002f;
        bool timerDone = false;

        if (Input.GetButtonDown("Crouch") && isGrounded)
        {
            _characterController.height = 1;
            var position = transform.position;
            playerCamera.transform.position = new Vector3(position.x, position.y, position.z);
            isRunning = false;
            _isCrouching = true;
        }
        else if (!Input.GetButton("Crouch"))
        {
            if (hit.distance != 0 && !(hit.distance >= 4)) return;
            timer += Time.deltaTime;
            if (timer >= timeToWait)
            {
                timerDone = true;
            }

            if (timerDone)
            {
                //returns from crouching
                StandUp();
                _isCrouching = false;
            }
        }

    }

    void StandUp()
    {
        _characterController.height = 2;
        var position = transform.position;
        playerCamera.transform.position = new Vector3(position.x, position.y + 1, position.z);
    }

    void Run()
    {
        if (Input.GetButtonDown("Run") && _moveDirection.normalized == _forward)
        {
            isRunning = true;
            runEffects.Play();
            playerCamera.fieldOfView = runFov;
            armCamera.fieldOfView = runFov;

        }
        else if (Input.GetButtonUp("Run") || _moveDirection.normalized != _forward || _isCrouching)
        {
            isRunning = false;
            runEffects.Stop();
            playerCamera.fieldOfView = initialFov;
            armCamera.fieldOfView = runFov;
        }
    }

    public void Jump()
    {
        _moveDirection.y = jumpSpeed;
    }
}