using System;
using Fantazee.Spells.Data;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class ShieldSpellInstance : SpellInstance
    {
        private ShieldSpellData data;
        
        public ShieldSpellInstance(ShieldSpellData data) : base(data)
        {
            this.data = data;
        }
    }
}