using System;
using System.Collections;
using Fantazee.Spells.Data;

namespace Fantazee.Battle.BattleSpells
{
    public class NoneBattleSpell : BattleSpell
    {
        public NoneBattleSpell(SpellData spellData) : base(spellData)
        {
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            yield return null;
            onComplete?.Invoke();
        }
    }
}