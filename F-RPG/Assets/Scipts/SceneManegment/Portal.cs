using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Core;

namespace RPG.SceneManegment
{


    public class Portal : MonoBehaviour
    {
         enum Distination { A,B,C,D}
        [SerializeField] int NextSceneIndex;
        [SerializeField] Transform SpawnPoint;
        [SerializeField] Distination distination;
        
        [SerializeField] float FadeTime = 1f;
        private void Start()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(SceneTranstion());
            }
        }

        IEnumerator SceneTranstion()
        {
            Fade fade = FindObjectOfType<Fade>();

            DontDestroyOnLoad(gameObject);
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
           
            yield return fade.FadeOut(FadeTime);

            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(NextSceneIndex);

             savingWrapper.Load();

             PeristentObjectSpawner.IsSpawned = false;

            Portal OtherPortal = GetOtherPortal();
            UpdatePlayerPosition(OtherPortal);

            savingWrapper.Save();


            yield return new WaitForSeconds(FadeTime);
            yield return fade.FadeIn(FadeTime);

            Destroy(gameObject);

        }

        Portal GetOtherPortal()
        {
            foreach(Portal i in GameObject.FindObjectsOfType<Portal>())
            {
                if (i == this) continue;
                if (i.distination != distination) continue;

                return i;
            }
           return null;
        }

        void UpdatePlayerPosition(Portal other)
        {
            GameObject player =  GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = other.SpawnPoint.position;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}