using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{

    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject PeristentObjectPrefabe;

        public static bool IsSpawned;
        private void Awake()
        {
            if (IsSpawned) return;
            SpwanPeristentObject();
            IsSpawned = true;
        }

        private  void SpwanPeristentObject()
        {
           GameObject PeristentObject = Instantiate(PeristentObjectPrefabe);
            DontDestroyOnLoad(PeristentObject);
        }

      
    }
}