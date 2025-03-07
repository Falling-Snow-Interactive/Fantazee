using System;
using System.Collections;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using FMOD.Studio;
using FMODUnity;
using Object = UnityEngine.Object;

namespace Fantazee.Battle.BattleSpells
{
    [Serializable]
    public abstract class BattleSpell
    {
        private SpellData spellData;
        private EventInstance castSfx;

        protected BattleSpell(SpellData spellSpellSpellData)
        {
            spellData = spellSpellSpellData;

            if (!spellData.CastSfx.IsNull)
            {
                castSfx = RuntimeManager.CreateInstance(spellData.CastSfx);
            }
        }

        public bool IsNone => spellData.Type == SpellType.None;

        public void Cast(Damage damage, Action onComplete = null)
        {
            if (castSfx.isValid())
            {
                castSfx.start();
            }

            if (spellData.CastVfx)
            {
                Object.Instantiate(spellData.CastVfx, BattleController.Instance.Player.transform);
            }
            
            BattleController.Instance.StartCoroutine(CastSequence(damage, onComplete));
        }

        protected abstract IEnumerator CastSequence(Damage damage, Action onComplete = null);
    }
}