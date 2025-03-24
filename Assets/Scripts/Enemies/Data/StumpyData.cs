using Fantazee.Battle.Boss.Bosses;
using Fantazee.Battle.Characters.Enemies.Actions.Randomizer;

namespace Fantazee.Enemies.Data
{
    [CreateAssetMenu(menuName = "Enemies/Stumpy")]
    public class StumpyData : EnemyData
    {
        [Header("Boss")]

        [SerializeField]
        private StumpyBossEnemy bossEnemy;
        public StumpyBossEnemy BossEnemy => bossEnemy;
        
        [Header("Stumpy")]
        
        [SerializeField]
        private int enemiesRemainingForSummon = 1;
        public int EnemiesRemainingForSummon => enemiesRemainingForSummon;

        [SerializeField]
        private ActionRandomizerEntry summonAction;
        public ActionRandomizerEntry SummonAction => summonAction;
    }
}