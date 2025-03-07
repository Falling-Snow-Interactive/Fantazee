using System;
using System.Collections;
using Fantazee.Battle;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    public class NoneSpellInstance : SpellInstance
    {
        public NoneSpellInstance(NoneSpellData data) : base(data)
        {
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            Debug.Log("This shouldn't ever be casted.");
            throw new NotImplementedException();
        }
    }
}