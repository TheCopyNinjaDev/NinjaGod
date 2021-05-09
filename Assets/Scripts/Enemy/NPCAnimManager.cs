using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimManager : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _animator;
    private NavMeshAgent _agent;
    
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int Speed = Animator.StringToHash("speed");

    
    private void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update() 
    {
        if (_agent.speed == 2)
        {
            _animator.SetBool(Moving, true);
            _animator.SetFloat(Speed, 1);
        }
        else if(_agent.speed == 5)
        {
            _animator.SetBool(Moving, true);
            _animator.SetFloat(Speed, 2);
        }
        else
        {
            _animator.SetBool(Moving, false);
            _animator.SetFloat(Speed, 0);
        }
            
    }
}
