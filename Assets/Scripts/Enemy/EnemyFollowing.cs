using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowing : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameObject _player;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        _agent.SetDestination(_player.transform.position);
    }
}
