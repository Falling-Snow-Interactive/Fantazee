using System;
using DG.Tweening;
using Fantazee.Currencies.Ui;
using Fantazee.Spells;
using Fantazee.Spells.Ui;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.Entries
{
    public class ShopSpellButton : SpellButton, IPointerExitHandler
    {
        [Header("Cost")]
        
        [SerializeField]
        private CurrencyEntryUi currencyEntryUi;
        
        [Header("Score References")]

        [SerializeField]
        private Image borderImage;

        [SerializeField]
        private SpellTooltip tooltip;
        
        [Header("     Input")]

        [SerializeField]
        private InputActionReference expandActionReference;
        private InputAction expandAction;

        private void Awake()
        {
            if (expandActionReference)
            {
                expandAction = expandActionReference.ToInputAction();
            }
        }
        
        private void OnEnable()
        {
            if (expandAction != null)
            {
                expandAction.started += OnExpandStarted;
                expandAction.canceled += OnExpandCanceled;

                expandAction.Enable();
            }
        }

        private void OnDisable()
        {
            if (expandAction != null)
            {
                expandAction.started -= OnExpandStarted;
                expandAction.canceled -= OnExpandCanceled;

                expandAction.Disable();
            }
        }
        
        public void Initialize(SpellInstance spell, Action<ShopSpellButton> onSelect)
        {
            canSelect = true;
            base.Initialize(spell, _ =>
                                   {
                                       Debug.Log("Boogrsp");
                                       onSelect?.Invoke(this);
                                   });
            currencyEntryUi.SetCurrency(spell.Data.Cost);
            tooltip.Initialize(spell);
            if (tooltip.transform is RectTransform rect)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            }

            tooltip.gameObject.SetActive(false);
            
            // TODO - show and hide 
        }
        
        public void PlayCantAfford()
        {
            DOTween.Complete(transform);
            DOTween.Complete(borderImage);
            
            transform.DOPunchScale(Vector3.one * -0.1f, 0.2f, 10, 1f);
            
            Color b1 = borderImage.color;
            Color b2 = Color.red;
            b2.a = b1.a;
            borderImage.color = b2;
            borderImage.DOColor(b1, 0.2f).SetLink(gameObject, LinkBehaviour.CompleteAndKillOnDisable);
        }

        public override void OnSelect()
        {
            base.OnSelect();
        }
        
        public override void OnDeselect()
        {
            base.OnDeselect();
            tooltip.gameObject.SetActive(false);
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