using DG.Tweening;
using Fantazee.Relics.Data;
using Fantazee.Relics.Information;
using Fantazee.Relics.Instance;
using Fantazee.Relics.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fantazee.Relics.Ui
{
    public class RelicEntryUi : MonoBehaviour
    {
        private RelicInstance relic;

        [SerializeField]
        private bool hideTooltipOnAwake = true;

        [Header("References")]
        
        [SerializeField]
        private Image image;

        [SerializeField]
        private GameObject relicTooltip;
        
        [SerializeField]
        private TMP_Text relicName;
        
        [SerializeField]
        private TMP_Text relicDescription;

        private void Awake()
        {
            relicTooltip.SetActive(!hideTooltipOnAwake);
        }

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
            relicName.text = relic.Data.Name;
            relicDescription.text = relic.Data.Description;

            relic.Activated += OnActivated;
        }

        public void ShowData(RelicData relicData)
        {
            image.sprite = relicData.Icon;
            relicName.text = relicData.Name;
            relicDescription.text = relicData.Description;
        }

        public void ShowData(RelicType type)
        {
            if (RelicSettings.Settings.TryGetRelic(type, out RelicData data))
            {
                ShowData(data);
            }
        }

        private void OnActivated()
        {
            image.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 10, 1f);
        }

        public void SetTooltip(bool set)
        {
            relicTooltip.SetActive(set);
        }
    }
}