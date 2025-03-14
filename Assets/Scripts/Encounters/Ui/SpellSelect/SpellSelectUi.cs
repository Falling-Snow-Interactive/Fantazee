using System;
using System.Collections.Generic;
using Fantazee.Instance;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Encounters.Ui.SpellSelect
{
    public class SpellSelectUi : MonoBehaviour
    {
        private SpellInstance spell;

        [SerializeField]
        private ScoreEntrySpell selectedEntry;

        [SerializeField]
        private List<ScoreEntry> scoreEntries = new();

        [SerializeField]
        private ScoreEntry fantazeeEntry;
        
        public void Initialize(SpellInstance spell, Action onComplete)
        {
            this.spell = spell;
            selectedEntry.Initialize(spell);

            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.Scoresheet.Scores.Count);
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ScoreEntry scoreEntry = scoreEntries[i];
                ScoreInstance score = GameInstance.Current.Character.Scoresheet.Scores[i];
                
                scoreEntry.Initialize(score, se =>
                                             {
                                                 OnScoreSelected(se, onComplete);
                                             });
            }
            
            fantazeeEntry.Initialize(GameInstance.Current.Character.Scoresheet.Fantazee, 
                                     se => OnScoreSelected(se, onComplete));
        }

        private void OnScoreSelected(ScoreEntry scoreEntry, Action onComplete)
        {
            
        }

        private void OnSpellSelected()
        {
            
        }
    }
}