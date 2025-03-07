using System;
using Fantazee.Spells.Data;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class DaggerSpellInstance : SpellInstance
    {
        private DaggerSpellData data;
        
        public DaggerSpellInstance(DaggerSpellData data) : base(data)
        {
            this.data = data;
        }
    }
}