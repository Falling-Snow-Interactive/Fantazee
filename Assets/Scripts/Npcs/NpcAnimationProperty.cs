using System;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace Fantazee.Npcs
{
    [Serializable]
    public class NpcAnimationProperty
    {
        [Header("Position")]
        
        [SerializeField]
        private Vector3 position;
        
        [SerializeField]
        private float positionTime;

        [SerializeField]
        private float prePositionTime;

        [SerializeField]
        private Ease positionEase;
        
        [Header("Scale")]
        
        [SerializeField]
        private Vector3 scale;
        
        [SerializeField]
        private float scaleTime;
        
        [SerializeField]
        private float preScaleTime;
        
        [SerializeField]
        private Ease scaleEase;
        
        [Header("Fade")]

        [SerializeField]
        private float fade;
        
        [SerializeField]
        private float fadeTime;

        [SerializeField]
        private float preFadeTime;

        [SerializeField]
        private Ease fadeEase;
        
        [Header("Finish")]
        
        [SerializeField]
        private float finishTime;
        
        public Sequence Apply(Image sprite)
        {
            Sequence sequence = DOTween.Sequence();

            sprite.transform.localPosition = position;
            sprite.transform.localScale = scale;
            Color color = sprite.color;
            color.a = fade;
            sprite.color = color;
            
            sequence.Insert(prePositionTime, sprite.transform.DOLocalMove(Vector3.zero, positionTime).SetEase(positionEase));
            sequence.Insert(preScaleTime, sprite.transform.DOScale(Vector3.one, scaleTime).SetEase(scaleEase));
            sequence.Insert(preFadeTime, sprite.DOFade(1, preScaleTime).SetEase(fadeEase));
            sequence.AppendInterval(finishTime);
            
            return sequence;
        }
    }
}