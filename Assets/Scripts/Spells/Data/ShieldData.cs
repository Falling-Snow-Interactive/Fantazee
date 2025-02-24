using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Shield")]
    public class ShieldData : SpellData
    {
        public override SpellType Type => SpellType.Shield;
    }
}