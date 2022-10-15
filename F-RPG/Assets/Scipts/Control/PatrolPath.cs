using UnityEngine;


namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                int x = GetNextIndex(i);

                Gizmos.DrawSphere(transform.GetChild(i).transform.position, .3f);

                Gizmos.DrawLine(GetPatrolPoint(i), GetPatrolPoint(x));

            }
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount) return 0;

            return i + 1;
        }
        public Vector3 GetPatrolPoint(int i)
        {
            return transform.GetChild(i).position;
        }

    }
}