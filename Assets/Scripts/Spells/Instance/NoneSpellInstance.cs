using System;
using Fantazee.Battle;
using Fantazee.SaveLoad;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class NoneSpellInstance : SpellInstance
    {
        public NoneSpellInstance(NoneSpellData data) : base(data) {}

        public NoneSpellInstance(SpellSave save) : base(save) {}
        
        protected override void Apply(Damage damage, Action onComplete)
        {
            Debug.LogWarning("This shouldn't ever be casted.");
            onComplete?.Invoke();
        }
    }
}