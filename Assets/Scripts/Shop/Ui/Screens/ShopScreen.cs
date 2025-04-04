using System;
using DG.Tweening;
using UnityEngine.Serialization;

namespace Fantazee.Shop.Ui.Screens
{
    public class ShopScreen : MonoBehaviour
    {
        public bool IsActiveScreen { get; private set; }
        
        [Header("Animation")]
        
        [FormerlySerializedAs("localIn")]
        [SerializeField]
        protected Vector3 showPos;
        
        [FormerlySerializedAs("localOut")]
        [SerializeField]
        protected Vector3 hidePos;

        [FormerlySerializedAs("inTime")]
        [SerializeField]
        private float showTime = 0.5f;
        
        [FormerlySerializedAs("outTime")]
        [SerializeField]
        private float hideTime = 0.5f;

        [FormerlySerializedAs("inEase")]
        [SerializeField]
        private Ease showEase = Ease.OutBack;
        
        [FormerlySerializedAs("outEase")]
        [SerializeField]
        private Ease hideEase = Ease.OutCubic;

        [Header("References")]

        [SerializeField]
        private GameObject root;

        public virtual void Show(bool force = false, Action onComplete = null)
        {
            IsActiveScreen = true;
            root.SetActive(true);
            if (force)
            {
                transform.localPosition = hidePos;
                onComplete?.Invoke();
                return;
            }
            
            transform.DOLocalMove(showPos, showTime)
                      .SetEase(showEase)
                      .OnComplete(() =>
                                  {
                                      onComplete?.Invoke(); 
                                  });
        }

        public virtual void Hide(bool force = false, Action onComplete = null)
        {
            IsActiveScreen = false;
            if (force)
            {
                transform.localPosition = hidePos;
                root.SetActive(false);
                onComplete?.Invoke();
                return;
            }
            
            transform.DOLocalMove(hidePos, hideTime)
                     .SetEase(hideEase)
                     .OnComplete(() =>
                                 {
                                     root.SetActive(false);
                                     onComplete?.Invoke(); 
                                 });
        }
    }
}
