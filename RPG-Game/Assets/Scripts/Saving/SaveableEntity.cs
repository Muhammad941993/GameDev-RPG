using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RGP.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        static Dictionary<string , SaveableEntity> globalLookUp = new Dictionary<string , SaveableEntity>();

        [SerializeField] string uniqueIdentifier = "";
      
        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            Dictionary<string,object> result = new Dictionary<string,object>();
            foreach (var saveable in GetComponents<ISaveable>())
            {
                result[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return result;
        }

        public void RestoreState(object state)
        {
           Dictionary<string , object> staticstate = (Dictionary<string, object>)state;
            foreach (var saveable in GetComponents<ISaveable>())
            {
                if (staticstate.ContainsKey(saveable.GetType().ToString()))
                {
                    saveable.RestoreState(staticstate[saveable.GetType().ToString()]);
                }
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.isPlaying) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);

            var prop = serializedObject.FindProperty("uniqueIdentifier");

            if (string.IsNullOrEmpty(prop.stringValue) || !IsUnique(prop.stringValue))
            {
                prop.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLookUp[prop.stringValue] = this;
        }
#endif


        private bool IsUnique(string candidate)
        {
            if(!globalLookUp.ContainsKey(candidate)) return true;

            if (globalLookUp[candidate] == this) return true;

            if (globalLookUp[candidate] == null)
            {
                globalLookUp.Remove(candidate);
                return true;
            }

            if (globalLookUp[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookUp.Remove(candidate);
                return true;
            }

            return false;
        }
    }
}