using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Settings;
using Fantazee.Dice;
using Fantazee.Dice.Settings;
using Fantazee.Instance;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Spells.Instance;
using FMODUnity;
using UnityEngine;

namespace Fantazee.Battle.Score.Ui
{
    public class BattleScoreEntry : ScoreEntry
    {
        [SerializeReference]
        private BattleScore battleScore;
        public BattleScore BattleScore => battleScore;

        [SerializeField]
        private bool isFinalized = false;
        public bool IsFinalized => isFinalized;
        
        private void OnEnable()
        {
            if (battleScore != null)
            {
                battleScore.DieAdded += OnDieAdded;
                battleScore.ScoreReset += OnBattleScoreReset;
                battleScore.SpellCastStart += OnSpellCastStart;
            }
        }

        private void OnDisable()
        {
            if (battleScore != null)
            {
                battleScore.DieAdded -= OnDieAdded;
                battleScore.ScoreReset -= OnBattleScoreReset;
                battleScore.SpellCastStart -= OnSpellCastStart;
            }
        }

        public void Initialize(BattleScore battleScore, Action<ScoreEntry> onSelect)
        {
            this.battleScore = battleScore;
            base.Initialize(battleScore.Score, onSelect);
            
            battleScore.DieAdded += OnDieAdded;
            battleScore.ScoreReset += OnBattleScoreReset;

            isFinalized = false;
            scoreText.gameObject.SetActive(false);
        }

        protected override List<int> GetDiceValues()
        {
            return new List<int>{0,0,0,0,0};
        }

        public int FinalizeScore()
        {
            isFinalized = true;
            int s = battleScore.Calculate();
            button.interactable = false;
            scoreText.gameObject.SetActive(true);
            scoreText.text = s.ToString();
            RuntimeManager.PlayOneShot(BattleSettings.Settings.ScoreSfx);
            scoreContainer.transform.DOPunchScale(DiceSettings.Settings.SquishAmount, 
                                                  DiceSettings.Settings.SquishTime,
                                                  10,
                                                  1f)
                          .SetEase(DiceSettings.Settings.SquishEase);

            return s;
        }

        public void ShowPreview()
        {
            previewText.gameObject.SetActive(true);
            int score = battleScore.Calculate(GameInstance.Current.Character.Dice);
            string s = $"Score {score.GetType().DeclaringType}: {score} | ";
            foreach (Die d in GameInstance.Current.Character.Dice)
            {
                s += $"{d.Value}";
                if (d != GameInstance.Current.Character.Dice[^1])
                {
                    s += " - ";
                }
            }
            Debug.Log($"{s}");
            previewText.text = score.ToString();
        }

        public void HidePreview()
        {
            previewText.gameObject.SetActive(false);
        }
        
        private void OnDieAdded()
        {
            for (int i = 0; i < battleScore.Dice.Count; i++)
            {
                ShowDieInSlot(i, battleScore.Dice[i].Value);
            }
        }

        private void OnBattleScoreReset()
        {
            isFinalized = false;
            for (int i = 0; i < diceImages.Count; i++)
            {
                int v = battleScore.Dice.Count > i ? battleScore.Dice[i].Value : 0;
                ShowDieInSlot(i, v);
            }

            button.interactable = battleScore.Dice.Count == 0;
            scoreText.text = battleScore.Dice.Count == 0 ? "" : battleScore.Calculate().ToString();
        }

        private void OnSpellCastStart(SpellInstance spell)
        {
            foreach (ScoreEntrySpell s in Spells)
            {
                if (s.Spell == spell)
                {
                    s.Punch();
                }
            }
        }
    }
}
