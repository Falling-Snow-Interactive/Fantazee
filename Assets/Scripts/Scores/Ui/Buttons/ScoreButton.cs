using System;
using DG.Tweening;
using Fantazee.Scores.Instance;
using Fantazee.Spells.Ui;
using Fantazee.Ui.Buttons;
using TMPro;
using UnityEngine.EventSystems;

namespace Fantazee.Scores.Ui.Buttons
{
    public class ScoreButton : SimpleButton
    {
        // Callback
        private Action<ScoreButton> onClickCallback;

        // Score
        public ScoreInstance Score { get; set; }
        private List<Tweener> colorTweens = new();

        [Header("References")]

        [SerializeField]
        private TMP_Text nameText;

        public TMP_Text NameText => nameText;


        [SerializeField]
        private List<SpellButton> spells = new();

        public void Initialize(ScoreInstance score, Action<ScoreButton> onClick)
        {
            Score = score;
            onClickCallback = onClick;

            Debug.Assert(spells.Count <= score.Spells.Count);
            for (int i = 0; i < spells.Count; i++)
            {
                spells[i].Initialize(score.Spells[i], null);
            }

            UpdateVisuals();
        }

        public void UpdateVisuals()
        {
            nameText.text = Score.Data.Name;
            if (button)
            {
                button.interactable = true;
            }

            Debug.Assert(spells.Count == Score.Spells.Count,
                         $"Spells Count ({spells.Count}) != Score Spells Count ({Score.Spells.Count}).",
                         gameObject);
            for (int i = 0; i < spells.Count; i++)
            {
                spells[i].Initialize(Score.Spells[i], null);
            }
        }

        protected List<int> GetDiceValues()
        {
            List<int> diceValues = new();

            switch (Score)
            {
                case NumberScoreInstance n:
                    diceValues = new List<int>
                                 {
                                     n.NumberData.Number,
                                     n.NumberData.Number,
                                     n.NumberData.Number,
                                     n.NumberData.Number,
                                     n.NumberData.Number
                                 };
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
                    diceValues = new List<int> { 5, 5, 5, 2, 2 };
                    break;
                case ChanceScoreInstance c:
                    diceValues = new List<int>
                                 {
                                     Random.Range(1, 6),
                                     Random.Range(1, 6),
                                     Random.Range(1, 6),
                                     Random.Range(1, 6),
                                     Random.Range(1, 6)
                                 };
                    break;
                case FantazeeScoreInstance f:
                    diceValues = new List<int> { 6, 6, 6, 6, 6 };
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
                    Debug.LogError($"{nameof(Score)} has not been implemented.");
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

        public override void OnClick()
        {
            base.OnClick();
            onClickCallback?.Invoke(this);
        }
    }
}