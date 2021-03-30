using System;
using UnityEngine;
using UnityEngine.Animations;

public class WallRunnig : MonoBehaviour
{

    [SerializeField] private LayerMask wall;
    
    private CharacterController _characterController;
    private ScFPSController _scFPSController;
    private bool _isOnWall;

    [HideInInspector] public int zRotation;


    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        
        _scFPSController = GetComponent<ScFPSController>();
    }

    private void Update()
    {

        Ray rayOnRightSide = new Ray(transform.position, transform.right);
        Ray rayOnLeftSide = new Ray(transform.position, -transform.right);
        Physics.Raycast(rayOnRightSide, out var hitR, 10f, wall);
        Physics.Raycast(rayOnLeftSide, out var hitL, 10f, wall);
        if (hitR.distance > 0 && hitR.distance <= 8f && !_characterController.isGrounded && _scFPSController.isRunning &&
            !_isOnWall)
        {
            WallRun(25);
        }
        else if (hitL.distance > 0 && hitL.distance <= 8f && !_characterController.isGrounded && _scFPSController.isRunning &&
                 !_isOnWall)
        {
            WallRun(-25);
        }
        else
        {
            Restore();
        }

        // Restores boolean 
        if (_characterController.isGrounded)
            _isOnWall = false;
    }

    private void WallJump()
    {
        _isOnWall = true;
        _scFPSController.Jump();
    }

    private void WallRun(int side)
    {
        _scFPSController.gravity = 5f;
        zRotation = side;
        _scFPSController.playerCamera.transform.Rotate(new Vector3(0, 0, zRotation));
        if (Input.GetButton("Jump") && _scFPSController.canMove)
        {
            WallJump();
        }
    }

    private void Restore()
    {
        _scFPSController.gravity = 20f;
        zRotation = 0;
        _scFPSController.playerCamera.transform.Rotate(new Vector3(0, 0, zRotation));
    }
}

