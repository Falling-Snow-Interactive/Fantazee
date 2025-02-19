using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectYahtzee.LoadingScreens
{
    public class LoadingScreen : MonoBehaviour
    {
        [Header("Delays")]
        
        [SerializeField]
        private float postShowDelay = 0.5f;
        
        [SerializeField]
        private float postHideDelay = 0.5f;
        
        [Header("Fade")]

        [SerializeField]
        private float fadeTime = 0.25f;

        [SerializeField]
        private Ease showEase = Ease.OutQuad;
        
        [SerializeField]
        private Ease hideEase = Ease.InQuad;
        
        [Header("References")]
        
        [SerializeField]
        private Image overlay;

        public void Show(Action onComplete)
        {
            Debug.Log("Show");
            overlay.gameObject.SetActive(true);
            overlay.DOColor(Color.black, fadeTime)
                   .OnComplete(() =>
                               {
                                   StartCoroutine(Delay(postShowDelay, onComplete));
                               });
        }

        public void Hide(Action onComplete)
        {
            Debug.Log("Hide");
            overlay.gameObject.SetActive(true);
            overlay.DOColor(Color.clear, fadeTime)
                   .OnComplete(() =>
                               {
                                   overlay.gameObject.SetActive(false);
                                   StartCoroutine(Delay(postHideDelay, onComplete));
                               });
        }

        private IEnumerator Delay(float delay, Action onComplete)
        {
            yield return new WaitForSeconds(delay);
            onComplete?.Invoke();
        }
    }
}
