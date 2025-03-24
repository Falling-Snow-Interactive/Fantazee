using Fantazee.Battle.Characters.Enemies;
using Fantazee.Battle.Characters.Enemies.Actions;
using Fantazee.Battle.Characters.Enemies.Actions.Randomizer;
using Fantazee.Enemies;
using Fantazee.Enemies.Data;

namespace Fantazee.Battle.Boss.Bosses
{
    public class StumpyBossEnemy : BattleEnemy
    {
        private StumpyData data;

        private ActionRandomizer withSummon;

        private ActionRandomizer toUse;
        protected override ActionRandomizer ActionRandomizer => toUse;

        public override void Initialize(EnemyData data)
        {
            base.Initialize(data);
            
            if (data is StumpyData stumpyData)
            {
                this.data = stumpyData;
                
                withSummon = new ActionRandomizer();
                foreach (ActionRandomizerEntry a in stumpyData.ActionRandomizer)
                {
                    withSummon.Add(a);
                }

                withSummon.Add(stumpyData.SummonAction);
            }
        }

        public override void SetupTurnActions()
        {
            toUse = BattleController.Instance.Enemies.Count > data.EnemiesRemainingForSummon + 1 
                        ? actionRandomizer 
                        : withSummon; // +1 is for the boss. 
            base.SetupTurnActions();
        }
    }
}