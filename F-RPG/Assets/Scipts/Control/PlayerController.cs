using RPG.Combat;
using RPG.Movement;
using UnityEngine;
using RPG.Core;


namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        Mover mover;
        Fighter fighter;
        Health health;

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead()) return;

            if (InteractWithCombat()) { return; }

            if (InteractWithMovement()) { return; }

        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.collider.GetComponent<CombatTarget>();

                if (target == null) continue;

                if (!fighter.CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    fighter.Attack(target.gameObject);
                    print("combat");

                }
                return true;

            }
            return false;
        }

        private bool InteractWithMovement()
        {

            RaycastHit Hit;
            bool hashit = Physics.Raycast(GetMouseRay(), out Hit);

            if (hashit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(Hit.point);
                    print("move");
                }

                return true;
            }
            return false;
        }
        Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}