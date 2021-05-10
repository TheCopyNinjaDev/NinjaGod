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
    private Transform _playerPos;
    private static readonly int Turn = Animator.StringToHash("Turn");


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
           
        
        var dir = _playerPos.position - transform.position;
        // When player is not in attack zone npc need to rotate to him
        if (Vector3.Angle(dir, transform.forward) > 45)
        {
            _agent.speed = 2;
            _animator.SetFloat(Turn, 0.25f);
        }
    }
}
