using System;
using Fantazee.Spells.Data;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class FireballSpellInstance : SpellInstance
    {
        private FireballSpellData data;
        
        public FireballSpellInstance(FireballSpellData data) : base(data)
        {
            this.data = data;
        }
    }
}