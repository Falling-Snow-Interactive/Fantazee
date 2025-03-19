using System.Collections.Generic;
using Fantazee.Battle.StatusEffects;
using Fantazee.StatusEffects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_09_AcidSpit", 
                     fileName = "spell_09_acid_spit_data", order = 8)]
    public class AcidSpitSpellData : SpellData
    {
        public override SpellType Type => SpellType.spell_09_acid_spit;
        
        [Header("Acid Spit")]
        
        [SerializeField]
        private BattleStatusData status;
        public BattleStatusData Status => status;

        protected override Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string, string> args = base.GetDescArgs();
            foreach (KeyValuePair<string, string> d in status.GetDescArgs())
            {
                args.Add(d.Key, d.Value);
            }
            return args;
        }
    }
}
