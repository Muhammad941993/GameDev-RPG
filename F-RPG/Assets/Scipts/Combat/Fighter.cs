using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        Health target;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBtweenAttack = 1f;
        [SerializeField] float DamageAmount;
        float timeScinceLastAttack = 0;
         Mover mover;
        ActionScheduler actionScheduler;
        Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            mover = GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            timeScinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (TargetInRange())
            {
                mover.Cancel();
                //Cancel();
                AttackBehaviour();


            }
            else
            {
                mover.MoveTo(target.transform.position);
            }


        }
        void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeScinceLastAttack > timeBtweenAttack)
            {
                anim.ResetTrigger("cancelAttack");
                anim.SetTrigger("attack");
                timeScinceLastAttack = 0;
            }
           
        }
        bool TargetInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.transform.GetComponent<Health>();
          //  Debug.Log("Attack");
        }


        public void Cancel()
        {
            anim.ResetTrigger("attack");
            anim.SetTrigger("cancelAttack");
            target = null;
           
        }

       public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;

            target = combatTarget.transform.GetComponent<Health>();
            return (target != null && !target.IsDead());
        }

        void Hit()
        {
            if(target != null)
            {
                target.TakeDamage(DamageAmount);
            }
            
           // print("Hit enemy");
        }
    }
}