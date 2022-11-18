using System.Collections;
using UnityEngine;


namespace RPG.SceneManegment
{
    public class Fade : MonoBehaviour
    {

        CanvasGroup CanvesGroup;
       

        private void Awake()
        {
            CanvesGroup = GetComponent<CanvasGroup>();
            //StartCoroutine(FadeInOut());
        }
       
        public void FadeOutFast()
        {
            CanvesGroup.alpha = 1;
        }
        public IEnumerator FadeOut(float time)
        {

            while (CanvesGroup.alpha < 1)
            {

                CanvesGroup.alpha += Time.deltaTime / time;

               
                yield return null;

                
            }


        }
        public IEnumerator FadeIn(float time =.2f)
        {
           
            while (CanvesGroup.alpha > 0)
            {

                CanvesGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }

           
        }
    }
}
