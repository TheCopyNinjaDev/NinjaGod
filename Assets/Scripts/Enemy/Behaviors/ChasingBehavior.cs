using System.Collections;
using Bolt;
using Ludiq;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Behaviors
{
    public class ChasingBehavior: StateMachineBehaviour
    {
        private NavMeshAgent _agent;
        private Transform _playerTransform;
        
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int Speed = Animator.StringToHash("speed");
        private static readonly int InAttackRange = Animator.StringToHash("InAttackRange");
        private static readonly int Turn = Animator.StringToHash("Turn");


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            /*Initialization*/
            _playerTransform = GameObject.FindWithTag("Player").transform;
            _agent = animator.GetComponent<NavMeshAgent>();

            _agent.speed = 5;
            _agent.SetDestination(_playerTransform.position);

            
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;

            var transform = animator.transform;
            var vectorToNextPosition = _agent.steeringTarget - transform.position;
            
            // Turning while Running
            if (Vector2.Dot(transform.forward, vectorToNextPosition) > 0)
            {
                animator.SetFloat(Turn, -1);
            }
            else if(Vector2.Dot(transform.forward, vectorToNextPosition) < 0)
            {
                animator.SetFloat(Turn, 1);
            }
            else
            {
                animator.SetFloat(Turn, 0);
            }
            
            
            
            
            // Checking distance to the player
            if (Vector2.Distance(animator.transform.position, _playerTransform.position) <= IDLEBehavior.attackRange && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                animator.SetBool(Moving, false);
                animator.SetFloat(Speed, 0);
                animator.SetBool(InAttackRange, true);
            }
        }
    }
}