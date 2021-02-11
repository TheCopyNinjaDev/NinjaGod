using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowing : MonoBehaviour
{
    //private Animator animator;
    private NavMeshAgent agent;
    //private List<string> animBooleans = new List<string>(new string[] { "Moving" });


    public GameObject player;
    public float attackDistance;

    private void Start()
    {
        //animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //player = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        if (Input.GetKey("p"))
        {
            //animator.SetTrigger("AttackTrigger");
        }

        /*Vector3 lookDirection = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(lookDirection);
        if(Vector3.Distance(transform.position, player.transform.position) > 6)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }*/
        agent.SetDestination(player.transform.position);


    }
}
