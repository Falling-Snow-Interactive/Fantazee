using System;
using Fantazee.Battle.Settings;
using Fantazee.Scores;
using Fantazee.Scores.Information;
using UnityEngine;

namespace Fantazee.Shop.Ui.Entries
{
    public class ScoreShopEntry : ShopEntryUi
    {
        private Action<ScoreShopEntry> onSelect;
        
        [SerializeField]
        private ScoreType score;
        public ScoreType Score => score;

        [SerializeReference]
        private ScoreInformation information;
        public ScoreInformation Information => information;
        
        public void Initialize(ScoreType score, Action<ScoreShopEntry> onSelect)
        {
            this.onSelect = onSelect;
            this.score = score;

            if (BattleSettings.Settings.ScoreInformation.TryGetInformation(score, out information))
            {
                ShowEntry(information.LocName.GetLocalizedString(), 
                          information.LocDesc.GetLocalizedString(),
                          information.Cost);
            }
        }
        
        public override void OnEntrySelected()
        {
            onSelect?.Invoke(this);
        }
    }
}