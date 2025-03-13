using System.Collections.Generic;
using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_06_ChainLightning", 
                     fileName = "Spell_06_ChainLightning", order = 6)]
    public class ChainLightningSpellData : SpellData
    {
        public override SpellType Type => SpellType.ChainLightning;

        [Header("Chain Lightning")]

        [Range(0,2)]
        [SerializeField]
        private float firstEnemyMod = 0.75f;
        public float FirstEnemyMod => firstEnemyMod;

        [Range(0, 2)]
        [SerializeField]
        private float secondEnemyMod = 0.25f;
        public float SecondEnemyMod => secondEnemyMod;
        
        protected override Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string, string> dict = base.GetDescArgs();
            dict.Add("FirstMod", $"{firstEnemyMod * 100f}");
            dict.Add("SecondMod", $"{secondEnemyMod * 100f}");
            return dict;
        }
    }
}