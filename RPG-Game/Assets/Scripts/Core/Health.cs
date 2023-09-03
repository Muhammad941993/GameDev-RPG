using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100;
        Animator animator;
        bool isDead = false;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public bool IsDead() => isDead;
        public void TakeDamage(float damage)
        {
            if (health <= 0) return;

            health = Mathf.Max(health - damage, 0);
            if(health <= 0)
            {
                animator.SetTrigger("die");
                isDead = true;
                GetComponent<ActionScheduler>().StopCurrentAction();
            }
            print(health);
        }
      
    }
}