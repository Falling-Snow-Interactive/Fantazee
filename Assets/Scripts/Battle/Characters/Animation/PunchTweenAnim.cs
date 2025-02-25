using System;
using DG.Tweening;
using UnityEngine;

namespace Fantazee.Battle.Characters.Animation
{
    [Serializable]
    public class PunchTweenAnim
    {
        [SerializeField]
        private Vector3 positionPunch = Vector3.zero;

        [SerializeField]

        private Vector3 rotationPunch = Vector3.zero;

        [SerializeField]
        private Vector3 scalePunch = Vector3.zero;

        [SerializeField]
        private float time = 0.3f;

        [SerializeField]
        private int vibrato = 2;

        [SerializeField]
        private float elasticity = 2f;

        [SerializeField]
        private Ease ease = Ease.Linear;

        public void Play(Transform target, Action onComplete = null)
        {
            DOTween.Complete(target);
            
            target.localPosition = Vector3.zero;
            target.localRotation = Quaternion.identity;
            target.localScale = Vector3.one;
            
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(target.DOPunchPosition(positionPunch, 
                                                   time, 
                                                   vibrato, 
                                                   elasticity)
                                  .SetEase(ease));
            
            sequence.Insert(0, target
                               .DOPunchRotation(rotationPunch, 
                                                time, 
                                                vibrato, 
                                                elasticity)
                               .SetEase(ease));
            
            sequence.Insert(0, target.DOPunchScale(scalePunch,
                                                   time,
                                                   vibrato,
                                                   elasticity)
                                     .SetEase(ease));
            
            sequence.OnComplete(() =>
                                {
                                    onComplete?.Invoke();
                                });
            sequence.Play();
        }
    }
}