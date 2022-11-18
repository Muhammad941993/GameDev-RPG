using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float ChaseDistance = 5f;

        GameObject Player;
        Fighter fighter;
        Health health;
        Mover mover;
        Vector3 GurdPosition;
        float timeScinceLastSceen = Mathf.Infinity;
        [SerializeField]float SuspetionTime = 5f;
        [SerializeField] float WayPointTolerance = 1f;
        [SerializeField] PatrolPath patrolPath;
        [Range(0,1)]
        [SerializeField] float PatrolSpeedFraction = 0.2f;
        ActionScheduler actionScheduler;
        int CurrentWayPointIntIndex = 0;
        [SerializeField] float DewillingTime = 2f;
        float TimeScienceArriveToPoint = Mathf.Infinity;
        private void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
            mover = GetComponent<Mover>();
            GurdPosition = transform.position;
            Player = GameObject.FindGameObjectWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead()) return;

            AttckTargetIfAlive();
        }


        bool IfPlayerInAttackRange()
        {
          return Vector3.Distance(transform.position, Player.transform.position) < ChaseDistance ;
        }

        private void AttckTargetIfAlive()
        {
            if (IfPlayerInAttackRange() && fighter.CanAttack(Player))
            {
                AttackBehaviour();
                timeScinceLastSceen = 0;

            }
            else if (timeScinceLastSceen < SuspetionTime)
            {
                SuspetionBehaviour();

            }
            else
            {
                PatrolBehaviour();

            }
            UpDateTime();
        }

        private void UpDateTime()
        {
            timeScinceLastSceen += Time.deltaTime;
            TimeScienceArriveToPoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = GurdPosition;

            if (patrolPath != null)
            {
                if (AtAwayPoint())
                {
                    CycleWayPoint();
                    TimeScienceArriveToPoint = 0;
                }
                nextPosition = GetCurrentWayPoint();
            }

            if(DewillingTime < TimeScienceArriveToPoint)
            {
                mover.StartMoveAction(nextPosition,PatrolSpeedFraction);
            }
           
        }

        private bool AtAwayPoint()
        {
            float x = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return x < WayPointTolerance;
        }

        private void CycleWayPoint()
        {
            CurrentWayPointIntIndex =  patrolPath.GetNextIndex(CurrentWayPointIntIndex);
        }

        private Vector3 GetCurrentWayPoint()
        {
           return patrolPath.GetPatrolPoint(CurrentWayPointIntIndex);
        }

        private void SuspetionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(Player);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, ChaseDistance);
            
        }
    }
}
