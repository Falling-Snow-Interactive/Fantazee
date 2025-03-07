using System;
using Fantazee.Spells.Data;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class HealSpellInstance : SpellInstance
    {
        private HealSpellData data;
        
        public HealSpellInstance(HealSpellData data) : base(data)
        {
            this.data = data;
        }
    }
}