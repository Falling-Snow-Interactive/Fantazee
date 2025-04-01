using DG.Tweening;
using Fantazee.Relics.Data;
using Fantazee.Relics.Instance;
using Fantazee.Relics.Settings;
using Fantazee.Ui.Buttons;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fantazee.Relics.Ui
{
    public class RelicEntryUi : SimpleButton
    {
        private RelicInstance relic;

        [SerializeField]
        private bool hideTooltipOnAwake = true;

        [Header("References")]
        
        [SerializeField]
        protected Image image;

        [FormerlySerializedAs("relicTooltip")]
        [SerializeField]
        protected GameObject tooltip;
        
        [SerializeField]
        protected TMP_Text relicName;
        
        [SerializeField]
        protected TMP_Text relicDescription;
        
        [Header("Input")]

        [SerializeField]
        private InputActionReference expandActionReference;
        private InputAction expandAction;

        private void Awake()
        {
            tooltip.SetActive(!hideTooltipOnAwake);
            if (expandActionReference)
            {
                expandAction = expandActionReference.ToInputAction();
            }
        }

        private void OnEnable()
        {
            if (relic != null)
            {
                relic.Activated += OnActivated;
            }

            if (expandAction != null)
            {
                expandAction.started += OnExpandStarted;
                expandAction.canceled += OnExpandCanceled;

                expandAction.Enable();
            }
        }

        private void OnDisable()
        {
            if (relic != null)
            {
                relic.Activated -= OnActivated;
            }

            if (expandAction != null)
            {
                expandAction.started -= OnExpandStarted;
                expandAction.canceled -= OnExpandCanceled;

                expandAction.Disable();
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
            tooltip.SetActive(set);
        }

        public override void OnSelect()
        {
            base.OnSelect();
            // tooltip.SetActive(true);
        }

        public override void OnDeselect()
        {
            base.OnDeselect();
            tooltip.SetActive(false);
        }
        
        private void OnExpandStarted(InputAction.CallbackContext ctx)
        {
            if (IsSelected)
            {
                tooltip.gameObject.SetActive(true);
            }
        }

        private void OnExpandCanceled(InputAction.CallbackContext ctx)
        {
            tooltip.gameObject.SetActive(false);
        }
    }
}