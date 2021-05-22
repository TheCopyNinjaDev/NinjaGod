using UnityEngine;

namespace Enemy.Behaviors
{
    public class AttackSelectorBehavior: StateMachineBehaviour
    {
        private static readonly int AttackNumber = Animator.StringToHash("AttackNumber");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetInteger(AttackNumber, Random.Range(0, 3));
            animator.SetTrigger(Attack);
            animator.SetBool(IsAttacking, true);
        }
    }
}