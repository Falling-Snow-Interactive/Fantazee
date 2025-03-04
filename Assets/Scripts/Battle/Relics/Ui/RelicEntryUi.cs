using System;
using DG.Tweening;
using Fantazee.Relics;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Battle.Relics.Ui
{
    public class RelicEntryUi : MonoBehaviour
    {
        private RelicInstance relic;

        [Header("References")]
        
        [SerializeField]
        private Image image;

        private void OnEnable()
        {
            if (relic != null)
            {
                relic.Activated += OnActivated;
            }
        }

        private void OnDisable()
        {
            if (relic != null)
            {
                relic.Activated -= OnActivated;
            }
        }

        public void Initialize(RelicInstance relic)
        {
            this.relic = relic;

            image.sprite = relic.Data.Icon;

            relic.Activated += OnActivated;
        }

        private void OnActivated()
        {
            image.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 10, 1f);
        }
    }
}