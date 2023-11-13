using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int health;
        Animator animator;
        ActionSchedular actionSchedular;
        bool isDead = false;
        private void Start()
        {
            actionSchedular = GetComponent<ActionSchedular>();
            animator = GetComponent<Animator>();
        }
        public bool IsDead() => isDead;
        public void Damage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                isDead = true;
                health = 0;
                animator.SetTrigger("death");
                actionSchedular.CancelCurrentAction();
            }
        }
    }
}