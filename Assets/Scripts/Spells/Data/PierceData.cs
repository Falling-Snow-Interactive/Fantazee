
using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Pierce")]
    public class PierceData : SpellData
    {
        public override SpellType Type => SpellType.Pierce;
    }
}
