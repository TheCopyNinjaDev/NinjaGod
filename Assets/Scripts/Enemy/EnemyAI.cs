
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
    private bool playerInSightRange, playerInAttackRange;

    private bool alive = true;

    private Animator animator;

    private float fillingSpeed = 0.2f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //Check for sight and attack range
        if (alive)
        {
            if(GetComponent<FieldOfView>().visibleTargets.Count > 0)
            {
                GetComponent<SpotIndicator>().FillTheSign(fillingSpeed);
            }
            playerInSightRange = GetComponent<SpotIndicator>().isSpotted() ? true : false;
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            //if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
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
}
