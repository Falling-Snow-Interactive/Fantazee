using System;
using Fantazee.Battle.Characters;
using Fantazee.StatusEffects;
using Fantazee.StatusEffects.Data;

namespace Fantazee.Battle.StatusEffects
{
    public static class BattleStatusFactory
    {
        public static BattleStatusEffect CreateInstance(StatusEffectData data, int turns, BattleCharacter character)
        {
            return data switch
                   {
                       BurnStatusData burn => new BurnBattleBattleStatus(burn, turns, character),
                       _ => throw new ArgumentOutOfRangeException(nameof(data))
                   };
        }
    }
}