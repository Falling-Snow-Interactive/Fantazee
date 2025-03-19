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

        public void Play(Transform target, Action onComplete = null)
        {
            DOTween.Complete(target);
            
            Sequence sequence = DOTween.Sequence();

            Tween move = target.DOLocalMove(position, time).SetEase(Ease.Linear);
            Tween rot = target.DOLocalRotate(rotation, time).SetEase(Ease.Linear);
            Tween scl = target.DOScale(scale, time).SetEase(Ease.Linear);

            sequence.Insert(0, move);
            sequence.Insert(0, rot);
            sequence.Insert(0, scl);
            
            sequence.SetLoops(loops, loopType);
            sequence.SetEase(ease);
            
            // sequence.SetLink(target.gameObject, LinkBehaviour.CompleteAndKillOnDisable);
            
            sequence.OnComplete(() => onComplete?.Invoke());
            
            sequence.Play();
        }
    }
}