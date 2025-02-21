using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Battle.Characters.Player;
using Fantazee.Battle.Scores;
using Fantazee.Battle.Scores.Ui;
using Fantazee.Battle.Settings;
using Fantazee.Battle.Ui;
using Fantazee.Boons;
using Fantazee.Boons.Handlers;
using Fantazee.Currencies;
using Fantazee.Dice;
using Fantazee.Items.Dice.Ui;
using FMODUnity;
using Fsi.Gameplay;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Battle
{
    public class BattleController : MbSingleton<BattleController>
    {
        public static event Action PlayerTurnStart;
        public static event Action PlayerTurnEnd;
        
        public static event Action RollStarted;
        public static event Action<Die> DieRolled;
        
        public static event Action<int> DiceScored;
        public static event Action<int> Scored;
        
        [Header("Camera")]
        
        [SerializeField]
        private new Camera camera;
        private Camera Camera
        {
            get
            {
                if (camera == null)
                {
                    camera = Camera.main;
                }

                return camera;
            }
        }

        [Header("Roll")]

        [SerializeField]
        private int rolls = 5;
        public int Rolls => rolls;

        [SerializeField]
        private int remainingRolls = 3;
        public int RemainingRolls => remainingRolls;

        private List<Die> lockedDice = new();
        public List<Die> LockedDice => lockedDice;

        private bool hasScoredRoll = false;

        [FormerlySerializedAs("score")]
        [Header("Score")]

        [SerializeField]
        private ScoreTracker scoreTracker;
        public ScoreTracker ScoreTracker => scoreTracker;

        [Header("Characters")]

        [SerializeField]
        private GameplayPlayer gameplayPlayer;
        public GameplayPlayer Player => gameplayPlayer;

        [SerializeField]
        private List<GameplayEnemy> enemies = new();
        public List<GameplayEnemy> Enemies => enemies;

        [SerializeField]
        private Transform enemyContainer;

        [SerializeField]
        private List<GameplayEnemy> enemyPool = new();

        [SerializeField]
        private RangeInt enemySpawns = new(3, 5);

        [Header("Battle Rewards")]
        
        [SerializeField]
        private BattleRewards battleRewards;

        [Header("Scoring")]

        [Header("Animation")]

        [SerializeField]
        private float scoreTime = 0.2f;
        
        [SerializeField]
        private Ease scoreEase = Ease.Linear;

        [Header("Sfx")]

        [SerializeField]
        private EventReference diceScoreSfx;
        
        // Boon Control
        private readonly List<IBoonDamageHandler> boonDamageHandlers = new();
        private readonly List<IBoonRollHandler> boonRollHandlers = new();
        
        private void OnEnable()
        {
            GameplayCharacter.Despawned += OnCharacterDespawned;
        }

        private void OnDisable()
        {
            GameplayCharacter.Despawned -= OnCharacterDespawned;
        }

        private void Start()
        {
            Debug.Log($"Battle - Start");
            SetupBattle();
            Debug.Log($"Battle - Ready");
            GameController.Instance.BattleReady();
        }

        public void BattleStart()
        {
            StartIntroduction(OnIntroductionFinished);
        }
        
        #region Setup Battle

        private void SetupBattle()
        {
            Debug.Log($"Battle - Setup");
            scoreTracker.Initialize();
            Player.Initialize();
            SetupDice();
            SetupEnemies();

            SetupBoons();
            
            // Hide enemies
            foreach (GameplayEnemy enemy in enemies)
            {
                enemy.Hide(null, 0, true);
            }
            
            // Hide player
            Player.Hide(null, 0, true);

        }

        private void SetupDice()
        {
            Debug.Log($"Battle - Setup Dice");
            for (int i = 0; i < GameController.Instance.GameInstance.Dice.Count; i++)
            {
                Die die = GameController.Instance.GameInstance.Dice[i];
                die.Roll();
                if (BattleUi.Instance.DiceControl.Dice.Count > i)
                {
                    DieUi dieUi = BattleUi.Instance.DiceControl.Dice[i];
                    dieUi.Initialize(die);

                    dieUi.Hide(null, 0, true);
                }
            }
        }

        private void SetupEnemies()
        {
            int spawns = enemySpawns.Random();
            float spawnOffset = 0;
            for (int i = 0; i < spawns; i++)
            {
                GameplayEnemy enemy = Instantiate(enemyPool[Random.Range(0, enemyPool.Count)], enemyContainer);
                
                float y = i % 2 == 0 ? + 0.5f : -0.5f;
                
                enemy.transform.localPosition += Vector3.left * spawnOffset + Vector3.forward * y;
                spawnOffset += enemy.Size;

                enemy.Initialize();

                enemies.Add(enemy);
            }
        }

        private void SetupBoons()
        {
            foreach (Boon boon in GameController.Instance.GameInstance.Boons)
            {
                BattleUi.Instance.BoonsUi.AddBoon(boon);
                if (boon is IBoonDamageHandler boonDamageHandler)
                {
                    boonDamageHandlers.Add(boonDamageHandler);
                }

                if (boon is IBoonRollHandler boonRollHandler)
                {
                    boonRollHandlers.Add(boonRollHandler);
                }
            }
        }
        
        #endregion
        
        #region Introduce

        private void StartIntroduction(Action onComplete = null)
        {
            StartCoroutine(IntroductionSequence(onComplete));
        }

        private IEnumerator IntroductionSequence(Action onComplete = null)
        {
            // Show player
            Player.Show(null);
            yield return new WaitForSeconds(0.2f);
            
            // Move enemies in
            foreach (GameplayEnemy enemy in enemies)
            {
                enemy.Show(null);
                yield return new WaitForSeconds(0.2f);
            }
            
            onComplete?.Invoke();
        }

        private void OnIntroductionFinished()
        {
            StartPlayerTurn();
        }
        
        #endregion
        
        #region Score/Damage
        
        public void SelectScoreEntry(ScoreEntry entry)
        {
            if (hasScoredRoll)
            {
                return;
            }
            
            hasScoredRoll = true;
            
            if (scoreTracker.CanScore(entry.Score.Type))
            {
                StartCoroutine(StartScoreSequence(entry, BattleUi.Instance.DiceControl.Dice));
            }
        }

        private IEnumerator StartScoreSequence(ScoreEntry entry, List<DieUi> diceUi)
        {
            Score score = entry.Score;
            List<Die> dice = GameController.Instance.GameInstance.Dice;
            List<Die> partOfScore = entry.Score.GetScoredDice(dice);
            
            // First, dice go to scoreboard
            List<Die> scoredDice = new();
            for (int i = 0; i < diceUi.Count; i++)
            {
                DieUi d = diceUi[i];
                
                bool inScore = partOfScore.Contains(d.Die);
                scoredDice.Add(d.Die);
                
                
                d.Image.transform.DOPunchScale(BattleSettings.Settings.SquishAmount, 
                                               BattleSettings.Settings.SquishTime, 
                                               10, 
                                               1f)
                 .SetEase(BattleSettings.Settings.SquishEase);
                RuntimeManager.PlayOneShot(diceScoreSfx);
                entry.SetDice(i, d.Die.Value, inScore);

                if (inScore)
                {
                    int s = entry.Score.Calculate(scoredDice);
                    entry.SetScore(s);
                    DiceScored?.Invoke(d.Die.Value); 
                }
                
                yield return new WaitForSeconds(scoreTime);
            }
            
            yield return new WaitForSeconds(0.5f);

            // Calculate damage
            
            int diceScore = score.Calculate(dice);
            Damage damage = new(diceScore);
            
            if (damage.Value > 0)
            {
                foreach (IBoonDamageHandler boon in boonDamageHandlers)
                {
                    boon.ReceiveDamage(ref damage);
                    boon.Boon.entryUi.Punch();
                    entry.SetScore(damage.Value);
                    
                    yield return new WaitForSeconds(0.5f);
                }

                yield return new WaitForSeconds(0.5f);

                // TODO - Somwthing weird going on with four of a kind
                ScoreTracker.AddScore(score, damage.Value);
                entry.SetScore(damage.Value);
                
                yield return new WaitForSeconds(0.5f);
            
                // Do attack
                bool ready = false;
                gameplayPlayer.PerformAttack(() =>
                                             {
                                                 ready = true;
                                             });
            
                yield return new WaitUntil(() => ready);

                RuntimeManager.PlayOneShot(gameplayPlayer.AttackHitSfx);
                enemies[^1].Damage(damage.Value);
                Scored?.Invoke(damage.Value);
            }
            else
            {
                ScoreTracker.AddScore(score, 0);
                entry.SetScore(0);
                Scored?.Invoke(0);
            }
            
            yield return new WaitForSeconds(0.5f);
            
            OnFinishedScoring();
        }

        private void OnFinishedScoring()
        {
            lockedDice.Clear();
            if (remainingRolls > 0)
            {
                BattleUi.Instance.DiceControl.ResetDice();
            }
            else
            {
                BattleUi.Instance.DiceControl.HideDice();
            }

            if (CheckEnemiesAlive())
            {
                TryRoll();
                return;
            }

            BattleWin();
        }

        public bool CheckEnemiesAlive()
        {
            foreach (GameplayEnemy enemy in enemies)
            {
                if (enemy.Health.IsAlive)
                {
                    return true;
                }
            }

            return false;
        }

        public void CheckWin()
        {
            if (!CheckEnemiesAlive())
            {
                BattleWin();
            }
        }
        
        #endregion
        
        #region Dice Control

        public void TryRoll()
        {
            if (remainingRolls > 0)
            {
                hasScoredRoll = false;
                remainingRolls--;
                foreach (Die d in GameController.Instance.GameInstance.Dice)
                {
                    if(!lockedDice.Contains(d))
                    {
                        d.Roll();
                    }
                }
                
                BattleUi.Instance.DiceControl.Roll(d =>
                                                     {
                                                         foreach (IBoonRollHandler boon in boonRollHandlers)
                                                         {
                                                             boon.OnDiceRoll(d);
                                                         }
                                                         DieRolled?.Invoke(d);
                                                     });
                RollStarted?.Invoke();
            }
        }
        
        #endregion
        
        #region Character Control
        
        private void OnCharacterDespawned(GameplayCharacter character)
        {
            if (character is GameplayEnemy enemy)
            {
                enemies.Remove(enemy);
            }
        }
        
        #endregion
        
        #region Player Turn
        
        private void StartPlayerTurn()
        {
            PlayerTurnStart?.Invoke();
            remainingRolls = rolls;
            lockedDice.Clear();
            BattleUi.Instance.DiceControl.ShowDice(TryRoll);
        }
        
        public void TryEndPlayerTurn()
        {
            BattleUi.Instance.DiceControl.HideDice();
            PlayerTurnEnd?.Invoke();
            StartEnemyTurn();
        }

        public void TryBonusAttack()
        {
            if (scoreTracker.BonusScore.IsReady)
            {
                // Player.DoBonusAttack();
                BattleUi.Instance.Scoreboard.BonusScoreUi.Disable();
            }
        }
        
        #endregion
        
        #region Enemy Turn

        private void StartEnemyTurn()
        {
            StartCoroutine(EnemyTurnSequence());
        }

        private IEnumerator EnemyTurnSequence()
        {
            Queue<GameplayEnemy> enemyQueue = new(enemies);
            while (enemyQueue.Count > 0)
            {
                GameplayEnemy curr = enemyQueue.Dequeue();
                bool attacking = true;
                curr.Attack(() => attacking = false);
                yield return new WaitUntil(() => !attacking);
            }
            
            Debug.Log("Finished enemy turns");
            StartPlayerTurn();
        }
        
        #endregion
        
        #region Battle End
        
        protected virtual void BattleWin()
        {
            BattleUi.Instance.ShowWinScreen();
            BattleUi.Instance.WinScreen.Initialize(battleRewards, OnBattleWinContinue);
        }

        private void OnBattleWinContinue()
        {
            GrantRewards();
            
            GameController.Instance.FinishedBattle(true);
        }

        private void GrantRewards()
        {
            foreach (Currency currency in battleRewards.Currencies)
            {
                GameController.Instance.GameInstance.Wallet.Add(currency);
            }
        }
        
        #endregion
        
        #region Boon Handlers

        public void RegisterBoonDamageHandlerCallback(IBoonDamageHandler hander)
        {
            boonDamageHandlers.Add(hander);
        }

        public void UnregisterBoonDamageHandlerCallback(IBoonDamageHandler handler)
        {
            boonDamageHandlers.Remove(handler);
        }

        public void RegisterBoonRollHandlerCallback(IBoonRollHandler handler)
        {
            boonRollHandlers.Add(handler);
        }

        public void UnregisterBoonRollHandlerCallback(IBoonRollHandler handler)
        {
            boonRollHandlers.Remove(handler);
        }
        
        #endregion
    }
}
