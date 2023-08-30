using RPG.Combat;
using RPG.Movement;
using UnityEngine;


namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;
        private void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        void Update()
        {
          if(InteractWithCombat()) return;

           if(InteractWithMovement()) return;

            print("the end");

        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            for (int i = 0; i < hits.Length; i++)
            {
                CombatTarget target = hits[i].transform.GetComponent<CombatTarget>();

                if (target != null)
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        fighter.Attack(target);
                    }
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool move = Physics.Raycast(GetMouseRay(), out hit);
            if (move)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.MoveTo(hit.point);
                    fighter.Cancel();
                }
                return true;
            }

            return false;
        }

      

        Ray GetMouseRay() => Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}