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
                       NoneSpellData d => new NoneSpellInstance(d),
                       DaggerSpellData d => new DaggerSpellInstance(d),
                       PierceSpellData d => new PierceSpellInstance(d),
                       ShieldSpellData d => new ShieldSpellInstance(d),
                       HealSpellData d => new HealSpellInstance(d),
                       FireballSpellData d => new FireballSpellInstance(d),
                       OverflowSpellData o => new OverflowSpellInstance(o),
                       ChainLightningSpellData c => new ChainLightningSpellInstance(c),
                       PushSpellData p => new PushSpellInstance(p),
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }
    }
}