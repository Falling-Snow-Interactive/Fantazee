using System;
using Fantazee.Instance;
using Fantazee.Scores;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Shop.Ui.ScoreSelect;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Shop.Ui.Screens
{
    public class SpellScoreScreen : ScoreScreen
    {
        private SpellType spellType;
        private SpellEntry selected;
        
        [Header("Spell References")]
        
        [SerializeField]
        private SpellEntry entry;

        public void Initialize(SpellEntry selected, Action onComplete)
        {
            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.ScoreTracker.Scores.Count);
            
            spellType = selected.Data.Type;
            this.onComplete = onComplete;
            this.selected = selected;
            
            entry.gameObject.SetActive(true);
            entry.transform.localPosition = Vector3.zero;
            entry.Initialize(selected.Data, null);
            
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ScoreSelectEntry scoreEntry = scoreEntries[i];
                Score score = GameInstance.Current.Character.ScoreTracker.Scores[i];
                
                scoreEntry.Initialize(score, PlayAnimation);
            }
        }

        protected override bool Apply(ScoreSelectEntry scoreEntry)
        {
            if (!GameInstance.Current.Character.Wallet.Remove(entry.Data.Cost))
            {
                Debug.LogWarning("Shop: Cannot afford spell. Returning to shop.");
                return false;
            }
            
            Debug.Log($"Shop Spell: {scoreEntry.Score.Type} {scoreEntry.Score.Spells[0]} -> {spellType}");
            scoreEntry.Score.Spells[0] = spellType;
            
            selected.gameObject.SetActive(false);

            return true;
        }
    }
}