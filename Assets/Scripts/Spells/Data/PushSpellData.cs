using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_07_Push", 
                     fileName = "Spell_07_Push", order = 7)]
    public class PushSpellData : SpellData
    {
        public override SpellType Type => SpellType.Push;

        [Header("Push")]

        [Range(0, 2f)]
        [SerializeField]
        private float pushDamageMod;
        public float PushDamageMod => pushDamageMod;
        
        [Header("Move")]
        
        [SerializeField]
        private float moveTime = 0.2f;
        public float MoveTime => moveTime;
        
        [SerializeField]
        private Ease moveEase = Ease.Linear;
        public Ease MoveEase => moveEase;

        [SerializeField]
        private float moveDelay = 0.0f;
        public float MoveDelay => moveDelay;

        protected override Dictionary<string, string> GetDescArgs()
        {
            float percent = pushDamageMod * 100f;
            
            Dictionary<string, string> args = base.GetDescArgs();
            args.Add("Percent", $"{percent}");
            return args;
        }
    }
}