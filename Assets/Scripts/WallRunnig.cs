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
        
        _scFPSController = GetComponent<ScFPSController>();
    }

    private void Update()
    {
        _wallChecker = new Ray(transform.position, transform.right);
        Physics.Raycast(_wallChecker, out var hit, 10f, wall);    
        if (hit.distance > 0 && hit.distance <= 1f && !_characterController.isGrounded)
        {
            _scFPSController.gravity = 0;
        }
        
    }
}

