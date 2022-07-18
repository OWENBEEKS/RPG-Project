using UnityEngine;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine currentlyActiveFade = null;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }
        IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            print("faded out");
            yield return FadeIn(2f);
            print("faded in");
        }
        public IEnumerator FadeOut(float time)
        {
            return Fade(1, time);
        }
        public IEnumerator FadeIn(float time)
        {
            return Fade(0, time);
        }
        public IEnumerator Fade(float target, float time)
        {
            if(currentlyActiveFade != null)
            {
                StopCoroutine(currentlyActiveFade);
            }
            currentlyActiveFade = StartCoroutine(FadeRoutine(1, time));
            yield return currentlyActiveFade;
        }
        private IEnumerator FadeRoutine(float target, float time)
        {
            while(Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time);
                yield return null;
            }
        }
    }
}