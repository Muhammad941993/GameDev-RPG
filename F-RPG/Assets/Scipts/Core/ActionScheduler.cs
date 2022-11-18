using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
           IAction CurrentAction;
        public void StartAction(IAction action)
        {
            if (CurrentAction == action) return;

            if(CurrentAction != null)
            {
               // print("Cancel cureent Action if not null");
                CurrentAction.Cancel();
               
            }
            CurrentAction = action;
           // print("current action turn to new");
        }


        public void CancelCurrentAction()
        {
            StartAction(null);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("space");
                CancelCurrentAction();
            }
        }
    }
}
