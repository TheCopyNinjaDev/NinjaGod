using UnityEngine;

namespace Enemy.Behaviors
{
    public class CombatBehavior: StateMachineBehaviour
    {
        private Transform _playerTransform;
        private static readonly int InAttackRange = Animator.StringToHash("InAttackRange");

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
            
            if (Vector2.Distance(animator.transform.position, _playerTransform.position) > IDLEBehavior.attackRange)
            {
                animator.SetBool(InAttackRange, false);
            }
        }
    }
}