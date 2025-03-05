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

        private void Start()
        {
            dir = direction.Random().normalized;
            Debug.Log($"{dir} * {distance} = {dir * distance}");
            
            transform.DOMove(transform.position + dir * distance, time)
                .SetEase(ease)
                .OnComplete(() => Destroy(gameObject));
        }
        
        public void SetValue(int number)
        {
            damageNumberText.text = number.ToString();
        }
    }
}