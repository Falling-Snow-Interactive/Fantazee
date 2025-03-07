using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Dice.Settings;
using Fantazee.Items.Dice.Information;
using Fantazee.Scores.Data;
using Fantazee.Scores.Information;
using Fantazee.Scores.Instance;
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
        
        private ScoreInformation information;
        
        private void Awake()
        {
            tooltip.Hide(true);
        }

        public virtual void Initialize(ScoreInstance score, Action<ScoreEntry> onSelect)
        {
            this.score = score;
            this.onSelect = onSelect;
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
                         $"Spells Count ({diceImages.Count}) != Score Spells Count ({score.Spells.Count}).", 
                         gameObject);
            for (int i = 0; i < spells.Count; i++)
            {
                spells[i].Initialize(i, score.Spells[i]);
            }
        }

        protected virtual List<int> GetDiceValues()
        {
            List<int> diceValues = new();

            switch (score)
            {
                case NumberScoreInstance n:
                    diceValues = new List<int>{n.Data.Number, n.Data.Number, n.Data.Number, n.Data.Number, n.Data.Number};
                    break;
                case KindScoreInstance k:
                    diceValues = new List<int>();
                    for (int i = 0; i < k.Data.Kind; i++)
                    {
                        diceValues.Add(4);
                    }
                    diceValues.Add(Random.Range(1, 4));
                    diceValues.Add(Random.Range(1, 4));
                    break;
                case RunScoreInstance r:
                    diceValues = new List<int>();
                    for (int i = 0; i < r.Data.Run; i++)
                    {
                        diceValues.Add(1 + i);
                    }

                    while (diceValues.Count < 5)
                    {
                        diceValues.Add(Random.Range(1, r.Data.Run));
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
        
        public void RequestSpell(Action<int, ScoreEntry> onSpellSelect)
        {
            foreach (ScoreEntrySpell spell in spells)
            {
                spell.Activate(i =>
                               {
                                   foreach (ScoreEntrySpell s in spells)
                                   {
                                       s.Deactivate();
                                   }
                                   onSpellSelect?.Invoke(i, this);
                               });
            }
        }
    }
}