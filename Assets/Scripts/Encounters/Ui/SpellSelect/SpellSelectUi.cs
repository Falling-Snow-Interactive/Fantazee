using System;
using System.Collections.Generic;
using Fantazee.Instance;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.Buttons;
using Fantazee.Spells;
using Fantazee.Spells.Ui;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Encounters.Ui.SpellSelect
{
    public class SpellSelectUi : MonoBehaviour
    {
        private SpellInstance spell;

        [SerializeField]
        private SpellButton selectedEntry;

        [SerializeField]
        private List<ScoreButton> scoreEntries = new();

        [FormerlySerializedAs("fantazeeEntry")]
        [SerializeField]
        private ScoreButton fantazeeButton;
        
        public void Initialize(SpellInstance spell, Action onComplete)
        {
            this.spell = spell;
            selectedEntry.Initialize(spell);

            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.Scoresheet.Scores.Count);
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ScoreButton scoreButton = scoreEntries[i];
                ScoreInstance score = GameInstance.Current.Character.Scoresheet.Scores[i];
                
                scoreButton.Initialize(score, se =>
                                             {
                                                 OnScoreSelected(se, onComplete);
                                             });
            }
            
            fantazeeButton.Initialize(GameInstance.Current.Character.Scoresheet.Fantazee, 
                                     se => OnScoreSelected(se, onComplete));
        }

        private void OnScoreSelected(ScoreButton scoreButton, Action onComplete)
        {
            
        }

        private void OnSpellSelected()
        {
            
        }
    }
}