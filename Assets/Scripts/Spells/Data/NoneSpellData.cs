using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/_Spell_None", 
                     fileName = "None", order = -1)]
    public class NoneSpellData : SpellData
    {
        public override SpellType Type => SpellType.spell_none;
    }
}