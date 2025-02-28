using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Battle.Characters.Player;
using Fantazee.Battle.Score;
using Fantazee.Battle.Score.Ui;
using Fantazee.Battle.Ui;
using Fantazee.Currencies;
using Fantazee.Dice;
using Fantazee.Dice.Ui;
using Fantazee.Instance;
using Fantazee.Scores.Ui;
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
        
        // Common instance references
        
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
        private int remainingRolls = 3;
        public int RemainingRolls => remainingRolls;

        private readonly List<Die> lockedDice = new();
        public List<Die> LockedDice => lockedDice;

        private bool hasScoredRoll = false;

        [FormerlySerializedAs("gameplayPlayer")]
        [Header("Characters")]

        [SerializeField]
        private BattlePlayer battlePlayer;
        public BattlePlayer Player => battlePlayer;

        [SerializeReference]
        private List<BattleEnemy> enemies = new();
        public List<BattleEnemy> Enemies => enemies;

        [SerializeField]
        private Transform enemyContainer;

        [SerializeField]
        private List<BattleEnemy> enemyPool = new();

        [SerializeField]
        private RangeInt enemySpawns = new(3, 5);

        [SerializeReference]
        private BattleRewards rewards;
        
        [Header("Battle Scores")]
        
        [SerializeField]
        private List<BattleScore> battleScores = new();
        public List<BattleScore> BattleScores => battleScores;

        [SerializeField]
        private BattleScore fantazeeBattleScore;
        
        [Header("Scoring")]

        [Header("Animation")]

        [SerializeField]
        private float scoreTime = 0.2f;
        
        [SerializeField]
        private Ease scoreEase = Ease.Linear;
        
        private void OnEnable()
        {
            BattleCharacter.Despawned += OnCharacterDespawned;
        }

        private void OnDisable()
        {
            BattleCharacter.Despawned -= OnCharacterDespawned;
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

            List<Scores.Score> scoreList = GameInstance.Current.Character.ScoreTracker.Scores;
            foreach (Scores.Score score in scoreList)
            {
                BattleScore bs = new(score);
                battleScores.Add(bs);
            }
            
            fantazeeBattleScore = new FantazeeBattleScore(GameInstance.Current.Character.ScoreTracker.Fantazee);
            BattleUi.Instance.Scoreboard.Initialize(battleScores, fantazeeBattleScore, SelectScoreEntry);
            
            Player.Initialize();
            SetupDice();
            SetupEnemies();
            
            // Hide enemies
            foreach (BattleEnemy enemy in enemies)
            {
                enemy.Hide(null, 0, true);
            }
            
            // Hide player
            Player.Hide(null, 0, true);
        }

        private void SetupDice()
        {
            Debug.Log($"Battle - Setup Dice");
            for (int i = 0; i < GameController.Instance.GameInstance.Character.Dice.Count; i++)
            {
                Die die = GameController.Instance.GameInstance.Character.Dice[i];
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
            rewards = new BattleRewards();
            int spawns = enemySpawns.Random();
            float spawnOffset = 0;
            for (int i = 0; i < spawns; i++)
            {
                BattleEnemy enemy = Instantiate(enemyPool[Random.Range(0, enemyPool.Count)], enemyContainer);
                
                float y = i % 2 == 0 ? + 0.5f : -0.5f;
                
                enemy.transform.localPosition += Vector3.left * spawnOffset + Vector3.forward * y;
                spawnOffset += enemy.Size;

                enemy.Initialize();
                rewards.Add(enemy.BattleRewards);

                enemies.Add(enemy);
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
            foreach (BattleEnemy enemy in enemies)
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
        
        private void SelectScoreEntry(ScoreEntry entry)
        {
            if (hasScoredRoll)
            {
                return;
            }
            
            hasScoredRoll = true;
            
            if (entry.Score.CanScore())
            {
                StartCoroutine(StartScoreSequence(entry, BattleUi.Instance.DiceControl.Dice));
            }
        }

        private IEnumerator StartScoreSequence(ScoreEntry entry, List<DieUi> diceUi)
        {
            foreach (DieUi d in diceUi)
            {
                d.ResetDice();
                d.Squish();
                entry.Score.AddDie(d.Die);
                
                yield return new WaitForSeconds(scoreTime);
            }
            
            yield return new WaitForSeconds(0.5f);
            
            // Calculate damage
            
            int s = entry.FinalizeScore();
            Damage damage = new(s);
            
            if (damage.Value > 0)
            {
                yield return new WaitForSeconds(0.5f);
                entry.Score.Cast(damage, () =>
                                         {
                                             Scored?.Invoke(damage.Value);
                                             OnFinishedScoring();
                                         });
            }
            else
            {
                entry.FinalizeScore();
                Scored?.Invoke(0);
                OnFinishedScoring();
            }
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
            foreach (BattleEnemy enemy in enemies)
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
                foreach (Die d in GameController.Instance.GameInstance.Character.Dice)
                {
                    if(!lockedDice.Contains(d))
                    {
                        d.Roll();
                    }
                }
                
                BattleUi.Instance.DiceControl.Roll(d =>
                                                     {
                                                         DieRolled?.Invoke(d);
                                                     });
                RollStarted?.Invoke();
            }
        }
        
        #endregion
        
        #region Character Control
        
        private void OnCharacterDespawned(BattleCharacter character)
        {
            if (character is BattleEnemy enemy)
            {
                enemies.Remove(enemy);
            }
        }
        
        #endregion
        
        #region Player Turn
        
        private void StartPlayerTurn()
        {
            PlayerTurnStart?.Invoke();
            remainingRolls = GameInstance.Current.Character.Rolls;
            lockedDice.Clear();
            Player.StartTurn();
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
            Debug.Log("No Bonus Yet");
            // if (scoreTracker.BonusScore.IsReady)
            // {
            //     // Player.DoBonusAttack();
            //     BattleUi.Instance.Scoreboard.BonusScoreUi.Disable();
            // }
        }
        
        #endregion
        
        #region Enemy Turn

        private void StartEnemyTurn()
        {
            StartCoroutine(EnemyTurnSequence());
        }

        private IEnumerator EnemyTurnSequence()
        {
            Queue<BattleEnemy> enemyQueue = new(enemies);
            yield return new WaitForSeconds(0.5f);
            while (enemyQueue.Count > 0)
            {
                BattleEnemy curr = enemyQueue.Dequeue();
                bool attacking = true;
                curr.Attack(() => attacking = false);
                yield return new WaitUntil(() => !attacking);
                yield return new WaitForSeconds(0.5f);
            }
            
            Debug.Log("Finished enemy turns");
            StartPlayerTurn();
        }
        
        #endregion
        
        #region Battle End
        
        protected virtual void BattleWin()
        {
            DOTween.Complete(Player.transform);
            Player.transform.DOLocalMoveX(9, 0.5f).SetEase(Ease.InOutCubic)
                  .OnComplete(() =>
                              {
                                  BattleUi.Instance.ShowWinScreen();
                                  BattleUi.Instance.WinScreen.Initialize(rewards, OnBattleWinContinue);
                              });
            exiting = false;
        }

        private bool exiting = false;

        private void OnBattleWinContinue()
        {
            if (exiting)
            {
                return;
            }
            
            exiting = true;
            DOTween.Complete(Player.transform);
            Player.transform.DOLocalMoveX(20, 0.5f).SetEase(Ease.InOutCubic)
                  .OnComplete(() =>
                              {
                                  rewards.Grant();
                                  GameController.Instance.FinishedBattle(true);
                              });
        }
        
        #endregion
    }
}
