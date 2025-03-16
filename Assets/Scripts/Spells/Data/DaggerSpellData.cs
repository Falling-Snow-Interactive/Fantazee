using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_00_Dagger", 
                     fileName = "Spell_00_Dagger", order = 0)]
    public class DaggerSpellData : SpellData
    {
        public override SpellType Type => SpellType.spell_00_dagger;
    }
}