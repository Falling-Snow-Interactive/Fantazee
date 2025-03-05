using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.LoadingScreens
{
    public class LoadingScreen : MonoBehaviour
    {
        [Header("Delays")]
        
        [SerializeField]
        private float preShowDelay = 0.5f;
        
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

        public void Show(float delay = 0, Action onComplete = null)
        {
            Debug.Log("LoadingScreen: Show");
            overlay.gameObject.SetActive(true);
            overlay.DOColor(Color.black, fadeTime)
                   .SetDelay(delay + preShowDelay)
                   .OnComplete(() =>
                               {
                                   StartCoroutine(Delay(postShowDelay, onComplete));
                               });
        }

        public void Hide(float delay = 0, Action onComplete = null)
        {
            Debug.Log("LoadingScreen: Hide");
            overlay.gameObject.SetActive(true);
            overlay.DOColor(Color.clear, fadeTime)
                   .SetDelay(delay)
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
