using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Shield")]
    public class ShieldSpellData : SpellData
    {
        public override SpellType Type => SpellType.Shield;
    }
}