using System;
using UnityEngine;

public class WallRunnig : MonoBehaviour
{

    [SerializeField] private LayerMask wall;
    
    private CharacterController _characterController;
    private Ray _wallChecker;
    private ScFPSController _scFPSController;
    

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _wallChecker = new Ray(transform.position, Vector3.right);
        _scFPSController = GetComponent<ScFPSController>();
    }

    private void Update()
    {
        Physics.Raycast(_wallChecker, out var hit, 1f, wall);
        if (hit.distance == 0 && !_characterController.isGrounded)
        {
            _scFPSController.gravity = 0;
        }
    }
}

