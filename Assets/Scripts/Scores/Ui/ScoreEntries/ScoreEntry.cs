using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Dice.Settings;
using Fantazee.Items.Dice.Information;
using Fantazee.Scores.Instance;
using Fantazee.Spells.Instance;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Fantazee.Scores.Ui.ScoreEntries
{
    public abstract class ScoreEntry : MonoBehaviour
    {
        private Action<ScoreEntry> onSelect;
        
        [SerializeReference]
        private ScoreInstance score;
        public ScoreInstance Score
        {
            get => score;
            set => score = value;
        }

        [Header("References")]
        
        [SerializeField]
        protected TMP_Text nameText;
        
        [SerializeField]
        protected TMP_Text scoreText;

        [SerializeField]
        protected TMP_Text previewText;
        
        [SerializeField]
        protected Transform scoreContainer;

        [SerializeField]
        protected Button button;

        [SerializeField]
        protected List<Image> diceImages = new();
        
        [SerializeField]
        private List<ScoreEntrySpell> spells = new();
        public List<ScoreEntrySpell> Spells => spells;

        [SerializeField]
        private ScoreEntrySpellTooltip tooltip;
        
        private void Awake()
        {
            tooltip.Hide(true);
            
            scoreText?.gameObject.SetActive(false);
            previewText?.gameObject.SetActive(false);
        }

        public virtual void Initialize(ScoreInstance score, Action<ScoreEntry> onSelect)
        {
            this.score = score;
            this.onSelect = onSelect;
            
            Debug.Assert(spells.Count == score.Spells.Count);
            for (int i = 0; i < Spells.Count; i++)
            {
                Spells[i].Initialize(score.Spells[i]);
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
            scoreText.text = "";
            button.interactable = true;

            List<int> d = GetDiceValues();

            Debug.Assert(diceImages.Count == d.Count, 
                         $"DiceImages Count ({diceImages.Count}) != Dice Count ({d.Count}).", 
                         gameObject);
            
            for (int i = 0; i < diceImages.Count; i++)
            {
                ShowDieInSlot(i, d[i]);
            }

            Debug.Assert(spells.Count == score.Spells.Count, 
                         $"Spells Count ({spells.Count}) != Score Spells Count ({score.Spells.Count}).", 
                         gameObject);
            for (int i = 0; i < spells.Count; i++)
            {
                spells[i].Initialize(score.Spells[i]);
            }
        }

        protected virtual List<int> GetDiceValues()
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
        
        public void ShowDieInSlot(int index, int value)
        {
            if (diceImages.Count >= index &&
                DiceSettings.Settings.SideInformation.TryGetInformation(value, out SideInformation info))
            {
                diceImages[index].sprite = info.Sprite;
                diceImages[index].transform.parent.DOPunchScale(Vector3.one * 0.1f, 0.2f, 2, 0.5f);
            }
        }
        
        public void RequestSpell(Action<SpellInstance, ScoreEntry> onSpellSelect)
        {
            foreach (ScoreEntrySpell spell in spells)
            {
                spell.Activate(select =>
                               {
                                   foreach (ScoreEntrySpell s in spells)
                                   {
                                       s.Deactivate();
                                   }

                                   onSpellSelect?.Invoke(select, this);
                               });
            }
        }
    }
}