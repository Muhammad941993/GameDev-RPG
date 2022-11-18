using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour , IAction ,ISaveable

    {
        // [SerializeField] Transform targer;
        NavMeshAgent NavMesh;
        [SerializeField] Animator animator;
        ActionScheduler actionScheduler;
        Health health;
        float MaxSpeed = 6f;

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

        public void StartMoveAction(Vector3 distenation ,float speedFraction)
        {
           
            actionScheduler.StartAction(this);
            MoveTo(distenation , speedFraction);
        }

        public void MoveTo(Vector3 distenation , float speedFraction)
        {
            NavMesh.destination = distenation;
            NavMesh.speed = MaxSpeed * Mathf.Clamp01(speedFraction);
            
            NavMesh.isStopped = false;
        }

        public void Cancel()
        {
          //  print("Cancel Move");
            NavMesh.isStopped = true;
        }

        [System.Serializable]
        struct MemorySaveData
        {
            public SerializableVector3 Position;
            public SerializableVector3 Rotation;
        }
        public object CaptureState()
        {
            MemorySaveData data = new MemorySaveData();
            data.Position = new SerializableVector3(transform.position);
            data.Rotation = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            MemorySaveData vector3 =(MemorySaveData) state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = vector3.Position.ToVector();
            transform.eulerAngles = vector3.Rotation.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;

        }
    }
}