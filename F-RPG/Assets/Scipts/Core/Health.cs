using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;



namespace RPG.Core
{
    public class Health : MonoBehaviour , ISaveable
    {
        [SerializeField] float health = 100;
        bool death;

        public bool IsDead() { return death; }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print(health);
            if(health == 0 && !death)
            {
                DeathBehaviour();
            }
        }

        public void DeathBehaviour()
        {
            death = true;
            GetComponent<Animator>().SetTrigger("death");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
            if (health == 0 && !death)
            {
                DeathBehaviour();
            }
        }
    }
}