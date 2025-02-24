using System;
using System.Collections;
using Fantazee.Spells;
using Fantazee.Spells.Data;

namespace Fantazee.Battle.BattleSpells
{
    public class PierceBattleSpell : BattleSpell
    {
        public PierceBattleSpell(SpellData spellData) : base(spellData)
        {
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            throw new NotImplementedException();
        }
    }
}