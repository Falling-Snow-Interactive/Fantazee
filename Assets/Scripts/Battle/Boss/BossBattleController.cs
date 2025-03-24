using Fantazee.Audio;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Enemies;
using Fantazee.Enemies.Data;
using Fantazee.Environments;
using Fantazee.Environments.Settings;
using Fantazee.Instance;

namespace Fantazee.Battle.Boss
{
    public class BossBattleController : BattleController
    {
        protected override void PlayMusic()
        {
            if (EnvironmentSettings.Settings.TryGetEnvironment(GameInstance.Current.Environment.Data.Type,
                                                               out EnvironmentData data))
            {
                MusicController.Instance.PlayMusic(data.BossMusic);
            }
        }

        public override void PlayerWin()
        {
            base.PlayerWin();

            GameController.Instance.GameInstance.Environment.SetReadyToAdvance();
        }

        protected override void SetupEnemies()
        {
            EnvironmentInstance env = GameInstance.Current.Environment;
            float spawnOffset = 0;
            
            StumpyData bossData = env.Data.Boss;
            BattleEnemy boss = Instantiate(bossData.BossEnemy, enemyContainer);
            boss.Initialize(bossData);
            boss.name = bossData.Name;
            Enemies.Add(boss);

            spawnOffset += boss.Data.Size;
            
            for(int i = 0; i < env.Data.BossSpawns.Count; i++)
            {
                EnemyData enemyData = env.Data.BossSpawns[i];
                BattleEnemy enemy = Instantiate(battleEnemyPrefab, enemyContainer);
                enemy.gameObject.name = $"{enemyData.Name} ({i})";
                
                float y = i % 2 == 0 ? 0.05f : -0.05f;
                enemy.transform.localPosition += Vector3.left * spawnOffset + Vector3.up * y;
                enemy.Initialize(enemyData);
                
                spawnOffset += enemy.Data.Size;
                
                Rewards.Add(enemy.Data.BattleRewards);
                Enemies.Insert(0, enemy);
            }
        }
    }
}