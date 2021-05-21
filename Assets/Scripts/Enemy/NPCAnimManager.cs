using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimManager : MonoBehaviour
{
    
    private Animator _animator;
    private NavMeshAgent _agent;
    
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int Speed = Animator.StringToHash("speed");
    private Transform _playerPos;
    
    


    private void Awake() 
    {
        _playerPos = GameObject.FindWithTag("Player").transform;
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
    
    /*Debug zone*/
    public LayerMask whatIsGround;
    private void OnDrawGizmos()
    {
        _playerPos = GameObject.FindWithTag("Player").transform;
        var origin = _playerPos.position;
        RaycastHit hit = default;
        if (Physics.Raycast(origin, Vector3.down, out hit, 2f, whatIsGround))
        {
            origin = new Vector3(origin.x, hit.transform.position.y, origin.z);    
        }
        else
        {
            origin = new Vector3(origin.x, 0, origin.z);
        }
        
        Handles.color = Color.red;
        Handles.DrawWireDisc(origin, Vector3.up, 6);
    }
}
