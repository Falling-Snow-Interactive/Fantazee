using System;
using Fantazee.Characters;
using Fantazee.Currencies.Ui;
using Fantazee.Scores;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.ScoreEntries;
using UnityEngine;

namespace Fantazee.Shop.Ui.Entries
{
    public class ShopScoreEntryPurchase : ShopScoreEntry
    {
        [Header("Cost")]
        
        [SerializeField]
        private CurrencyEntryUi currencyEntryUi;
        
        public override void Initialize(ScoreInstance score, Action<ScoreEntry> onSelect)
        {
            base.Initialize(score, onSelect);
            currencyEntryUi.SetCurrency(score.Data.Cost);
        }
    }
}