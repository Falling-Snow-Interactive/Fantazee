using System;
using System.Collections.Generic;
using Fantazee.Scores.Instance;
using Fantazee.Spells.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Fantazee.Scores.Ui.Buttons
{
    public class ScoreButton : MonoBehaviour
    {
        private Action<ScoreButton> onSelect;
        
        [SerializeReference]
        private ScoreInstance score;
        public ScoreInstance Score
        {
            get => score;
            set => score = value;
        }

        [Header("References")]
        
        [SerializeField]
        private TMP_Text nameText;
        public TMP_Text NameText => nameText;

        [SerializeField]
        protected Button button;
        public Button Button => button;
        
        [SerializeField]
        private List<SpellButton> spells = new();
        public List<SpellButton> Spells => spells;

        public void Initialize(ScoreInstance score, Action<ScoreButton> onSelect)
        {
            this.score = score;
            this.onSelect = onSelect;
            
            Debug.Assert(spells.Count <= score.Spells.Count);
            for (int i = 0; i < Spells.Count; i++)
            {
                Spells[i].Initialize(score.Spells[i], null);
            }
            
            UpdateVisuals();
        }
        
        public void OnSelect()
        {
            onSelect?.Invoke(this);
        }

        public void UpdateVisuals()
        {
            nameText.text = score.Data.Name;
            button.interactable = true;

            Debug.Assert(spells.Count == score.Spells.Count, 
                         $"Spells Count ({spells.Count}) != Score Spells Count ({score.Spells.Count}).", 
                         gameObject);
            for (int i = 0; i < spells.Count; i++)
            {
                spells[i].Initialize(score.Spells[i], null);
            }
        }

        protected List<int> GetDiceValues()
        {
            List<int> diceValues = new();

            switch (score)
            {
                case NumberScoreInstance n:
                    diceValues = new List<int>{n.NumberData.Number, 
                                                  n.NumberData.Number, 
                                                  n.NumberData.Number,
                                                  n.NumberData.Number,
                                                  n.NumberData.Number};
                    break;
                case KindScoreInstance k:
                    diceValues = new List<int>();
                    for (int i = 0; i < k.KindData.Kind; i++)
                    {
                        diceValues.Add(6);
                    }

                    while (diceValues.Count < 5)
                    {
                        diceValues.Add(Random.Range(1, 6));
                    }

                    break;
                case RunScoreInstance r:
                    diceValues = new List<int>();
                    for (int i = 0; i < r.RunData.Run; i++)
                    {
                        diceValues.Add(1 + i);
                    }

                    while (diceValues.Count < 5)
                    {
                        diceValues.Add(Random.Range(1, r.RunData.Run));
                    }

                    break;
                case FullHouseScoreInstance f:
                    diceValues = new List<int> { 5,5,5,2,2 };
                    break;
                case ChanceScoreInstance c:
                    diceValues = new List<int>{Random.Range(1, 6), 
                                                  Random.Range(1, 6), 
                                                  Random.Range(1, 6), 
                                                  Random.Range(1, 6), 
                                                  Random.Range(1, 6)};
                    break;
                case FantazeeScoreInstance f:
                    diceValues = new List<int> { 6,6,6,6,6 };
                    break;
                case TwoPairScoreInstance tp:
                    diceValues = new List<int> { 6, 6, 5, 5, Random.Range(1, 5) };
                    break;
                case EvenOddScoreInstance e when !e.EvenOddData.Even:
                    diceValues = new List<int> { 1, 3, 5, 3, 1 };
                    break;
                case EvenOddScoreInstance e when e.EvenOddData.Even:
                    diceValues = new List<int> { 2, 4, 6, 4, 2 };
                    break;
                default:
                    Debug.LogError($"{nameof(score)} has not been implemented.");
                    break;
            }
            
            return diceValues;
        }

        #region Request Spell
        
        public void RequestSpell(Action<SpellButton> onSpellSelect)
        {
            foreach (SpellButton spell in spells)
            {
                spell.Activate(s => OnSpellSelected(s, onSpellSelect));
            }
        }

        private void OnSpellSelected(SpellButton spell, Action<SpellButton> onSpellSelect)
        {
            foreach (SpellButton s in spells)
            {
                s.Deactivate();
            }

            onSpellSelect?.Invoke(spell);
        }
        
        #endregion
    }
}