using System.Collections.Generic;
using Fantazee.Battle.StatusEffects;
using Fantazee.StatusEffects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_04_Fireball", 
                     fileName = "Spell_04_Fireball", order = 4)]
    public class FireballSpellData : SpellData
    {
        public override SpellType Type => SpellType.spell_04_fireball;

        [Header("Fireball")]

        [Range(0f, 2f)]
        [SerializeField]
        private float damageMod = 1f;
        public float DamageMod => damageMod;
        
        [Header("Status Effect")]
        
        [SerializeField]
        private BattleStatusData statusEffect;
        public BattleStatusData StatusEffect => statusEffect;
        
        protected override Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string, string> args = base.GetDescArgs();

            foreach (KeyValuePair<string, string> d in statusEffect.GetDescArgs())
            {
                args.Add(d.Key, d.Value);
            }
            
            return args;
        }
    }
}
