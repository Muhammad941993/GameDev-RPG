using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Start()
        {
            GetComponent<PlayableDirector>().stopped += EnableControl;
            GetComponent<PlayableDirector>().played += DisableControler;
            player = GameObject.FindGameObjectWithTag("Player");

        }
        public void DisableControler(PlayableDirector pd)
        {
            print("disable");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        public void EnableControl(PlayableDirector pd)
        {
            print("enable");

            player.GetComponent<PlayerController>().enabled = true;

        }
    }
}