using RGP.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            var dic = LoadFile(saveFile);
            CaptureState(dic);
            SaveFile(saveFile,dic);
        }

        public void Load(string saveFile)
        {
            var dic = LoadFile(saveFile);
            RestoreState(dic);
        }

        private void SaveFile(string saveFile, object captureObject)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("save to :: " + path);

            using (FileStream fileStream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, captureObject);
            }
        }
        private void CaptureState(Dictionary<string, object> dict)
        {
            foreach (SaveableEntity saveableEntity in FindObjectsOfType<SaveableEntity>())
            {
                dict[saveableEntity.GetUniqueIdentifier()] = saveableEntity.CaptureState();
            }
            dict["lastSceenIndex"] = SceneManager.GetActiveScene().buildIndex;
        }
        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string v = GetPathFromSaveFile(saveFile);
            if (!File.Exists(v))
            {
                return new Dictionary<string, object>();
            }
            using (FileStream fileStream = File.Open(v, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                return (Dictionary<string, object>)formatter.Deserialize(fileStream);
            }
        }

        public void RestoreState(Dictionary<string, object> _object)
        {
            foreach (SaveableEntity saveableEntity in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveableEntity.GetUniqueIdentifier();
                if (_object.ContainsKey(id))
                {
                    saveableEntity.RestoreState(_object[saveableEntity.GetUniqueIdentifier()]);
                    print(saveableEntity.GetUniqueIdentifier());
                }
            }
        }
        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }

        internal IEnumerator LoadLastScene(string saveFile)
        {
            var dic =  LoadFile(saveFile);
            if (dic.ContainsKey("lastSceenIndex"))
            {
                int lastScene = (int)dic["lastSceenIndex"];

                if (lastScene != SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync(lastScene);
                }
            }
            RestoreState(dic);
        }
    }
}