using System;
using DG.Tweening;
using UnityEngine;

namespace Fantazee.Shop.Ui.Screens
{
    public class ShopScreen : MonoBehaviour
    {
        [Header("Animation")]
        
        [SerializeField]
        protected Vector3 localIn;
        
        [SerializeField]
        protected Vector3 localOut;

        [SerializeField]
        private float inTime = 0.5f;
        
        [SerializeField]
        private float outTime = 0.5f;

        [SerializeField]
        private Ease inEase = Ease.OutBack;
        
        [SerializeField]
        private Ease outEase = Ease.OutCubic;

        public void SlideIn(Action onComplete = null)
        {
            transform.DOLocalMove(localIn, inTime)
                      .SetEase(inEase)
                      .OnComplete(() =>
                                  {
                                      onComplete?.Invoke(); 
                                  });
        }

        public void SlideOut(Action onComplete = null)
        {
            transform.DOLocalMove(localOut, outTime)
                     .SetEase(outEase)
                     .OnComplete(() =>
                                 {
                                     onComplete?.Invoke(); 
                                 });
        }
    }
}
