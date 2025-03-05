using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Fireball")]
    public class FireballSpellData : SpellData
    {
        public override SpellType Type => SpellType.Fireball;
    }
}
