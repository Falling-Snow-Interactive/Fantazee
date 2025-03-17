using System;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using Fantazee.Spells.Settings;
using UnityEngine;

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
                       BiteSpellData b => new BiteSpellInstance(b),
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }

        public static SpellInstance CreateInstance(SpellType type)
        {
            if (SpellSettings.Settings.TryGetSpell(type, out SpellData data))
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
                           BiteSpellData b => new BiteSpellInstance(b),
                           _ => throw new ArgumentOutOfRangeException()
                       };
            }

            Debug.LogWarning($"No Spell Data found for spell type {type}");
            throw new ArgumentOutOfRangeException();
        }
    }
}