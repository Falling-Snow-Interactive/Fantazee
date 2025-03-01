using System;
using Fantazee.Currencies.Ui;
using Fantazee.Scores;
using Fantazee.Scores.Ui;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Shop.Ui.ScoreSelect;
using UnityEngine;

namespace Fantazee.Shop.Ui.Entries
{
    public class ShopScoreEntryPurchase : ShopScoreEntry
    {
        [Header("Cost")]
        
        [SerializeField]
        private CurrencyEntryUi currencyEntryUi;
        
        public override void Initialize(Score score, Action<ScoreEntry> onSelect)
        {
            base.Initialize(score, onSelect);
            currencyEntryUi.SetCurrency(score.Information.Cost);
        }
    }
}