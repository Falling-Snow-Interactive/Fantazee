using System;
using DG.Tweening;
using UnityEngine;

namespace Fantazee.Battle.Characters.Animation
{
    [Serializable]
    public class TweenAnim
    {
        [SerializeField]
        private Vector3 position = Vector3.zero;

        [SerializeField]
        private Vector3 rotation = Vector3.zero;

        [SerializeField]
        private Vector3 scale = Vector3.one;

        [SerializeField]
        private float time = 1f;

        [SerializeField]
        private Ease ease = Ease.Linear;
        
        [SerializeField]
        private int loops = 0;

        [SerializeField]
        private LoopType loopType;

        public void Play(Transform target)
        {
            DOTween.Complete(target);
            
            target.localPosition = Vector3.zero;
            target.localRotation = Quaternion.identity;
            target.localScale = Vector3.one;
            
            target.DOLocalMove(position, time).SetEase(ease).SetLoops(loops, loopType);
            target.DOLocalRotate(rotation, time).SetEase(ease).SetLoops(loops, loopType);
            target.DOScale(scale, time).SetEase(ease).SetLoops(loops, loopType);
        }
    }
}