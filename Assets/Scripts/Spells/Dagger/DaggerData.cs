using UnityEngine;

namespace Fantazee.Spells.Dagger
{
    [CreateAssetMenu(menuName = "Spells/Dagger")]
    public class DaggerData : SpellData
    {
        public override SpellType Type => SpellType.Dagger;
    }
}