using System;
using System.Collections;
using System.Collections.Generic;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Battle.Characters.Player;
using Fantazee.Spells.Data;
using UnityEngine;

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
            BattlePlayer player = BattleController.Instance.Player;

            player.Visuals.Attack();

            player.Shield.Add(damage.Value);

            yield return new WaitForSeconds(0.5f);
            
            onComplete?.Invoke();
        }
    }
}