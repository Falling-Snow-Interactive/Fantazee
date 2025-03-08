using System;
using System.Collections;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Player;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class ShieldSpellInstance : SpellInstance
    {
        private ShieldSpellData data;
        
        public ShieldSpellInstance(ShieldSpellData data) : base(data)
        {
            this.data = data;
        }
        
        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;

            player.Visuals.Action();
            player.Shield.Add(damage.Value);

            yield return new WaitForSeconds(0.5f);
            
            onComplete?.Invoke();
        }
    }
}