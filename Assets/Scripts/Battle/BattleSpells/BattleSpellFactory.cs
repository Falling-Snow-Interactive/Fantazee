using System;
using Fantazee.Spells;
using Fantazee.Spells.Data;

namespace Fantazee.Battle.BattleSpells
{
    public static class BattleSpellFactory
    {
        public static BattleSpell Create(SpellData data)
        {
            return data.Type switch
                   {
                       SpellType.None => new NoneBattleSpell(data),
                       SpellType.Dagger => new DaggerBattleSpell(data as DaggerData),
                       SpellType.Pierce => new PierceBattleSpell(data as PierceData),
                       SpellType.Shield => new ShieldBattleSpell(data as ShieldData),
                       SpellType.Heal => new HealBattleSpell(data as HealSpellData),
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }
    }
}