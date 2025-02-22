using System;
using DG.Tweening;
using UnityEngine;

namespace Fantazee.Battle.Characters
{
    [Serializable]
    public class CharacterTweenAnimation
    {
        public enum TweenType
        {
            Scale = 0,
            Rotation = 1,
            Position = 2,
            PunchScale = 3,
            PunchPosition = 4,
            PunchRotation = 5,
        }
        
        [SerializeField]
        private TweenType type = TweenType.Scale;

        [SerializeField]
        private bool local = true;

        [SerializeField]
        private Vector3 vector;

        [SerializeField]
        private float time;

        [SerializeField]
        private Ease ease;

        [SerializeField]
        private int loops = -1;
        
        [SerializeField]
        private LoopType loopType;

        private Tween tween;

        public void Play(Transform target, Action onComplete = null)
        {
            switch (type)
            {
                case TweenType.Scale:
                    ScaleTween(target, onComplete);
                    break;
                case TweenType.Rotation:
                    RotationTween(target, onComplete);
                    break;
                case TweenType.Position:
                    PositionTween(target, onComplete);
                    break;
                case TweenType.PunchScale:
                    PunchScaleTween(target, onComplete);
                    break;
                case TweenType.PunchPosition:
                    PunchPositionTween(target, onComplete);
                    break;
                case TweenType.PunchRotation:
                    PunchRotationTween(target, onComplete);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ScaleTween(Transform target, Action onComplete = null)
        {
            tween?.Kill();
            tween = target.DOScale(vector, time)
                          .SetEase(ease)
                          .SetLoops(loops, loopType)
                          .OnComplete(() => onComplete?.Invoke());
        }

        private void RotationTween(Transform target, Action onComplete = null)
        {
            tween?.Kill();
            if (local)
            {
                tween = target.DOLocalRotate(vector, time)
                              .SetEase(ease)
                              .SetLoops(loops, loopType)
                              .OnComplete(() => onComplete?.Invoke());
            }
            else
            {
                tween = target.DOMove(vector, time)
                              .SetEase(ease)
                              .SetLoops(loops, loopType)
                              .OnComplete(() => onComplete?.Invoke());
            }
        }

        private void PositionTween(Transform target, Action onComplete = null)
        {
            tween?.Kill();
            if (local)
            {
                tween = target.DOLocalMove(vector, time)
                              .SetEase(ease)
                              .SetLoops(loops, loopType)
                              .OnComplete(() => onComplete?.Invoke());
            }
            else
            {
                tween = target.DOMove(vector, time)
                              .SetEase(ease)
                              .SetLoops(loops, loopType)
                              .OnComplete(() => onComplete?.Invoke());
            }
        }

        private void PunchScaleTween(Transform target, Action onComplete = null)
        {
            tween?.Kill();
            tween = target.DOPunchScale(vector, time)
                          .SetEase(ease)
                          .SetLoops(loops, loopType)
                          .OnComplete(() => onComplete?.Invoke());
        }
        
        private void PunchRotationTween(Transform target, Action onComplete = null)
        {
            tween?.Kill();
            tween = target.DOPunchRotation(vector, time)
                          .SetEase(ease)
                          .SetLoops(loops, loopType)
                          .OnComplete(() => onComplete?.Invoke());
        }

        private void PunchPositionTween(Transform target, Action onComplete = null)
        {
            tween?.Kill();
            tween = target.DOPunchPosition(vector, time, 2, 0.2f)
                          .SetEase(ease)
                          .SetLoops(loops, loopType)
                          .OnComplete(() => onComplete?.Invoke());
        }
    }
}