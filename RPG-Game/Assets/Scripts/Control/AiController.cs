using RPG.Combat;
using RPG.Control;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] float wayPointDwellingTime = 3;
        [SerializeField] PatrolPath patrolPath;
        float wayPointTolerance = 1;
        float timeScienceArraiveWayPoint = float.MaxValue;
        int currentWayPointIndex = 0;
        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;
        Vector3 guardPosition;

        float timeScienceSawPlayer = float.MaxValue;
        ActionScheduler actionScheduler;
        // Start is called before the first frame update
        void Start()
        {
            actionScheduler = GetComponent<ActionScheduler>();
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
                timeScienceSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeScienceSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimer();
        }

        private void UpdateTimer()
        {
            timeScienceSawPlayer += Time.deltaTime;
            timeScienceArraiveWayPoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if(patrolPath != null)
            {
                if (AtWayPoint())
                {
                    CycleWayPoint();
                    timeScienceArraiveWayPoint = 0;
                }

                nextPosition = GetCurrentWayPoints();
            }

            if(timeScienceArraiveWayPoint > wayPointDwellingTime)
            {
                mover.StartMoveAction(nextPosition);
            }
        }

        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position , GetCurrentWayPoints());
            return distanceToWayPoint < wayPointTolerance;
        }

        private void CycleWayPoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }

        private Vector3 GetCurrentWayPoints()
        {
            return patrolPath.GetCurrentWayPoint(currentWayPointIndex);

        }

        private void SuspicionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player.gameObject);
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