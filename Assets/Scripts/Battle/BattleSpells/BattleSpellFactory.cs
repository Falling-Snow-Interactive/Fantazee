using System;
using Fantazee.Spells;
using Fantazee.Spells.Dagger;

namespace Fantazee.Battle.BattleSpells
{
    public static class BattleSpellFactory
    {
        public static BattleSpell Create(SpellData data)
        {
            return data.Type switch
                   {
                       SpellType.Dagger => new DaggerBattleSpell(data as DaggerData),
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }
    }
}