using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RGP.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            for (int t = 0; t < transform.childCount; t++)
            {
                Gizmos.DrawSphere(transform.GetChild(t).position, 0.3f);
                Gizmos.DrawLine(GetCurrentWayPoint(t), GetCurrentWayPoint(GetNextIndex(t)));
            }
        }
        public int GetNextIndex(int index)
        {
            int next = index + 1;
            if (next >= transform.childCount) next = 0;
            return next;
        }

        public Vector3 GetCurrentWayPoint(int current)
        {
            return transform.GetChild(current).position;
        }
    }
}