using System;
using System.Collections;
using System.Collections.Generic;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Battle.Characters.Player;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Battle.BattleSpells
{
    public class FireballBattleSpell : BattleSpell
    {
        private FireballSpellData fireballData;
        
        public FireballBattleSpell(FireballSpellData fireballData) : base(fireballData)
        {
            this.fireballData = fireballData;
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            List<BattleEnemy> enemies = BattleController.Instance.Enemies;
            
            float count = enemies.Count;
            
            // TODO - should probably have a vfx here
            
            player.Visuals.Attack();
            yield return new WaitForSeconds(0.2f);
            foreach (BattleEnemy enemy in enemies)
            {
                if (enemy.Health.IsAlive)
                {
                    enemy.Damage(Mathf.RoundToInt(damage.Value/count));
                }
            }
            yield return new WaitForSeconds(1f);
            onComplete?.Invoke();
        }
    }
}