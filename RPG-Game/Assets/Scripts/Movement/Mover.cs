using RGP.Saving;
using RPG.Combat;
using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour ,IAction ,ISaveable
    {
        NavMeshAgent agent;
        Animator animator;
        ActionScheduler actionSheduler;
        Health health;
        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionSheduler = GetComponent<ActionScheduler>();
        }

        // Update is called once per frame
        void Update()
        {
            agent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformVector(velocity);

            animator.SetFloat("forwordSpeed", localVelocity.z);
        }
        public void StartMoveAction(Vector3 destination)
        {
            actionSheduler.StartAction(this);
            MoveTo(destination);
        }
        public void MoveTo(Vector3 destination)
        {
            agent.destination = destination;
            agent.isStopped = false;
        }

       
        public void Cancel()
        {
            agent.isStopped = true;

        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);

        }

        public void RestoreState(object state)
        {
            GetComponent<NavMeshAgent>().enabled = false;
            SerializableVector3 x = (SerializableVector3)state;
            transform.position = x.ToVector();
            print(x.ToVector() + state.GetHashCode().ToString());
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}