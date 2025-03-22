using System;
using Fantazee.Battle;
using Fantazee.Scores;
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

        protected override void Apply(ScoreResults scoreResults, Action onComplete)
        {
            int h = Mathf.RoundToInt(scoreResults.Value * data.HealMod);
            BattleController.Instance.Player.Heal(h);
            onComplete?.Invoke();
        }
        
        protected override void OnCast()
        {
            BattleController.Instance.Player.Visuals.Action();
            base.OnCast();
        }

        protected override Vector3 GetHitPos()
        {
            return BattleController.Instance.Player.transform.position;
        }
    }
}