using System;
using System.Collections;
using System.Collections.Generic;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Battle.Characters.Player;
using Fantazee.Spells.Data;
using FMODUnity;
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

            RuntimeManager.PlayOneShot(data.AttackSfx);
            player.Visuals.Attack();

            yield return new WaitForSeconds(0.2f);
            
            RuntimeManager.PlayOneShot(data.AttackHitSfx);
            enemies[^1].Damage(damage.Value);

            if (enemies.Count > 1)
            {
                yield return new WaitForSeconds(0.2f);
                enemies[^2].Damage(Mathf.RoundToInt(damage.Value/2f));
            }
            
            yield return new WaitForSeconds(1f);
            
            onComplete?.Invoke();
        }
    }
}