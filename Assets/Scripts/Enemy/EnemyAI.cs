
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    //States
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange, playerIsNoticed;
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
            playerIsNoticed = spotIndicator.IsTriggered();
            playerInSightRange = spotIndicator.IsSpotted() || playerInAttackRange;
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerIsNoticed && !playerInAttackRange) CheckForPlayer();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
    }

    bool posSetted = false;
    Vector3 previosPlayersPos = Vector3.zero;
    private void CheckForPlayer()
    {
        if (!posSetted)
        {
            previosPlayersPos = player.position;
            posSetted = true;
        }
        agent.SetDestination(previosPlayersPos);
        agent.speed = 5;
        animator.SetBool("Moving", true);
        animator.SetFloat("speed", 1);
    }
    private void ResetDestination()
    {
        if(spotIndicator.isUnnoticed())
            posSetted = false;
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
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
            walkPointSet = true;
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


        if (!alreadyAttacked)
        {
            ///Attack code here
            animator.SetTrigger("Attack");
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
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
