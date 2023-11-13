using RPG.Core;
using RPG.Movement;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour , IAction
    {
        [SerializeField] float fightDistance = 2;
        [SerializeField] float timeBetweenAttacks = 1;

        Health target;
        Mover mover;
        Animator animator;
        ActionScheduler actionSheduler;

        float timeScienceLastAttack = float.PositiveInfinity;
        [SerializeField] int damageAmount;

        private void Start()
        {
            mover = GetComponent<Mover>();
            actionSheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            timeScienceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;
          

            if (Vector3.Distance(transform.position, target.transform.position) > fightDistance)
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }
        public bool CanAttack(GameObject target)
        {
            if (target == null) return false;
            Health health = target.GetComponent<Health>();
            return !health.IsDead();
        }
                                    
        private void AttackBehaviour()
        {
            // this will trigger Hit() event
            if (timeScienceLastAttack < timeBetweenAttacks) return;
            TriggerAttack();
            transform.LookAt(target.transform.position);
            timeScienceLastAttack = 0;
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        //Animation Event
        void Hit()
        {
            target?.TakeDamage(damageAmount);
        }

        public void Attack(GameObject combatTarget)
        {
            if (combatTarget == null) return;
            target = combatTarget.GetComponent<Health>();
            actionSheduler.StartAction(this);
        }

        public void Cancel()
        {
            target = null;
            StopAttack();
            mover.Cancel();
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

    }
}
