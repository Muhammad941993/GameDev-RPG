using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        Transform target;
        Mover mover;
        float fightDistance = 2;
        private void Start()
        {
            mover = GetComponent<Mover>();
        }
        private void Update()
        {
            if(target != null)
            {

                if (Vector3.Distance(transform.position , target.position) > fightDistance)
                {
                    mover.MoveTo(target.position);

                }
                else
                {
                   // target = null;
                    mover.Stop();
                }
            }
        }
        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            print("Attack");
        }
        public void Cancel()
        {
            target = null;
        }
    }
}
