using System;
using Fantazee.Battle;
using Fantazee.SaveLoad;
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

        public HealSpellInstance(SpellSave save) : base(save)
        {
            data = save.Data as HealSpellData;
        }

        protected override void Apply(Damage damage, Action onComplete)
        {
            int h = Mathf.RoundToInt(damage.Value * data.HealMod);
            BattleController.Instance.Player.Heal(h);
            onComplete?.Invoke();
        }

        protected override Vector3 GetHitPos()
        {
            return BattleController.Instance.Player.transform.position;
        }
    }
}