using System;
using System.Collections;
using Fantazee.Spells.Data;

namespace Fantazee.Battle.BattleSpells
{
    [Serializable]
    public class ShieldBattleSpell : BattleSpell
    {
        private ShieldData data;
        
        public ShieldBattleSpell(ShieldData data) : base(data)
        {
            this.data = data;
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            throw new NotImplementedException();
        }
    }
}