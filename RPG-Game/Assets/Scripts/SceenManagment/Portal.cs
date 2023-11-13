using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.SceneManagment
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A,B, C,D,E
        }
        [SerializeField] int SceenNumber = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 3;
        [SerializeField] float fadeInTime = 1;
        private void OnTriggerEnter(Collider other)
        {

            if (other.transform.CompareTag("Player"))
            {
                StartCoroutine(Transtion());
            }
        }

        IEnumerator Transtion()
        {
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper saving = FindObjectOfType<SavingWrapper>();


            yield return fader.FadeOut(fadeOutTime);
            saving.Save();

            yield return SceneManager.LoadSceneAsync(SceenNumber);
            saving.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            saving.Save();

            yield return new WaitForSeconds(0.7f);
            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);

        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
            gameObject.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            //gameObject.transform.position = otherPortal.spawnPoint.position;
            gameObject.transform.rotation = otherPortal.spawnPoint.rotation;
            //gameObject.GetComponent<NavMeshAgent>().enabled = true;

        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (destination != portal.destination) continue;

                return portal;
            }
            return null;
        }
    }
}