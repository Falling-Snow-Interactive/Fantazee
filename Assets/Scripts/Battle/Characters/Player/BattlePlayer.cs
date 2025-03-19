using System;
using System.Collections;
using System.Collections.Generic;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.Score;
using Fantazee.Battle.Score.Ui;
using Fantazee.Battle.Settings;
using Fantazee.Battle.Ui;
using Fantazee.Dice;
using Fantazee.Dice.Ui;
using Fantazee.Instance;
using Fantazee.Scores;
using Fantazee.Scores.Instance;
using FMODUnity;
using Fsi.Gameplay.Healths;
using UnityEngine;

namespace Fantazee.Battle.Characters.Player
{
    public class BattlePlayer : BattleCharacter
    {
        public event Action RollStarted;
        public event Action<ScoreResults> Scored;

        public event Action RollsChanged;
        
        // Ui Shortcuts
        private DiceControlUi DiceControl => BattleUi.Instance.DiceControl;

        [SerializeReference]
        private CharacterInstance instance;
        
        // Character shortcuts
        public override Health Health => instance.Health;
        
        [Header("Scores")]
        
        [SerializeReference]
        private List<BattleScore> battleScores = new();
        
        [SerializeReference]
        private BattleScore fantazeeBattleScore;
        
        [Header("Turn")]
        
        [SerializeField] 
        private int rollsRemaining = 3;
        public int RollsRemaining
        {
            get => rollsRemaining;
            set
            {
                rollsRemaining = value;
                RollsChanged?.Invoke();
            }
        }

        [SerializeReference]
        private readonly List<Die> lockedDice = new();
        public List<Die> LockedDice => lockedDice;

        [SerializeField]
        private bool hasScoredRoll = false;
        
        [SerializeField]
        private bool isRolling = false;
        
        // Audio
        protected override EventReference DeathSfxRef => instance.Data.DeathSfx;
        protected override EventReference EnterSfxRef => instance.Data.EnterSfx;
        
        // Callback receivers
        private List<IRollStartedCallbackReceiver> rollStartedReceivers = new();
        private List<IRollFinishedCallbackReceiver> rollFinishedReceivers = new();
        private List<IDieRolledCallbackReceivers> dieRolledCallbackReceivers = new();

        public void Initialize(CharacterInstance character)
        {
            instance = character;
            SpawnVisuals(character.Data.Visuals);
            
            // Setup scores and UI
            List<ScoreInstance> scoreList = character.Scoresheet.Scores;
            foreach (ScoreInstance score in scoreList)
            {
                BattleScore bs = new(score);
                battleScores.Add(bs);
            }
            
            fantazeeBattleScore = new BattleScore(character.Scoresheet.Fantazee);
            BattleUi.Instance.Scoresheet.Initialize(battleScores, fantazeeBattleScore, OnSelectScoreButton);

            SetupDice();
            
            base.Initialize();

            name = ToString();
        }
        
        private void SetupDice()
        {
            Debug.Log($"BattlePlayer - Setup Dice");
            for (int i = 0; i < instance.Dice.Count; i++)
            {
                Die die = instance.Dice[i];
                die.Roll();
                if (BattleUi.Instance.DiceControl.Dice.Count > i)
                {
                    DieUi dieUi = BattleUi.Instance.DiceControl.Dice[i];
                    dieUi.Initialize(die);

                    dieUi.Hide(null, 0, true);
                }
            }
        }

        public override void EndTurn()
        {
            DiceControl.HideDice(null);
            base.EndTurn();
        }

        protected override void CharacterStartTurn()
        {
            // Check if there's any score unscored. If all have been scored, take a turn to reset.
            Debug.Log("BattlePlayer: Start Turn");
            bool canPlay = fantazeeBattleScore.CanScore();
            if (!canPlay)
            {
                foreach (BattleScore battleScore in battleScores)
                {
                    if (battleScore.CanScore())
                    {
                        canPlay = true;
                        break;
                    }
                }
            }

            if (canPlay)
            {
                RollsRemaining = instance.Rolls;
                lockedDice.Clear();
                
                TryRoll();
            }
            else
            {
                ResetScores(EndTurn);
            }
        }

        private void OnSelectScoreButton(BattleScoreButton battleScoreButton)
        {
            if (hasScoredRoll || isRolling)
            {
                return;
            }
            
            hasScoredRoll = true;
            
            if (battleScoreButton.BattleScore.CanScore())
            {
                StartCoroutine(StartScoreSequence(battleScoreButton, BattleUi.Instance.DiceControl.Dice));
            }
        }
        
        private IEnumerator StartScoreSequence(BattleScoreButton button, List<DieUi> diceUi)
        {
            BattleSettings settings = BattleSettings.Settings;
            
            foreach (DieUi d in diceUi)
            {
                d.ResetDice();
                d.Squish();
                button.BattleScore.AddDie(d.Die);
                
                yield return new WaitForSeconds(settings.ScoreTime);
            }
            
            yield return new WaitForSeconds(0.5f);
            
            // Calculate score
            ScoreResults scoreResults = new(button.BattleScore.Score, button.BattleScore.Dice);
            bool ready = false;
            StartCoroutine(CallScoreReceivers(scoreResults, sr =>
                                                            {
                                                                scoreResults = sr;
                                                                ready = true;
                                                            }));
            yield return new WaitUntil(() => ready);
            button.FinalizeScore(scoreResults.Value);
            
            if (scoreResults.Value > 0)
            {
                yield return new WaitForSeconds(0.5f);
                button.BattleScore.Cast(scoreResults, () =>
                                                      {
                                                          OnFinishedScoring(scoreResults);
                                                      });
            }
            else
            {
                button.FinalizeScore(0);
                OnFinishedScoring(scoreResults);
            }
        }
        
        
        private void OnFinishedScoring(ScoreResults results)
        {
            if (!BattleController.Instance.CheckEnemiesAlive())
            {
                BattleController.Instance.PlayerWin();
                return;
            }
            
            lockedDice.Clear();
            Scored?.Invoke(results);
            if (RollsRemaining > 0)
            {
                DiceControl.ResetDice();
                TryRoll();
            }
            else
            {
                EndTurn();
            }
        }
        
        private void ResetScores(Action onComplete)
        {
            foreach (BattleScore battleScore in battleScores)
            {
                battleScore.ResetScore();
            }
            fantazeeBattleScore.ResetScore();
            
            onComplete?.Invoke();
        }
        
        #region Dice Control

        public void TryRoll()
        {
            if (RollsRemaining > 0 && !isRolling)
            {
                hasScoredRoll = false;
                RollsRemaining--;
                foreach (Die d in instance.Dice)
                {
                    if(!lockedDice.Contains(d))
                    {
                        d.Roll();
                    }
                }

                isRolling = true;
                StartCoroutine(CallRollStartedReceivers(null));
                DiceControl.Roll(this, d =>
                                       {
                                           StartCoroutine(CallDieRolledReceivers(d, null));
                                       },
                                 () =>
                                 {
                                     StartCoroutine(CallRollFinishedReceivers(() =>
                                                                              {
                                                                                  isRolling = false;
                                                                              }));
                                 });
                RollStarted?.Invoke();
            }
        }
        
        #endregion
        
        #region Callback receivers
        
        public void RegisterRollStartedReceiver(IRollStartedCallbackReceiver callbackReceiver)
        {
            rollStartedReceivers.Add(callbackReceiver);
        }

        public void UnregisterRollStartedReceiver(IRollStartedCallbackReceiver callbackReceiver)
        {
            rollStartedReceivers.Remove(callbackReceiver);
        }

        public IEnumerator CallRollStartedReceivers(Action onComplete)
        {
            foreach (IRollStartedCallbackReceiver receiver in rollStartedReceivers)
            {
                if (receiver == null)
                {
                    continue;
                }

                bool ready = false;
                receiver.OnRollStarted(() => ready = true);
                yield return new WaitUntil(() => ready);
            }

            yield return new WaitForSeconds(0.5f);
            onComplete?.Invoke();
        }
        
        public void RegisterRollFinishedReceiver(IRollFinishedCallbackReceiver callbackReceiver)
        {
            rollFinishedReceivers.Add(callbackReceiver);
        }

        public void UnregisterRollFinishedReceiver(IRollFinishedCallbackReceiver callbackReceiver)
        {
            rollFinishedReceivers.Remove(callbackReceiver);
        }

        public IEnumerator CallRollFinishedReceivers(Action onComplete)
        {
            foreach (IRollFinishedCallbackReceiver receiver in rollFinishedReceivers)
            {
                if (receiver == null)
                {
                    continue;
                }

                bool ready = false;
                receiver.OnRollFinished(() => ready = true);
                yield return new WaitUntil(() => ready);
            }

            yield return new WaitForSeconds(0.5f);
            onComplete?.Invoke();
        }
        
        public void RegisterDieRolledReceiver(IDieRolledCallbackReceivers callbackReceiver)
        {
            dieRolledCallbackReceivers.Add(callbackReceiver);
        }

        public void UnregisterDieRolledReceiver(IDieRolledCallbackReceivers callbackReceiver)
        {
            dieRolledCallbackReceivers.Remove(callbackReceiver);
        }

        public IEnumerator CallDieRolledReceivers(Die die, Action onComplete)
        {
            foreach (IDieRolledCallbackReceivers receiver in dieRolledCallbackReceivers)
            {
                if (receiver == null)
                {
                    continue;
                }

                bool ready = false;
                receiver.OnDieRolled(die, () => ready = true);
                yield return new WaitUntil(() => ready);
            }

            yield return new WaitForSeconds(0.5f);
            onComplete?.Invoke();
        }
        
        #endregion

        public override string ToString()
        {
            string s = $"BattlePlayer - {instance}";
            return base.ToString();
        }
    }
}