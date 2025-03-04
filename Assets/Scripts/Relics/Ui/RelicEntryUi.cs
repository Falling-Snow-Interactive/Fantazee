using DG.Tweening;
using Fantazee.Relics.Data;
using Fantazee.Relics.Information;
using Fantazee.Relics.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Relics.Ui
{
    public class RelicEntryUi : MonoBehaviour
    {
        private RelicInstance relic;

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
            relicTooltip.SetActive(false);
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
            relicName.text = relic.Data.LocName.GetLocalizedString();
            relicDescription.text = relic.Data.LocDesc.GetLocalizedString();

            relic.Activated += OnActivated;
        }

        public void ShowData(RelicData relicData)
        {
            image.sprite = relicData.Icon;
            relicName.text = relicData.LocName.GetLocalizedString();
            relicDescription.text = relicData.LocDesc.GetLocalizedString();
        }

        public void ShowData(RelicType type)
        {
            if (RelicSettings.Settings.Information.TryGetInformation(type, out RelicInformation info))
            {
                ShowData(info.Data);
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