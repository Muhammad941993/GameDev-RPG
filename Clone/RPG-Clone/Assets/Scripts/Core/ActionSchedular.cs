using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSchedular : MonoBehaviour
{
    IAction action;

    public void SetCurrentAction(IAction _action)
    {
        if (action == _action) return;
        if(action != null)
        {
            action?.Cancel();
        }
        action = _action;
    }

    public void CancelCurrentAction()
    {
        SetCurrentAction(null);
    }
}
