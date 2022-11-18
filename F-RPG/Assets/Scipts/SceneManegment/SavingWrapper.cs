using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;


namespace RPG.SceneManegment
{
     
    public class SavingWrapper : MonoBehaviour
    {
        const string defultSaveFile = "save";
        private IEnumerator Start()
        {
            Fade fade = FindObjectOfType<Fade>();

            fade.FadeOutFast();
          yield return GetComponent<SavingSystem>().LoadLastScene(defultSaveFile);
            yield return fade.FadeIn();
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
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
            GetComponent<SavingSystem>().Load(defultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defultSaveFile);
        }


        //private float fadeInTime = .5f;

        //private IEnumerator Start()
        //{
        //    Fade fade = FindObjectOfType<Fade>();
        //    fade.FadeOutFast();
        //    yield return GetComponent<SavingSystem>().LoadLastScene(defultSaveFile);
        //    yield return fade.FadeIn(fadeInTime);

        //}
        //// Update is called once per frame
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.L))
        //    {
        //        Load();
        //    }
        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        Save();
        //    }
        //}

        //public void Save()
        //{
        //    GetComponent<SavingSystem>().Save(defultSaveFile);
        //}

        //public void Load()
        //{
        //    GetComponent<SavingSystem>().Load(defultSaveFile);
        //}
    }
   
}
