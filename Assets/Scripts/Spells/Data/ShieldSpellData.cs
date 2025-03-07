using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_02_Shield", fileName = "Spell_02_Shield", order = 2)]
    public class ShieldSpellData : SpellData
    {
        public override SpellType Type => SpellType.Shield;
    }
}