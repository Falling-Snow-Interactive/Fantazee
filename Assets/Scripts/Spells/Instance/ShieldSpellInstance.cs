using System;
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
        protected override void Apply(Damage damage, Action onCompelte)
        {
            BattlePlayer player = BattleController.Instance.Player;

            player.Visuals.Action();
            int d = Mathf.RoundToInt(damage.Value * data.ShieldMod);
            player.Shield.Add(d);
            onCompelte?.Invoke();
        }

        protected override Vector3 GetHitPos()
        {
            return Vector3.zero;
        }
    }
}