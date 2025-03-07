using System;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;

namespace Fantazee.Spells
{
    public static class SpellFactory
    {
        public static SpellInstance CreateInstance(SpellData data)
        {
            return data switch
                   {
                       NoneSpellData d => new SpellInstance(d),
                       DaggerSpellData d => new DaggerSpellInstance(d),
                       PierceSpellData d => new PierceSpellInstance(d),
                       ShieldSpellData d => new ShieldSpellInstance(d),
                       HealSpellData d => new HealSpellInstance(d),
                       FireballSpellData d => new FireballSpellInstance(d),
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }
    }
}