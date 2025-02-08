using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Fsi.Gameplay;
using ProjectYahtzee.Battle.Characters;
using ProjectYahtzee.Battle.Characters.Enemies;
using ProjectYahtzee.Battle.Characters.Player;
using ProjectYahtzee.Battle.Scores;
using ProjectYahtzee.Battle.Scores.Ui;
using ProjectYahtzee.Battle.Ui;
using ProjectYahtzee.Battle.Ui.Dices;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace ProjectYahtzee.Battle
{
    public class BattleController : MbSingleton<BattleController>
    {
        public static event Action Rolled;

        public static event Action<int> DiceScored;
        
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

        [SerializeField]
        private Transform enemyContainer;

        [SerializeField]
        private List<GameplayEnemy> enemyPool = new();

        [SerializeField]
        private RangeInt enemySpawns = new(3, 5);

        [Header("Scoring")]

        [Header("Animation")]

        [SerializeField]
        private float scoreTime = 0.3f;
        
        [SerializeField]
        private Ease scoreEase = Ease.Linear;

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
            if (scoreTracker.CanScore(entry.Score.Type))
            {
                int value = scoreTracker.AddScore(entry.Score, GameController.Instance.GameInstance.Dice);
                PlayScoreSequence(entry,
                                  GameplayUi.Instance.DiceControl.Dice,
                                  () =>
                                  {
                                      GameplayUi.Instance.Scoreboard
                                                .SetScore(entry.Score.Type, GameController.Instance.GameInstance.Dice);

                                      OnFinishedScoring(value);
                                  });
            }
        }

        private void PlayScoreSequence(ScoreEntry entry, List<DiceUi> dice, Action onComplete = null)
        {
            Sequence sequence = DOTween.Sequence();

           const  float delay = 0.3f;

            for (int i = 0; i < dice.Count; i++)
            {
                DiceUi d = dice[i];
                int iCached = i;
                float delayTime = delay * i;
                
                DiceScored?.Invoke(d.Dice.Value);

                Vector3 destination = entry.DiceImages[i].transform.position;
                TweenerCore<Vector3, Vector3, VectorOptions> move = d.Image
                                                                     .transform
                                                                     .DOMove(destination, scoreTime)
                                                                     .SetEase(scoreEase)
                                                                     .SetDelay(delayTime)
                                                                     .OnComplete(() =>
                                                                     {
                                                                         entry.SetDice(iCached, d.Dice.Value);
                                                                         d.gameObject.SetActive(false);
                                                                     });
                TweenerCore<Vector3, Vector3, VectorOptions> scale = d.Image
                                                                      .transform
                                                                      .DOScale(Vector3.one * 0.1f, scoreTime)
                                                                      .SetEase(scoreEase)
                                                                      .SetDelay(delayTime);
                
                sequence.Insert(0, move);
                sequence.Insert(0, scale);
            }

            sequence.OnComplete(() => onComplete?.Invoke());
            sequence.Play();
        }

        private void OnFinishedScoring(int damage)
        {
            gameplayPlayer.PerformAttack(() =>
                                         {
                                             enemies[^1].Damage(damage);
                                             foreach (GameplayEnemy enemy in enemies)
                                             {
                                                 if (enemy.Health.IsAlive)
                                                 {
                                                     TryRoll();
                                                     return;
                                                 }
                                             }

                                             BattleWin();
                                         });
            
            foreach (Dices.Dice d in GameController.Instance
                                                   .GameInstance.Dice)
            {
                d.Locked = false;
            }
        }
        
        #endregion
        
        #region Dice Control

        public void TryRoll()
        {
            if (remainingRolls > 0)
            {
                remainingRolls--;
                foreach (Dices.Dice d in GameController.Instance.GameInstance.Dice)
                {
                    if (!d.Locked)
                    {
                        d.Roll();
                    }
                }
                
                GameplayUi.Instance.DiceControl.Roll();
                Rolled?.Invoke();
            }
        }
        
        private void HideDice()
        {
            GameplayUi.Instance.DiceControl.HideDice(null);
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
            remainingRolls = rolls;
            foreach (Dices.Dice d in GameController.Instance.GameInstance.Dice)
            {
                d.Locked = false;
            }
            TryRoll();
        }
        
        public void TryEndPlayerTurn()
        {
            HideDice();
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
