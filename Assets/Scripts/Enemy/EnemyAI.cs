
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    //Patroling
    public Vector3 walkPoint;
    private bool _walkPointSet;
    public float walkPointRange;
    //Attacking
    public float timeBetweenAttacks;

    private bool _alreadyAttacked;
    //States
    public float sightRange, attackRange;
    private bool _playerInSightRange, _playerInAttackRange, _playerIsNoticed;
    private bool alive = true;
    private Animator animator;
    private float fillingSpeed = 1f;
    private SpotIndicator spotIndicator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spotIndicator = GetComponent<SpotIndicator>();
    }

    private void Update()
    {
        /*Check if npc is alive*/
        if (alive)
        {
            ControlSpotSign();
            ResetDestination();
            _playerIsNoticed = spotIndicator.IsTriggered();
            _playerInSightRange = spotIndicator.IsSpotted() || _playerInAttackRange;
            _playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (!_playerInSightRange && !_playerInAttackRange) Patroling();
            if (_playerIsNoticed && !_playerInAttackRange) CheckForPlayer();
            if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
            if (_playerInAttackRange && _playerInSightRange) AttackPlayer();
        }
    }

    private bool _posSetted = false;
    private Vector3 _previosPlayersPos = Vector3.zero;
    private void CheckForPlayer()
    {
        if (!_posSetted)
        {
            _previosPlayersPos = player.position;
            _posSetted = true;
        }
        agent.SetDestination(_previosPlayersPos);
        agent.speed = 5;
        animator.SetBool("Moving", true);
        animator.SetFloat("speed", 1);
    }
    private void ResetDestination()
    {
        if(spotIndicator.isUnnoticed())
            _posSetted = false;
    }
    private void Patroling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        animator.SetFloat("speed", 1);
        animator.SetBool("Moving", true);

        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            _walkPointSet = true;
    }
    private void ChasePlayer()
    {
        GetComponent<FieldOfView>().viewAngle = 360f;
        agent.SetDestination(player.position);
        agent.speed = 10;
        animator.SetBool("Moving", true);
        animator.SetFloat("speed", 2);
    }
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        animator.SetBool("Moving", false);


        if (!_alreadyAttacked)
        {
            ///Attack code here
            animator.SetTrigger("Attack");
            ///End of attack code

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
        alive = false;
        animator.SetTrigger("Die");
        Destroy(gameObject, 30);
    }
    private void ControlSpotSign()
    {
        if (GetComponent<FieldOfView>().visibleTargets.Count > 0)
        {
            spotIndicator.FillTheSign(fillingSpeed);
        }
        else
        {
            spotIndicator.UnfillTheSign(0);
        }
    }
}
