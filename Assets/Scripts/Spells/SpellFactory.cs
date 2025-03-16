using System;
using Fantazee.SaveLoad;
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
                           _ => throw new ArgumentOutOfRangeException()
                       };
            }

            Debug.LogWarning($"No Spell Data found for spell type {type}");
            throw new ArgumentOutOfRangeException();
        }

        public static SpellInstance CreateInstance(SpellSave save)
        {
            return save.Data.Type switch
                   {
                       SpellType.spell_none => new NoneSpellInstance(save),
                       SpellType.spell_00_dagger => new NoneSpellInstance(save),
                       SpellType.spell_01_pierce => new PierceSpellInstance(save),
                       SpellType.spell_02_shield => new ShieldSpellInstance(save),
                       SpellType.spell_03_heal => new HealSpellInstance(save),
                       SpellType.spell_04_fireball => new FireballSpellInstance(save),
                       SpellType.spell_05_overflow => new OverflowSpellInstance(save),
                       SpellType.spell_06_chainlightning => new ChainLightningSpellInstance(save),
                       SpellType.spell_07_push => new PushSpellInstance(save),
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }
    }
}