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
    public class DaggerBattleSpell : BattleSpell
    {
        private DaggerData data;
        
        public DaggerBattleSpell(DaggerData data) : base(data)
        {
            this.data = data;
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            List<BattleEnemy> enemies = BattleController.Instance.Enemies;
            
            player.Visuals.Attack();
            yield return new WaitForSeconds(0.2f);
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].Health.IsAlive)
                {
                    enemies[i].Damage(damage.Value);
                    break;
                }
            }
            yield return new WaitForSeconds(1f);
            onComplete?.Invoke();
        }
    }
}