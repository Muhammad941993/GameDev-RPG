using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


namespace RPG.Cinematics
{
    public class Cinematics : MonoBehaviour
    {
        bool isTriggerd;
        private void OnTriggerEnter(Collider other)
        {
            if(!isTriggerd && other.CompareTag("Player"))
            {
                GetComponent<PlayableDirector>().Play();
                isTriggerd = true;
               
            }
            
        }
    }
}