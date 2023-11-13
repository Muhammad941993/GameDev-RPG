using RGP.Control;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Control
{
    public class AiController : MonoBehaviour
    {
        GameObject target;
        Fight fight;
        Mover mover;
        Health health;
        ActionSchedular action;

        [SerializeField] PatrolPath patrolPath;

        [SerializeField] int ChaseDistance = 5;
        float toleranceDistance = 0.5f;
        [SerializeField] Vector3 guardPosition;
        float timeScinceLastSeenPlayer=0;
        float suspetionTime = 5;
        float DewellingTime = 2;
        int pathIndexPosition = 0;
        float timeScienceArriveWayPoint = 0;

        // Start is called before the first frame update
        void Start()
        {
            timeScienceArriveWayPoint = DewellingTime;
            timeScinceLastSeenPlayer = suspetionTime;
            action = GetComponent<ActionSchedular>();
            guardPosition = transform.position;
            mover = GetComponent<Mover>();
            target = GameObject.FindGameObjectWithTag("Player");
            fight = GetComponent<Fight>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRange() && fight.CanAttack(target))
            {
                timeScinceLastSeenPlayer = 0;
                fight.Attack(target.gameObject);
            }
            else if (timeScinceLastSeenPlayer < suspetionTime)
            {
                SuspetionBehavioer();
            }
            else
            {
                PatrolBehaviour();

            }
            timeScinceLastSeenPlayer += Time.deltaTime;
            timeScienceArriveWayPoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            var nextPostion = guardPosition;

            if (patrolPath != null)
            {
                if (AtPosition())
                {
                    timeScienceArriveWayPoint = 0;
                    CycleWayPoint();
                }

                nextPostion = GetCurrentWayPoint();
            }
            if (timeScienceArriveWayPoint > DewellingTime)
            {
                mover.ActionMoveTo(nextPostion);

            }

        }

        private bool AtPosition()
        {
            return Vector3.Distance(transform.position, GetCurrentWayPoint()) < toleranceDistance;

        }

        private void CycleWayPoint()
        {
            pathIndexPosition = patrolPath.GetNextIndex(pathIndexPosition);
        }

        private Vector3 GetCurrentWayPoint()
        {
           return patrolPath.GetCurrentWayPoint(pathIndexPosition);
        }

        private void SuspetionBehavioer()
        {
            action.CancelCurrentAction();
        }

        
        bool InAttackRange()
        {
            return (Vector3.Distance(transform.position, target.transform.position) < ChaseDistance && fight.CanAttack(target));
        }
    }
}