using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickUp : MonoBehaviour
    {
        [SerializeField]Weapon weapon;
        [SerializeField] float RespawnTime = 5;
      
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<Fighter>().SpawnEquippedWeapon(weapon);
                //Destroy(gameObject);
              StartCoroutine(RespawnPickUp(RespawnTime));
            }
        }




        IEnumerator RespawnPickUp(float afterSeconds)
        {
            ShowPickUp(false);
            yield return new WaitForSeconds(afterSeconds);
            ShowPickUp(true);
        }


        void ShowPickUp(bool shouldShow)
        {
            GetComponent<SphereCollider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }
    }
}