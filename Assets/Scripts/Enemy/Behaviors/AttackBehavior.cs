using UnityEngine;

namespace Enemy.Behaviors
{
    public class AttackBehavior: StateMachineBehaviour
    {
        private int _number;
        private Transform _playerTransform;
                
        
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            /*Initialization*/ 
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }
        
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var position = animator.rootPosition;
            var lookVector = _playerTransform.position - position;
            lookVector.y = position.y;
            var rot = Quaternion.LookRotation(lookVector);
            animator.rootRotation = Quaternion.RotateTowards(animator.transform.rotation, rot, 90);
        }
        
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(IsAttacking, false);
        }
    }
}