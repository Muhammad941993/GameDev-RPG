using RPG.Combat;
using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour ,IAction
    {
        NavMeshAgent agent;
        Animator animator;
        Health health;
        ActionSchedular action;

        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();
            action = GetComponent<ActionSchedular>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            agent.enabled = !health.IsDead();
            Vector3 localVector = transform.InverseTransformVector(agent.velocity);
            animator.SetFloat("forwodSpeed", localVector.z);
        }

        public void ActionMoveTo(Vector3 pos)
        {
            action.SetCurrentAction(this);
            MoveTo(pos);
        }
        public void MoveTo(Vector3 pos)
        {
            agent.isStopped = false;

            agent.destination = pos;
        }

      

        public void Cancel()
        {
            agent.isStopped = true;

        }
    }
}