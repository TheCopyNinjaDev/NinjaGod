
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _player;
    public LayerMask whatIsGround, whatIsPlayer;
    //Patroling
    public Vector3 walkPoint;
    private bool _walkPointSet;
    public float walkPointRange;
    //Attacking
    public float timeBetweenAttacks;

    private bool _alreadyAttacked = false;
    //States
    public float sightRange, attackRange;
    private bool _playerInSightRange, _playerInAttackRange, _playerIsNoticed;
    private bool _alive = true;
    private Animator _animator;
    private const float FillingSpeed = 1f;
    private SpotIndicator _spotIndicator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        if(GameObject.FindGameObjectWithTag("Player"))
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        _spotIndicator = GetComponent<SpotIndicator>();
    }

    private void Update()
    {
        /*Check if npc is alive*/
        if (_alive)
        {
            ControlSpotSign();
            ResetDestination();
            _playerIsNoticed = _spotIndicator.IsTriggered();
            _playerInSightRange = _spotIndicator.IsSpotted() || _playerInAttackRange;
            _playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (!_playerInSightRange && !_playerInAttackRange) Patroling();
            if (_playerIsNoticed && !_playerInAttackRange) CheckForPlayer();
            if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
            if (_playerInAttackRange && _playerInSightRange) AttackPlayer();
        }
    }

    private bool _posSetted = false;
    private Vector3 _previosPlayersPos = Vector3.zero;

    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Die1 = Animator.StringToHash("Die");

    private void CheckForPlayer()
    {
        if (!_posSetted)
        {
            _previosPlayersPos = _player.position;
            _posSetted = true;
        }
        _agent.SetDestination(_previosPlayersPos);
        _agent.speed = 5;
        
    }
    private void ResetDestination()
    {
        if(_spotIndicator.IsUnnoticed())
            _posSetted = false;
    }
    private void Patroling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet)
            _agent.SetDestination(walkPoint);

        var distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }
    private void SearchWalkPoint()
    {

        //Calculate random point in range
        var randomZ = Random.Range(-walkPointRange, walkPointRange);
        var randomX = Random.Range(-walkPointRange, walkPointRange);

        var position = transform.position;
        walkPoint = new Vector3(position.x + randomX, position.y, position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            _walkPointSet = true;
    }
    private void ChasePlayer()
    {
        GetComponent<FieldOfView>().viewAngle = 360f;
        _agent.SetDestination(_player.position);
        _agent.speed = 10;
    }
    // ReSharper disable Unity.PerformanceAnalysis
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        _agent.SetDestination(transform.position);
        _agent.speed = 0;


        if (!_alreadyAttacked)
        {
            _animator.SetTrigger(Attack);

            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }
    public void Die()
    {
        _alive = false;
        _animator.SetTrigger(Die1);
        Destroy(gameObject, 30);
    }
    // ReSharper disable Unity.PerformanceAnalysis
    private void ControlSpotSign()
    {
        if (GetComponent<FieldOfView>().visibleTargets.Count > 0)
        {
            _spotIndicator.FillTheSign(FillingSpeed);
        }
        else
        {
            _spotIndicator.UnfillTheSign(0);
        }
    }


    
}
