using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool triggerOnce = false;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            if(triggerOnce) { return; }

            triggerOnce = true;
            GetComponent<PlayableDirector>().Play();
        }
    }
}