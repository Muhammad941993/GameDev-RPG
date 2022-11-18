using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;



namespace RPG.Cinematics
{
    public class CiinematicControllerRemover : MonoBehaviour
    {
        GameObject player;
        PlayableDirector playableDirector;
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playableDirector = GetComponent<PlayableDirector>();

            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
        }
        private void DisableControl(PlayableDirector obj)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }


        private void EnableControl(PlayableDirector obj)
        {
            player.GetComponent<PlayerController>().enabled = true;

        }

      
    }
}
