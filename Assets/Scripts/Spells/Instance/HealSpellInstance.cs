using System;
using Fantazee.Battle;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class HealSpellInstance : SpellInstance
    {
        private HealSpellData data;
        
        public HealSpellInstance(HealSpellData data) : base(data)
        {
            this.data = data;
        }

        protected override void Apply(Damage damage)
        {
            BattleController.Instance.Player.Heal(damage.Value);
        }
    }
}