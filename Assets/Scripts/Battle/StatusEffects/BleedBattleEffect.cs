using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.Characters;
using Fantazee.StatusEffects.Data;

namespace Fantazee.Battle.StatusEffects
{
    public class BleedBattleEffect : BattleStatusEffect, ITakingDamageCallback
    {
        public BleedBattleEffect(BleedStatusData data, int turns, BattleCharacter character) : base(data, turns, character)
        {
        }
        
        public override void Activate()
        {
            base.Activate();
            Character.RegisterTakingDamageReceiver(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            Character.UnregisterTakingDamageReceiver(this);
        }

        public int OnTakingDamage(int damage)
        {
            if (damage <= 0)
            {
                return damage;
            }
            
            damage += TurnsRemaining;
            return damage;
        }
    }
}