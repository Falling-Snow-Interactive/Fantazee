using System;
using Fantazee.Battle.Characters;
using Fantazee.StatusEffects.Data;

namespace Fantazee.StatusEffects.Instance
{
    public class BurnStatusInstance : StatusEffectInstance
    {
        private BurnStatusData data;
        
        public BurnStatusInstance(BurnStatusData data, int turns, BattleCharacter character) 
            : base(data, turns, character)
        {
            this.data = data;
        }

        private void OnTurnStart(Action onComplete)
        {
        }
    }
}