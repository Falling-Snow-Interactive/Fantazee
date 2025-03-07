using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Dice.Settings;
using Fantazee.Items.Dice.Information;
using Fantazee.Scores.Information;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Scores.Ui.ScoreEntries
{
    public abstract class ScoreEntry : MonoBehaviour
    {
        private Action<ScoreEntry> onSelect;
        
        [SerializeReference]
        private Score score;
        public Score Score
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

        public virtual void Initialize(Score score, Action<ScoreEntry> onSelect)
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
            nameText.text = score.Information.LocName.GetLocalizedString();
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

            switch (score.Type)
            {
                case ScoreType.Ones:
                    diceValues = new List<int> { 1,1,1,1,1 };
                    break;
                case ScoreType.Twos:
                    diceValues = new List<int> { 2,2,2,2,2 };
                    break;
                case ScoreType.Threes:
                    diceValues = new List<int> { 3,3,3,3,3 };
                    break;
                case ScoreType.Fours:
                    diceValues = new List<int> { 4,4,4,4,4 };
                    break;
                case ScoreType.Fives:
                    diceValues = new List<int> { 5,5,5,5,5 };
                    break;
                case ScoreType.Sixes:
                    diceValues = new List<int> { 6,6,6,6,6 };
                    break;
                case ScoreType.ThreeOfAKind:
                    diceValues = new List<int> { 6,6,6,5,4 };
                    break;
                case ScoreType.FourOfAKind:
                    diceValues = new List<int> { 6,6,6,6,5 };
                    break;
                case ScoreType.FullHouse:
                    diceValues = new List<int> { 5,5,5,2,2 };
                    break;
                case ScoreType.SmallRun:
                    diceValues = new List<int> { 4,5,6,1,1 };
                    break;
                case ScoreType.LargeRun:
                    diceValues = new List<int> { 3,4,5,6,1 };
                    break;
                case ScoreType.Chance:
                    diceValues = new List<int> { 6,4,5,1,5 };
                    break;
                case ScoreType.Fantazee:
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