using System;
using DG.Tweening;
using UnityEngine;

namespace Fantazee.Ui
{
    [Serializable]
    public class ScreenAnimationProperties
    {
        [Tooltip("True: \tAnimate from the set values to 0.\n" +
                 "False: \tAnimate from 0 to the set values.")]
        [SerializeField]
        private bool animateIn = true;

        [Header("Move")]
        
        [SerializeField]
        private Vector3 move = Vector3.zero;

        [SerializeField]
        private float moveTime = 1f;

        [SerializeField]
        private Ease moveEase = Ease.Linear;

        [SerializeField]
        private float moveDelay = 0;
        
        [Header("Rotate")]
        
        [SerializeField]
        private Vector3 rotate = Vector3.zero;
        
        [SerializeField]
        private float rotateTime = 1f;
        
        [SerializeField]
        private Ease rotateEase = Ease.Linear;

        [SerializeField]
        private float rotateDelay;
        
        [Header("Scale")]
        
        [SerializeField]
        private Vector3 scale = Vector3.zero;
        
        [SerializeField]
        private float scaleTime = 1f;
        
        [SerializeField]
        private Ease scaleEase = Ease.Linear;
        
        [SerializeField]
        private float scaleDelay;

        [Header("Delays")]

        [SerializeField]
        private float startDelay = 0f;
        
        [SerializeField]
        private float endDelay = 0f;

        public Sequence PlaySequence(Transform transform, bool play = true)
        {
            Sequence sequence = DOTween.Sequence();

            Tween mov = transform.DOLocalMove(move, moveTime).SetEase(moveEase);
            Tween rot = transform.DOLocalMove(rotate, rotateTime).SetEase(rotateEase);
            Tween sca = transform.DOLocalMove(scale, moveTime).SetEase(scaleEase);
            
            sequence.Insert(startDelay + moveDelay, mov);
            sequence.Insert(startDelay + rotateDelay, rot);
            sequence.Insert(startDelay + scaleDelay, sca);
            
            sequence.AppendInterval(endDelay);
            
            if (play)
            {
                sequence.Play();
            }
            
            return sequence;
        }
    }
}