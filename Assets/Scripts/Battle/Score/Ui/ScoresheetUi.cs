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

        [SerializeField]
        private ScoreButton fantazeeButton;

        private readonly Dictionary<ScoreInstance, ScoreButton> buttons = new();

        public void Initialize(Scoresheet scoresheet)
        {
            Debug.Assert(scoresheet.Scores.Count == scoreButtons.Count);
            for (int i = 0; i < scoresheet.Scores.Count; i++)
            {
                ScoreInstance score = scoresheet.Scores[i];
                ScoreButton button = scoreButtons[i];
                button.Initialize(score, OnScoreEntrySelected);
                
                buttons.Add(score, button);
            }
            
            fantazeeButton.Initialize(scoresheet.Fantazee, OnScoreEntrySelected);
        }

        public void RequestScore(Action<ScoreButton> onScoreSelect)
        {
            this.onScoreSelect = onScoreSelect;
            spellRequested = false;
        }

        public void RequestSpell(Action<ScoreButton, SpellButton> onSpellSelect)
        {
            this.onSpellSelect = onSpellSelect;
            spellRequested = true;
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