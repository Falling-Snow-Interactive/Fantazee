using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Settings;
using Fantazee.Dice.Settings;
using Fantazee.Instance;
using Fantazee.Items.Dice.Information;
using Fantazee.Scores;
using Fantazee.Scores.Ui.Buttons;
using Fantazee.Spells;
using Fantazee.Spells.Ui;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Battle.Score.Ui
{
    public class BattleScoreButton : MonoBehaviour
    {
        [SerializeField]
        private ScoreButton scoreButton;
        
        private BattleScore battleScore;
        public BattleScore BattleScore => battleScore;

        [SerializeField]
        private bool isFinalized = false;
        public bool IsFinalized => isFinalized;
        
        [Header("Dice")]
        
        [SerializeField]
        private List<Image> diceImages = new();

        [Header("Animation")]

        [SerializeField]
        private float scoreSquishAmount = 0.5f;

        [SerializeField]
        private float scoreSquishTime = 0.2f;

        [Header("References")]

        [SerializeField]
        private Transform scoreContainer;
        
        [SerializeField]
        private TMP_Text scoreText;

        [SerializeField]
        private TMP_Text previewText;
        
        private void OnEnable()
        {
            if (battleScore != null)
            {
                battleScore.DieAdded += OnDieAdded;
                battleScore.ScoreReset += OnBattleScoreReset;
                battleScore.SpellCastStart += OnSpellCastStart;
            }
            
            if (BattleController.Instance && BattleController.Instance.Player)
            {
                BattleController.Instance.Player.RollFinished += OnRollFinished;
                BattleController.Instance.Player.RollStarted += OnRollStarted;
            }
        }

        private void OnDisable()
        {
            if (BattleController.Instance && battleScore != null)
            {
                battleScore.DieAdded -= OnDieAdded;
                battleScore.ScoreReset -= OnBattleScoreReset;
                battleScore.SpellCastStart -= OnSpellCastStart;
            }

            if (BattleController.Instance.Player)
            {
                BattleController.Instance.Player.RollFinished -= OnRollFinished;
                BattleController.Instance.Player.RollStarted -= OnRollStarted;
            }
        }

        public void Initialize(BattleScore battleScore, Action<BattleScoreButton> onSelect)
        {
            this.battleScore = battleScore;
            scoreButton.Initialize(battleScore.Score, _ =>
                                                      {
                                                          onSelect?.Invoke(this);
                                                      });
            
            battleScore.DieAdded += OnDieAdded;
            battleScore.ScoreReset += OnBattleScoreReset;
            battleScore.SpellCastStart += OnSpellCastStart;

            BattleController.Instance.Player.RollFinished += OnRollFinished;
            BattleController.Instance.Player.RollStarted += OnRollStarted;
            
            isFinalized = false;
            scoreText.gameObject.SetActive(false);
            previewText.gameObject.SetActive(false);

            List<int> vs = GetDiceValues();
            for (int i = 0; i < vs.Count; i++)
            {
                int v = vs[i];
                ShowDieInSlot(i, v);
            }
        }

        private List<int> GetDiceValues()
        {
            return new List<int>{0,0,0,0,0};
        }

        public void FinalizeScore(int score)
        {
            isFinalized = true;
            scoreButton.Button.interactable = false;
            scoreText.gameObject.SetActive(true);
            scoreText.text = score.ToString();
            RuntimeManager.PlayOneShot(BattleSettings.Settings.ScoreSfx);
            scoreContainer.transform.DOPunchScale(Vector3.one * scoreSquishAmount, 
                                                  scoreSquishTime,
                                                  10,
                                                  1f)
                          .SetEase(DiceSettings.Settings.SquishEase);
            previewText.gameObject.SetActive(false);
        }

        private void ShowPreview()
        {
            if (isFinalized)
            {
                return;
            }
            
            previewText.gameObject.SetActive(true);

            ScoreResults results = new(battleScore.Score, GameInstance.Current.Character.Dice);
            results = BattleController.Instance.Player.CheckScoreReceivers(results);
            previewText.text = results.Value.ToString();
        }
        
        private void HidePreview()
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

        private void ShowDieInSlot(int slot, int value)
        {
            if (DiceSettings.Settings.SideInformation.TryGetInformation(value, out SideInformation info))
            {
                diceImages[slot].sprite = info.Sprite;
            }
            else if(DiceSettings.Settings.SideInformation.TryGetInformation(0, out SideInformation info2))
            {
                diceImages[slot].sprite = info2.Sprite;
            }
            else
            {
                diceImages[slot].sprite = null;
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

            scoreButton.Button.interactable = battleScore.Dice.Count == 0;
            scoreText.text = battleScore.Dice.Count == 0 ? "" : battleScore.Calculate().ToString();
            
            transform.DOShakeRotation(0.3f, Vector3.one * 4f, 10, 90f, true, ShakeRandomnessMode.Full)
                     .SetEase(Ease.Linear);
        }

        private void OnSpellCastStart(SpellInstance spell)
        {
            foreach (SpellButton s in scoreButton.Spells)
            {
                if (s.Spell == spell)
                {
                    s.Punch();
                }
            }
        }

        private void OnRollFinished()
        {
            ShowPreview();
        }

        private void OnRollStarted()
        {
            HidePreview();
        }
    }
}
