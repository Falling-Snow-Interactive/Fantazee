using System;
using DG.Tweening;
using Fantazee.Currencies.Ui;
using Fantazee.Spells;
using Fantazee.Spells.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.Entries
{
    public class ShopSpellButton : MonoBehaviour
    {
        public SpellInstance Spell => spellButton.Spell;
        
        [SerializeField]
        private SpellButton spellButton;
        
        [Header("Cost")]
        
        [SerializeField]
        private CurrencyEntryUi currencyEntryUi;
        
        [Header("Score References")]

        [SerializeField]
        private Image borderImage;
        
        public void Initialize(SpellInstance spell, Action<ShopSpellButton> onSelect)
        {
            spellButton.Initialize(spell, _ =>
                                   {
                                       onSelect?.Invoke(this);
                                   });
            currencyEntryUi.SetCurrency(spell.Data.Cost);
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
            borderImage.DOColor(b1, 0.2f);
        }
    }
}