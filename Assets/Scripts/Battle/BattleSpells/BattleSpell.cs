using System;
using System.Collections;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fantazee.Battle.BattleSpells
{
    [Serializable]
    public abstract class BattleSpell
    {
        private SpellData data;
        private EventInstance castSfx;

        protected BattleSpell(SpellData spellData)
        {
            data = spellData;

            if (!data.CastSfx.IsNull)
            {
                castSfx = RuntimeManager.CreateInstance(data.CastSfx);
            }
        }

        public bool IsNone => data.Type == SpellType.None;

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
    }
}