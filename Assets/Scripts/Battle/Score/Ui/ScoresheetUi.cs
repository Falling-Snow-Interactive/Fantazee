using System;
using System.Collections.Generic;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Scoresheets;
using Fantazee.Scores.Ui.Buttons;
using Fantazee.Spells.Ui;
using UnityEngine;

namespace Fantazee.Battle.Score.Ui
{
    public class ScoresheetUi : MonoBehaviour
    {
        private Action<ScoreButton> onScoreSelect;
        private Action<ScoreButton, SpellButton> onSpellSelect;

        private bool spellRequested = false;
        
        [Header("Buttons")]
        
        [SerializeField]
        private List<ScoreButton> scoreButtons = new();
        public List<ScoreButton> ScoreButtons => scoreButtons;

        [SerializeField]
        private ScoreButton fantazeeButton;
        
        public void Initialize(Scoresheet scoresheet)
        {
            Debug.Assert(scoresheet.Scores.Count == scoreButtons.Count);
            for (int i = 0; i < scoresheet.Scores.Count; i++)
            {
                ScoreInstance score = scoresheet.Scores[i];
                ScoreButton button = scoreButtons[i];
                button.Initialize(score, OnScoreEntrySelected);
            }
            
            fantazeeButton.Initialize(scoresheet.Fantazee, OnScoreEntrySelected);
        }

        public void RequestScore(Action<ScoreButton> onScoreSelect, bool excludeFantazee = false)
        {
            this.onScoreSelect = onScoreSelect;
            spellRequested = false;

            fantazeeButton.SetInteractable(!excludeFantazee);
        }

        public void RequestSpell(Action<ScoreButton, SpellButton> onSpellSelect)
        {
            this.onSpellSelect = onSpellSelect;
            spellRequested = true;
            fantazeeButton.SetInteractable(true);
        }

        private void OnScoreEntrySelected(ScoreButton scoreButton)
        {
            if (spellRequested)
            {
                scoreButton.RequestSpell(spellButton =>
                                          {
                                              OnSpellSelected(scoreButton, spellButton);
                                          });
            }
            else
            {
                onScoreSelect?.Invoke(scoreButton);
            }
        }

        private void OnSpellSelected(ScoreButton scoreButton, SpellButton spellButton)
        {
            onSpellSelect?.Invoke(scoreButton, spellButton);
        }
    }
}