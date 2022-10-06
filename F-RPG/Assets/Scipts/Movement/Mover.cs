using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
   // [SerializeField] Transform targer;
    NavMeshAgent NavMesh;
    [SerializeField] Animator animator;
   


    // Start is called before the first frame update
    void Start()
    {
        NavMesh = GetComponent<NavMeshAgent>();
       // NavMesh.destination = targer.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToMouse();
        }
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        Vector3 navVelocity = NavMesh.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(navVelocity);
        float speed = localVelocity.z;
        animator.SetFloat("Blend", speed);
    }

    void MoveToMouse()
    {
        Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        bool hashit =   Physics.Raycast(Ray, out Hit);

        if (hashit)
            NavMesh.destination = Hit.point;
    }
}
