using System.Runtime.InteropServices.ComTypes;
using Bolt;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

namespace Enemy.Behaviors
{
    public class IDLEBehavior: StateMachineBehaviour
    {
        public const float attackRange = 6f;

        private Transform _playerTransform;
        private NavMeshAgent _agent;
        
        
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int Speed = Animator.StringToHash("speed");

        
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            /*Initialization*/ 
            _agent = animator.GetComponent<NavMeshAgent>();
            
            _agent.speed = 0;
            
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            

            // var position = animator.transform.position;
            // var lookVector = _playerTransform.position - position;
            // lookVector.y = position.y;
            // var rot = Quaternion.LookRotation(lookVector);
            // animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, rot, 90);
            
            
            if (Vector2.Distance(animator.transform.position, _playerTransform.position) > attackRange)
            {
                animator.SetBool(Moving, true);
                animator.SetFloat(Speed, 1f);
                
            }
        }
    }
}