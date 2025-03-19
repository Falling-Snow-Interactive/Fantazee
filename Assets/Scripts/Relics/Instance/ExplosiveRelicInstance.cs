using System;
using Fantazee.Battle;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Dice;
using Fantazee.Instance;
using Fantazee.Relics.Data;

namespace Fantazee.Relics.Instance
{
    public class ExplosiveRelicInstance : RelicInstance, IDieRolledCallbackReceivers
    {
        private readonly ExplosiveRelicData explosiveData;
        
        public ExplosiveRelicInstance(ExplosiveRelicData data, CharacterInstance character) : base(data, character)
        {
            explosiveData = data;
        }

        public override void Enable()
        {
            BattleController.BattleStarted += OnBattleStart;
            BattleController.BattleEnded += OnBattleEnd;
        }

        public override void Disable()
        {
            BattleController.BattleStarted -= OnBattleStart;
            BattleController.BattleEnded -= OnBattleEnd;
        }

        private void OnBattleStart()
        {
            BattleController.Instance.Player.RegisterDieRolledReceiver(this);
        }

        private void OnBattleEnd()
        {
            BattleController.Instance.Player.UnregisterDieRolledReceiver(this);
        }

        public void OnDieRolled(Die die, Action onComplete)
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
            
            onComplete?.Invoke();
        }
    }
}