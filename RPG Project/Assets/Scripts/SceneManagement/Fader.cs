using UnityEngine;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            StartCoroutine(FadeOutIn());
        }
        IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            print("faded out");
            yield return FadeIn(2f);
            print("faded in");
        }
        IEnumerator FadeOut(float time)
        {
            while(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }
        IEnumerator FadeIn(float time)
        {
            while(canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}