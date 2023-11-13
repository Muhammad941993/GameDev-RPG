using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fight : MonoBehaviour, IAction
    {
        [SerializeField]Health target;
        Mover mover;
        ActionSchedular action;
        Animator animator;
        Health health;
        [SerializeField] int AttackDamage;
        [SerializeField] float fightDistance = 3;
        [SerializeField] float timeBetweenAttack = 1;
        float timeCounter = 0;
        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            action = GetComponent<ActionSchedular>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            timeCounter += Time.deltaTime;
            if (health.IsDead()) return;
            if (target == null) return;
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

        private void AttackBehaviour()
        {
            if (timeCounter > timeBetweenAttack && !target.IsDead())
            {
                timeCounter = 0;
                animator.SetTrigger("attack");
            }
        }
        void Hit()
        {
            print("hit player");
            target.Damage(AttackDamage);

        }
        public bool CanAttack(GameObject _target)
        {
            if(_target == null) return false;
            var tar = _target.GetComponent<Health>();
            return !tar.IsDead();
        }
        public void Attack(GameObject _target)
        {
            if (_target == null) return;
            target = _target.GetComponent<Health>();

            action.SetCurrentAction(this);
            //print("Attack");
        }

        public void Cancel()
        {
            //print(gameObject.name+"Cancel"+target?.name);
            target = null;
        }
    }
}