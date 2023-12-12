using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform RhandTransform = null;
        [SerializeField] Transform LhandTransform = null;
        [SerializeField] Weapon deafultWeapon=null;
        Health target;
        Mover mover;
        Animator animator;
        ActionScheduler actionSheduler;
        [SerializeField] float timeBetweenAttacks = 1;
        float timeScienceLastAttack = float.PositiveInfinity;

        Weapon currentWeapon;
        private void Awake()
        {
            mover = GetComponent<Mover>();
            actionSheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }
        private void Start()
        {
            if(currentWeapon == null)
            {
             SpawnEquippedWeapon(deafultWeapon);
            }
        }
        private void Update()
        {
            timeScienceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;
          

            if (Vector3.Distance(transform.position, target.transform.position) > currentWeapon.GetFightDistance())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }
       
        public void SpawnEquippedWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            currentWeapon.Spawn(RhandTransform, LhandTransform, animator);
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
            StartHit();
        }

        //Animation Event
        void Shoot()
        {
            StartHit();
        }

        void StartHit()
        {
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(RhandTransform,LhandTransform,target);
            }
            else
            {
                target?.TakeDamage(currentWeapon.GetDamageAmount());
            }
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

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
          SpawnEquippedWeapon(Resources.Load<Weapon>((string)state));
        }
    }
}
