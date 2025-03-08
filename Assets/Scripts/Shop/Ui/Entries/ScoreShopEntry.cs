using System;
using Fantazee.Scores.Instance;
using UnityEngine;

namespace Fantazee.Shop.Ui.Entries
{
    public class ScoreShopEntry : ShopEntryUi
    {
        private Action<ScoreShopEntry> onSelect;
        
        [SerializeField]
        private ScoreInstance score;
        public ScoreInstance Score => score;
        
        public void Initialize(ScoreInstance score, Action<ScoreShopEntry> onSelect)
        {
            this.onSelect = onSelect;
            this.score = score;
            ShowEntry(score.Data.Name, score.Data.Description, score.Data.Cost);
        }
        
        public override void OnEntrySelected()
        {
            onSelect?.Invoke(this);
        }
    }
}