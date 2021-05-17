using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimManager : MonoBehaviour
{
    public bool isRotating;
    
    private Rigidbody _rb;
    private Animator _animator;
    private NavMeshAgent _agent;
    
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int Speed = Animator.StringToHash("speed");
    private Transform _playerPos;


    private void Awake() 
    {
        _playerPos = GameObject.FindWithTag("Player").transform;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update() 
    {
        if (_agent.speed == 2)
        {
            _animator.SetBool(Moving, true);
            _animator.SetFloat(Speed, 0.5f);
        }
        else if(_agent.speed == 5)
        {
            _animator.SetBool(Moving, true);
            _animator.SetFloat(Speed, 1);
        }
        else
        {
            _animator.SetBool(Moving, false);
            _animator.SetFloat(Speed, 0);
        }
    }
}
