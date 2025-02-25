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
    public class PierceBattleSpell : BattleSpell
    {
        private PierceData data;
        
        public PierceBattleSpell(PierceData data) : base(data)
        {
            this.data = data;
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            List<BattleEnemy> enemies = BattleController.Instance.Enemies;

            player.Visuals.Attack();

            yield return new WaitForSeconds(0.2f);

            BattleEnemy e0 = enemies[^1];
            BattleEnemy e1 = null;
            if (enemies.Count > 1)
            {
                e1 = enemies[^2];
            }

            e0.Damage(damage.Value);

            if (e1)
            {
                yield return new WaitForSeconds(0.2f);
                e1.Damage(Mathf.RoundToInt(damage.Value/2f));
            }
            
            yield return new WaitForSeconds(1f);
            
            onComplete?.Invoke();
        }
    }
}