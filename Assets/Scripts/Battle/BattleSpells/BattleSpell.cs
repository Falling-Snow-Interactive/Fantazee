using System;
using System.Collections;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Battle.BattleSpells
{
    [Serializable]
    public abstract class BattleSpell
    {
        [SerializeReference]
        private SpellData data;
        public SpellData Data { get => data; set => data = value; }

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