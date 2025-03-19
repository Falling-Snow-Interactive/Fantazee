using System.Collections.Generic;
using Fantazee.Battle.StatusEffects;
using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_08_Bite", 
                     fileName = "Spell_08_Bite_Data", order = 8)]
    public class BiteSpellData : SpellData
    {
        public override SpellType Type => SpellType.spell_08_bite;
        
        [Header("Bite")]
        
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
