using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour , IAction

    {
        // [SerializeField] Transform targer;
        NavMeshAgent NavMesh;
        [SerializeField] Animator animator;
        ActionScheduler actionScheduler;
        Health health;

        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();
            actionScheduler = GetComponent<ActionScheduler>();
            NavMesh = GetComponent<NavMeshAgent>();
            // NavMesh.destination = targer.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead()) { NavMesh.enabled = false; }
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 navVelocity = NavMesh.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(navVelocity);
            float speed = localVelocity.z;
            animator.SetFloat("Blend", speed);
           
        }

        public void StartMoveAction(Vector3 distenation)
        {
            //  GetComponent<Fighter>().Cancel();
            actionScheduler.StartAction(this);
            MoveTo(distenation);
        }

        public void MoveTo(Vector3 distenation)
        {
            NavMesh.destination = distenation;
            NavMesh.isStopped = false;
        }

        public void Cancel()
        {
          
            NavMesh.isStopped = true;
        }

       
    }
}