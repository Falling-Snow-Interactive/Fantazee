using System;
using System.Collections;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Player;
using Fantazee.Spells.Data;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class HealSpellInstance : SpellInstance
    {
        private HealSpellData data;
        private EventInstance healSfx;
        
        public HealSpellInstance(HealSpellData data) : base(data)
        {
            this.data = data;
            if (!data.CastSfx.IsNull)
            {
                healSfx = RuntimeManager.CreateInstance(data.CastSfx);
            }
        }
        
        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            
            player.Visuals.Action();
            yield return new WaitForSeconds(0.3f);
            player.Heal(damage.Value);
            if (healSfx.isValid())
            {
                healSfx.start();
            }
            yield return new WaitForSeconds(1f);
            onComplete?.Invoke();
        }
    }
}