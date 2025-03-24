using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Audio;
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Battle.Characters.Player;
using Fantazee.Battle.Ui;
using Fantazee.Enemies;
using Fantazee.Environments;
using Fantazee.Environments.Settings;
using Fantazee.Instance;
using Fsi.Gameplay;
using UnityEngine;
using Random = UnityEngine.Random;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Battle
{
    public class BattleController : MbSingleton<BattleController>
    {
        public static event Action BattleStarted;
        public static event Action BattleEnded;
        
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

        private BattlePlayer player;
        public BattlePlayer Player => player;

        protected List<BattleEnemy> enemies = new();
        public List<BattleEnemy> Enemies => enemies;

        [Header("Characters")]

        [SerializeField]
        private Transform playerContainer;

        [SerializeField]
        private BattlePlayer battlePlayerPrefab;
        
        [Header("Enemies")]
        
        [SerializeField]
        protected Transform enemyContainer;

        [SerializeField]
        protected BattleEnemy battleEnemyPrefab;

        [SerializeReference]
        protected BattleRewards rewards;
        
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
            BattleStarted?.Invoke();
            StartIntroduction(OnIntroductionFinished);
        }
        
        #region Setup Battle

        private void SetupBattle()
        {
            Debug.Log($"Battle - Setup");
            
            player = Instantiate(battlePlayerPrefab, playerContainer);
            Player.Initialize(GameInstance.Current.Character);
            rewards = new BattleRewards();
            
            SetupRelics();
            SetupEnemies();
            
            PlayMusic();
        }

        protected virtual void PlayMusic()
        {
            if (EnvironmentSettings.Settings.TryGetEnvironment(GameInstance.Current.Environment.Data.Type,
                                                               out EnvironmentData data))
            {
                MusicController.Instance.PlayMusic(data.BattleMusic);
            }
        }

        private void SetupRelics()
        {
            Debug.Log("Battle: Setup Relics");
            BattleUi.Instance.RelicUi.Initialize(GameInstance.Current.Character.Relics);
        }

        protected virtual void SetupEnemies()
        {
            EnvironmentInstance env = GameInstance.Current.Environment;
            if (env.Data.EnemyPool.Count > 0)
            {
                int spawns = env.Data.EnemySpawns.Random();
                for (int i = 0; i < spawns; i++)
                {
                    EnemyData enemyData = env.Data.EnemyPool[Random.Range(0, env.Data.EnemyPool.Count)];
                    SpawnEnemy(enemyData);
                }
            }
        }

        public BattleEnemy SpawnEnemy(EnemyData data)
        {
            BattleEnemy enemy = Instantiate(battleEnemyPrefab, enemyContainer);

            float y = enemies.Count % 2 == 0 ? 0.05f : -0.05f;
            float spawnOffset = 0;
            foreach (BattleEnemy spawned in enemies)
            {
                spawnOffset += spawned.Data.Size;
            }
            enemy.transform.localPosition += Vector3.left * spawnOffset + Vector3.up * y;
            enemy.Initialize(data);
            
            enemy.gameObject.name = $"{data.Name}";

            enemy.Hide(null, true);
            
            rewards.Add(enemy.Data.BattleRewards);
            enemies.Insert(0, enemy);

            return enemy;
        }
        
        #endregion
        
        #region Introduce

        private void StartIntroduction(Action onComplete = null)
        {
            StartCoroutine(IntroductionSequence(onComplete));
        }

        private IEnumerator IntroductionSequence(Action onComplete = null)
        {
            yield return new WaitForSeconds(0.2f);
            
            // Move enemies in
            for (int i = 0; i < enemies.Count; i++)
            {
                BattleEnemy enemy = enemies[i];
                enemy.Show(null, i * 0.2f, false);
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
                PlayerWin();
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
            
            CheckWin();
        }
        
        #endregion
        
        #region Player Control
        
        private void StartPlayerTurn()
        {
            foreach (BattleEnemy enemy in Enemies)
            {
                enemy.SetupTurnActions();
            }
            
            player.StartTurn(OnPlayerTurnEnd);
        }
        
        private void OnPlayerTurnEnd()
        {
            StartCoroutine(TurnEndDelay());
        }

        private IEnumerator TurnEndDelay()
        {
            yield return new WaitForSeconds(0.5f);
            StartEnemyTurn();
        }
        
        #endregion
        
        #region Enemy Control

        public int EnemiesRemaining()
        {
            int count = 0;
            foreach (BattleEnemy e in enemies)
            {
                if (e.Health.IsAlive)
                {
                    count++;
                }
            }
            return count;
        }

        private void StartEnemyTurn()
        {
            Queue<BattleEnemy> enemyQueue = new();
            foreach (BattleEnemy e in enemies)
            {
                if (e.Health.IsAlive)
                {
                    enemyQueue.Enqueue(e);
                }
            }
            
            DoNextEnemyTurn(enemyQueue);
        }

        private void DoNextEnemyTurn(Queue<BattleEnemy> queue)
        {
            if (queue.Count > 0)
            {
                BattleEnemy enemy = queue.Dequeue();
                enemy.StartTurn(() => OnEnemyTurnEnd(queue));
            }
            else
            {
                Debug.LogError("Battle: Shouldn't be doing an enemy turn on an empty queue.", gameObject);
            }
        }

        private void OnEnemyTurnEnd(Queue<BattleEnemy> queue)
        {
            if (queue.Count > 0)
            {
                DoNextEnemyTurn(queue);
            }
            else
            {
                DOTween.Sequence().AppendInterval(0.5f).OnComplete(EnemyTurnsFinished).Play(); // Settings up a delay
                // EnemyTurnsFinished();
            }
        }
        
        private void EnemyTurnsFinished()
        {
            StartPlayerTurn();
        }

        public bool TryGetFrontEnemy(out BattleEnemy enemy)
        {
            foreach(BattleEnemy e in enemies)
            {
                if (e.Health.IsAlive)
                {
                    enemy = e;
                    return true;
                }
            }

            enemy = null;
            return false;
        }
        
        #endregion
        
        #region Battle End

        public virtual void PlayerWin()
        {
            DOTween.Complete(Player.transform);
            Player.transform.DOLocalMoveX(9, 0.5f).SetEase(Ease.InOutCubic)
                  .OnComplete(() =>
                              {
                                  BattleUi.Instance.ShowWinScreen();
                                  BattleUi.Instance.WinScreen.Initialize(rewards, OnBattleWinContinue);
                                  Player.Visuals.Victory();
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
                  .OnPlay(() =>
                          {
                              // Player.Visuals.Idle();
                          })
                  .OnComplete(() =>
                              {
                                  rewards.Grant();
                                  BattleEnded?.Invoke();
                                  GameController.Instance.FinishedBattle(true);
                              });
        }
        
        #endregion
    }
}
