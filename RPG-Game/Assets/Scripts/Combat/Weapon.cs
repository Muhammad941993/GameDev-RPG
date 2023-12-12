using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    [CreateAssetMenu(fileName ="weapon",menuName ="Weapon/New Weapon",order =0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] int damageAmount =1;
        [SerializeField] float fightDistance = 2;
        [SerializeField] GameObject EquippedPrefab=null;
        [SerializeField] AnimatorOverrideController animatorOverrideController = null;
        [SerializeField] bool IsRightHand = false;
        [SerializeField] GameObject projectile = null;
        const string weaponName = "weapon";
        public void Spawn(Transform RHtransform, Transform LHtransform, Animator animator)
        {
            DestroyOldWeapon(RHtransform, LHtransform);

            Transform htransform = GetWeaponTransform(RHtransform, LHtransform);

            if (EquippedPrefab != null)
            {
               var w = Instantiate(EquippedPrefab, htransform);
                w.name = weaponName;
            }

            var animatorOvarride = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverrideController != null)
            {
                animator.runtimeAnimatorController = animatorOverrideController;
            }else if (animatorOvarride != null)
            {
                animator.runtimeAnimatorController = animatorOvarride.runtimeAnimatorController;
            }

        }

        void DestroyOldWeapon(Transform RHtransform, Transform LHtransform)
        {
            Transform weapon = RHtransform.Find(weaponName);
            if (weapon == null)
            {
                weapon = LHtransform.Find(weaponName);
            }

            if (weapon == null) return;

            weapon.name = "old";
            Destroy(weapon.gameObject);
        }

        private Transform GetWeaponTransform(Transform RHtransform, Transform LHtransform)
        {
            Transform htransform = null;
            if (IsRightHand) { htransform = RHtransform; }
            else { htransform = LHtransform; }
            return htransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform Rhand ,  Transform LHhand , Health target)
        {
            Transform htransform = GetWeaponTransform (Rhand , LHhand);
            var x = Instantiate(projectile, htransform.position,Quaternion.identity);
            x.GetComponent<Projectile>().SetTarget(target,damageAmount);
        }
        public float GetFightDistance() => fightDistance;
        public float GetDamageAmount() => damageAmount;
       
    }
}