using RPG.Saving;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagment
{
    public class SavingWrapper : MonoBehaviour
    {
        const string saveFile = "save";
        SavingSystem savingSystem;

        private IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            savingSystem =   GetComponent<SavingSystem>();
            yield return savingSystem.LoadLastScene(saveFile);
            yield return fader.FadeIn(.2f);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Load()
        {
            savingSystem.Load(saveFile);
        }

        public void Save()
        {
            savingSystem.Save(saveFile);
        }
    }
}