using RPG.Combat;
using RPG.Control;
using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;
        Vector3 guardPosition;
        // Start is called before the first frame update
        void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindGameObjectWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardPosition = transform.position;
        }
        // Update is called once per frame
        void Update()
        {
            if (health.IsDead()) return;
            
            if (DistanceToPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player.gameObject);

            }
            else
            {
                mover.StartMoveAction(guardPosition);
            }
        }

        private bool DistanceToPlayer()
        {
           float distance =  Vector3.Distance(transform.position, player.transform.position);
            return distance < chaseDistance;
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}