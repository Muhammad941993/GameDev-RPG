using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fight fight;
        Health health;
        void Start()
        {
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            fight = GetComponent<Fight>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead()) return;


            if (CombatInterAction()) return;
            if (MovementInterAction()) return;
        }

        bool MovementInterAction()
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(MousePosionToRay(), out hit))
                {
                    mover.ActionMoveTo(hit.point);
                    return true;
                }
            }
            return false;
        }

        bool CombatInterAction()
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit[] hits = Physics.RaycastAll(MousePosionToRay(), 30f);
                {
                    for (int i = 0; i < hits.Length; i++)
                    {
                        var target = hits[i].collider.GetComponent<CombatTarget>();
                        if(target ==  null) continue;

                        if(fight.CanAttack(target.gameObject))
                        if (target != null)
                        {
                            fight.Attack(target.gameObject);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        Ray MousePosionToRay() => Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}