using System;
using System.Collections;
using System.Collections.Generic;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Battle.Characters.Player;
using Fantazee.Spells.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fantazee.Battle.BattleSpells
{
    public class HealBattleSpell : BattleSpell
    {
        public HealBattleSpell(SpellData spellData) : base(spellData)
        {
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            
            player.Visuals.Action();
            yield return new WaitForSeconds(0.3f);
            player.Heal(damage.Value);
            yield return new WaitForSeconds(1f);
            onComplete?.Invoke();
        }
    }
}