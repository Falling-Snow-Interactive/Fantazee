using System;
using System.Collections;
using Fantazee.Battle;
using Fantazee.Spells.Data;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public abstract class SpellInstance
    {
        [SerializeReference]
        private SpellData data;
        public SpellData Data => data;

        private EventInstance castSfx;
        private EventInstance loopSfx;
        private EventInstance hitSfx;

        protected SpellInstance(SpellData data)
        {
            this.data = data;

            if (!data.CastSfx.IsNull)
            {
                castSfx = RuntimeManager.CreateInstance(data.CastSfx);
            }
        }
        
        public void Cast(Damage damage, Action onComplete = null)
        {
            if (castSfx.isValid())
            {
                castSfx.start();
            }

            if (data.CastVfx)
            {
                Object.Instantiate(data.CastVfx, BattleController.Instance.Player.transform);
            }
            
            BattleController.Instance.StartCoroutine(CastSequence(damage, onComplete));
        }
        
        protected abstract IEnumerator CastSequence(Damage damage, Action onComplete = null);
        
        public override string ToString()
        {
            return data.Name;
        }
    }
}