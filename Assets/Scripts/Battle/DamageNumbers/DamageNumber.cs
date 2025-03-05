using DG.Tweening;
using Fsi.Gameplay;
using TMPro;
using UnityEngine;

namespace Fantazee.Battle.DamageNumbers
{
    public class DamageNumber : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text damageNumberText;

        [SerializeField]
        private RangeVector3 direction;

        private Vector3 dir;

        [SerializeField]
        private float distance;

        [SerializeField]
        private float time = 1f;

        [SerializeField]
        private Ease ease;

        [SerializeField]
        private Vector3 scale;

        [SerializeField]
        private Ease scaleEase;

        private void Start()
        {
            dir = direction.Random().normalized;
            transform.DOMove(transform.position + dir * distance, time)
                .SetEase(ease)
                .OnComplete(() => Destroy(gameObject));
            transform.DOScale(scale, time)
                     .SetEase(scaleEase);
        }
        
        public void SetValue(int number)
        {
            damageNumberText.text = number.ToString();
        }
    }
}