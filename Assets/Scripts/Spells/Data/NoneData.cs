
using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/None")]
    public class NoneData : SpellData
    {
        public override SpellType Type => SpellType.None;
    }
}
