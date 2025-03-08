using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Dice;
using Fantazee.Instance;
using Fantazee.Relics.Data;

namespace Fantazee.Relics.Instance
{
    public class ExplosiveRelicInstance : RelicInstance
    {
        private readonly ExplosiveRelicData explosiveData;
        
        public ExplosiveRelicInstance(ExplosiveRelicData data, CharacterInstance character) : base(data, character)
        {
            explosiveData = data;
        }

        public override void Enable()
        {
            BattleController.DieRolled += OnDieRolled;
        }

        public override void Disable()
        {
            BattleController.DieRolled -= OnDieRolled;
        }

        private void OnDieRolled(Die die)
        {
            if (die.Value == explosiveData.Number)
            {
                foreach (BattleEnemy enemy in BattleController.Instance.Enemies)
                {
                    if (enemy.Health.IsAlive)
                    {
                        enemy.Damage(explosiveData.Damage);
                    }
                }
            }
        }
    }
}