using System;
using Fantazee.Spells.Data;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class PierceSpellInstance : SpellInstance
    {
        private PierceSpellData data;

        public PierceSpellInstance(PierceSpellData data) : base(data)
        {
            this.data = data;
        }
    }
}