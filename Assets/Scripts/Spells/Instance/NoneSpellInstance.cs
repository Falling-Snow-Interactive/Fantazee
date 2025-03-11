using Fantazee.Battle;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    public class NoneSpellInstance : SpellInstance
    {
        public NoneSpellInstance(NoneSpellData data) : base(data) {}

        protected override void Apply(Damage damage)
        {
            Debug.LogWarning("This shouldn't ever be casted.");
        }
    }
}