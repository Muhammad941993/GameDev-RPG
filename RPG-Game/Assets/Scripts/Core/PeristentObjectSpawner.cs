using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject peristentObjectPrefabe;

        static bool hasSpawned;

        private void Awake()
        {
            if (hasSpawned) return;
            SpawnPersistentData();
            hasSpawned = true;
           
        }

        void SpawnPersistentData()
        {
            GameObject _gameObject = Instantiate(peristentObjectPrefabe);
            DontDestroyOnLoad(_gameObject);

        }
    }
}