using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            for (int i = 0; i< transform.childCount; i++)
            {

                Gizmos.DrawSphere(GetCurrentWayPoint(i),0.2f);
               
                Gizmos.DrawLine(GetCurrentWayPoint(i), GetCurrentWayPoint(GetNextIndex(i)));
            }

           
        }

        public int GetNextIndex(int index)
        {
            if (index +1 == transform.childCount)
            {
                return 0;
            }
            return index +1;
        }
        public Vector3 GetCurrentWayPoint(int index)
        {
            return transform.GetChild(index).position;
        }
    }
}