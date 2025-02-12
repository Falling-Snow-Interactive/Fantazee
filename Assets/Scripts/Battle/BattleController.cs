using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using Fsi.Gameplay;
using ProjectYahtzee.Battle.Characters;
using ProjectYahtzee.Battle.Characters.Enemies;
using ProjectYahtzee.Battle.Characters.Player;
using ProjectYahtzee.Battle.Scores;
using ProjectYahtzee.Battle.Scores.Ui;
using ProjectYahtzee.Battle.Settings;
using ProjectYahtzee.Battle.Ui;
using ProjectYahtzee.Battle.Ui.Dices;
using ProjectYahtzee.Boons;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace ProjectYahtzee.Battle
{
    public class BattleController : MbSingleton<BattleController>
    {
        public static event Action PlayerTurnStart;
        public static event Action PlayerTurnEnd;
        
        public static event Action RollStarted;
        public static event Action<Dices.Dice> DiceRolled;
        
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

        [Header("Scoring")]

        [Header("Animation")]

        [SerializeField]
        private float scoreTime = 0.2f;
        
        [SerializeField]
        private Ease scoreEase = Ease.Linear;

        [Header("Sfx")]

        [SerializeField]
        private EventReference diceScoreSfx;
        
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
            SetupBattle();
            StartIntroduction(OnIntroductionFinished);
        }
        
        #region Setup Battle

        private void SetupBattle()
        {
            scoreTracker.Initialize();
            Player.Initialize();
            SetupDice();
            SetupEnemies();

            SetupBoons();
        }

        private void SetupDice()
        {
            for (int i = 0; i < GameController.Instance.GameInstance.Dice.Count; i++)
            {
                Dices.Dice dice = GameController.Instance.GameInstance.Dice[i];
                if (GameplayUi.Instance.DiceControl.Dice.Count > i)
                {
                    DiceUi diceUi = GameplayUi.Instance.DiceControl.Dice[i];
                    diceUi.Initialize(dice);

                    diceUi.Hide(null, 0, true);
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
                GameplayUi.Instance.BoonsUi.AddBoon(boon);
            }
        }
        
        #endregion
        
        #region Introduce

        private void StartIntroduction(Action onComplete = null)
        {
            // Hide enemies
            foreach (GameplayEnemy enemy in enemies)
            {
                enemy.Hide(null, 0, true);
            }
            
            // Hide player
            Player.Hide(null, 0, true);

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
        
        #region Scoring
        
        public void SelectScoreEntry(ScoreEntry entry)
        {
            if (hasScoredRoll)
            {
                return;
            }
            
            hasScoredRoll = true;
            
            if (scoreTracker.CanScore(entry.Score.Type))
            {
                StartCoroutine(StartScoreSequence(entry, GameplayUi.Instance.DiceControl.Dice));
            }
        }

        private IEnumerator StartScoreSequence(ScoreEntry entry, List<DiceUi> diceUi)
        {
            Score score = entry.Score;
            List<Dices.Dice> dice = GameController.Instance.GameInstance.Dice;
            List<Dices.Dice> partOfScore = entry.Score.GetScoredDice(dice);
            
            // First, dice go to scoreboard
            List<Dices.Dice> scoredDice = new();
            for (int i = 0; i < diceUi.Count; i++)
            {
                DiceUi d = diceUi[i];
                
                bool inScore = partOfScore.Contains(d.Dice);
                scoredDice.Add(d.Dice);
                
                
                d.Image.transform.DOPunchScale(GameplaySettings.Settings.SquishAmount, 
                                               GameplaySettings.Settings.SquishTime, 
                                               10, 
                                               1f)
                 .SetEase(GameplaySettings.Settings.SquishEase);
                RuntimeManager.PlayOneShot(diceScoreSfx);
                entry.SetDice(i, d.Dice.Value, inScore);

                if (inScore)
                {
                    int s = entry.Score.Calculate(scoredDice);
                    entry.SetScore(s);
                    DiceScored?.Invoke(d.Dice.Value); 
                }
                
                yield return new WaitForSeconds(scoreTime);
            }
            
            yield return new WaitForSeconds(0.5f);

            // Get bonuses from boons
            

            int diceScore = score.Calculate(dice);
            if (diceScore > 0)
            {
                float s = diceScore;

                foreach (Boon boon in GameController.Instance.GameInstance.Boons)
                {
                    float b = boon.GetBonus();

                    if (b > 0)
                    {
                        s += b;
                        boon.entryUi.Punch();
                        entry.SetScore(Mathf.RoundToInt(s));

                        yield return new WaitForSeconds(0.5f);
                    }
                }

                yield return new WaitForSeconds(0.5f);

                int total = Mathf.RoundToInt(s);
                ScoreTracker.AddScore(score, total);
                entry.SetScore(total);
                
                yield return new WaitForSeconds(0.5f);
            
                // Do attack

                bool ready = false;
                gameplayPlayer.PerformAttack(() =>
                                             {
                                                 ready = true;
                                             });
            
                yield return new WaitUntil(() => ready);

                RuntimeManager.PlayOneShot(gameplayPlayer.AttackHitSfx);
                enemies[^1].Damage(total);
                Scored?.Invoke(total);
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
            foreach (DiceUi d in GameplayUi.Instance.DiceControl.Dice)
            {
                d.Dice.Locked = false;
                if (remainingRolls > 0)
                {
                    d.ResetDice();
                }
            }
            
            foreach (GameplayEnemy enemy in enemies)
            {
                if (enemy.Health.IsAlive)
                {
                    TryRoll();
                    return;
                }
            }
            
            BattleWin();
        }
        
        #endregion
        
        #region Dice Control

        public void TryRoll()
        {
            if (remainingRolls > 0)
            {
                hasScoredRoll = false;
                remainingRolls--;
                foreach (Dices.Dice d in GameController.Instance.GameInstance.Dice)
                {
                    if (!d.Locked)
                    {
                        d.Roll();
                    }
                }
                
                GameplayUi.Instance.DiceControl.Roll(d =>
                                                     {
                                                         DiceRolled?.Invoke(d);
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
            foreach (Dices.Dice d in GameController.Instance.GameInstance.Dice)
            {
                d.Locked = false;
            }
            TryRoll();
        }
        
        public void TryEndPlayerTurn()
        {
            PlayerTurnEnd?.Invoke();
            StartEnemyTurn();
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
        
        private void BattleWin()
        {
            ProjectSceneManager.Instance.LoadMap();
        }
        
        #endregion
    }
}
