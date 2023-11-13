using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour ,ISaveable
    {
        [SerializeField] float health = 100;
        bool isDead = false;
       

        public bool IsDead() => isDead;
        public void TakeDamage(float damage)
        {
            if (health <= 0) return;
            health = Mathf.Max(health - damage, 0);
            Die();
        }
        void Die()
        {
            if (health <= 0)
            {
               GetComponent<Animator>().SetTrigger("die");

                isDead = true;
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
            print(health);
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
            Die();
        }
    }
}