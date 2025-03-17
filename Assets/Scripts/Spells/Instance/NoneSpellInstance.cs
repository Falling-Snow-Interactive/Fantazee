using System;
using Fantazee.Battle;
using Fantazee.Scores;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    public class NoneSpellInstance : SpellInstance
    {
        public NoneSpellInstance(NoneSpellData data) : base(data) {}

        protected override void Apply(ScoreResults scoreResults, Action onComplete)
        {
            Debug.LogWarning("This shouldn't ever be casted.");
            onComplete?.Invoke();
        }
    }
}