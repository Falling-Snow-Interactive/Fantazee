using System;
using System.Collections;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Battle.BattleSpells
{
    [Serializable]
    public abstract class BattleSpell
    {
        [SerializeReference]
        private SpellData data;

        protected BattleSpell(SpellData spellData)
        {
            data = spellData;
        }

        public void Cast(Damage damage, Action onComplete = null)
        {
            BattleController.Instance.StartCoroutine(CastSequence(damage, onComplete));
        }

        protected abstract IEnumerator CastSequence(Damage damage, Action onComplete = null);
    }
}