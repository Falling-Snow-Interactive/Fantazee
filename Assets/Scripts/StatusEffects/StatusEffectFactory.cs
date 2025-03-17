using System;
using Fantazee.Battle.Characters;
using Fantazee.StatusEffects.Data;
using Fantazee.StatusEffects.Instance;

namespace Fantazee.StatusEffects
{
    public static class StatusEffectFactory
    {
        public static StatusEffectInstance CreateInstance(StatusEffectData data, int turns, BattleCharacter character)
        {
            return data switch
                   {
                       BurnStatusData burn => new BurnStatusInstance(burn, turns, character),
                       _ => throw new ArgumentOutOfRangeException(nameof(data))
                   };
        }
    }
}