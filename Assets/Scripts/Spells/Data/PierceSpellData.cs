using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_01_Pierce", 
                     fileName = "Spell_01_Pierce", order = 1)]
    public class PierceSpellData : SpellData
    {
        public override SpellType Type => SpellType.spell_01_pierce;
        
        [Header("Pierce")]

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
