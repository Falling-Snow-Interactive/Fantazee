using System;
using Fantazee.Characters;
using Fantazee.Currencies.Ui;
using Fantazee.Scores;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.Buttons;
using UnityEngine;

namespace Fantazee.Shop.Ui.Entries
{
    public class ShopScoreButtonPurchase : ShopScoreButton
    {
        [Header("Cost")]
        
        [SerializeField]
        private CurrencyEntryUi currencyEntryUi;
        
        public override void Initialize(ScoreInstance score, Action<ScoreButton> onSelect)
        {
            base.Initialize(score, onSelect);
            currencyEntryUi.SetCurrency(score.Data.Cost);
        }
    }
}